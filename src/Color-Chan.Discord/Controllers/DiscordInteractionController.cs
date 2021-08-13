using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Configurations;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.Params.Webhook;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Rest.Models.Interaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Controllers
{
    /// <summary>
    ///     The api controller that will receive all the interaction events from discord.
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{apiVersion}/discord")]
    public class DiscordInteractionController : ControllerBase
    {
        private const string SignatureHeader = "X-Signature-Ed25519";
        private const string TimeStampHeader = "X-Signature-Timestamp";
        private const string ReturnContentType = "application/json";
        private readonly IDiscordInteractionAuthService _authService;
        private readonly IDiscordSlashCommandHandler _commandHandler;
        private readonly InteractionsConfiguration _configuration;
        private readonly DiscordTokens _discordTokens;
        private readonly ILogger<DiscordInteractionController> _logger;
        private readonly IDiscordRestApplication _restApplication;
        private readonly JsonSerializerOptions _serializerOptions;

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordInteractionController" />.
        /// </summary>
        /// <param name="authService">The <see cref="IDiscordInteractionAuthService" /> that will verify the request.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="serializerOptions">The JSON options used for serialization.</param>
        /// <param name="commandHandler">The command handler for all the slash commands.</param>
        /// <param name="configuration">The configurations for interactions.</param>
        /// <param name="restApplication">The REST class for application api calls.</param>
        /// <param name="discordTokens">The bot tokens and IDs.</param>
        public DiscordInteractionController(IDiscordInteractionAuthService authService, ILogger<DiscordInteractionController> logger, IOptions<JsonSerializerOptions> serializerOptions,
                                            IDiscordSlashCommandHandler commandHandler, IOptions<InteractionsConfiguration> configuration, IDiscordRestApplication restApplication,
                                            DiscordTokens discordTokens)
        {
            _authService = authService;
            _logger = logger;
            _commandHandler = commandHandler;
            _configuration = configuration.Value;
            _restApplication = restApplication;
            _discordTokens = discordTokens;
            _serializerOptions = serializerOptions.Value;
        }

        /// <summary>
        ///     Handles an interaction event from discord.
        /// </summary>
        /// <returns>
        ///     An <see cref="ActionResult" /> with the json result of the request.
        /// </returns>
        /// <exception cref="JsonException">Thrown when the provided request body isn't valid.</exception>
        [HttpPost("interaction")]
        public async Task<ActionResult> HandleInteractionRequestAsync()
        {
            // Check if the request has the headers needed for verification.
            if (!Request.Headers.ContainsKey(SignatureHeader) || !Request.Headers.ContainsKey(TimeStampHeader))
            {
                return Unauthorized();
            }

            // Verify the interaction request.
            var signature = Request.Headers[SignatureHeader].ToString();
            var timeStamp = Request.Headers[TimeStampHeader].ToString();

            if (_configuration.VerifyInteractions && !await _authService.VerifySignatureAsync(signature, timeStamp, Request.Body).ConfigureAwait(false))
            {
                _logger.LogWarning("Failed to verify interaction command");
                return Unauthorized("Failed to verify signatures");
            }

            // Convert the JSON body to a DiscordInteractionData object.
            if (Request.Body.CanSeek) Request.Body.Seek(0, SeekOrigin.Begin);
            var interactionData = await JsonSerializer.DeserializeAsync<DiscordInteractionData>(Request.Body, _serializerOptions).ConfigureAwait(false);
            if (interactionData is null) throw new JsonException("Failed to deserialize JSON body to DiscordInteractionData");

            _logger.LogDebug("Verified Interaction {Id}", interactionData.Id.ToString());

            // Acknowledge the interaction.
            if (_configuration.AcknowledgeInteractions && interactionData.RequestType != DiscordInteractionRequestType.Ping)
            {
                var acknowledgeResponse = new DiscordInteractionResponseData
                {
                    Type = DiscordInteractionResponseType.DeferredChannelMessageWithSource
                };

                var acknowledgeResult = await _restApplication.CreateInteractionResponseAsync(interactionData.Id, interactionData.Token, acknowledgeResponse).ConfigureAwait(false);
                if (!acknowledgeResult.IsSuccessful)
                {
                    _logger.LogWarning("Failed to acknowledge interaction {Id}, reason: {Message}", interactionData.Id.ToString(), acknowledgeResult.ErrorResult?.ErrorMessage);
                }
            }

            // Execute the correct interaction type.
            var response = interactionData.RequestType switch
            {
                DiscordInteractionRequestType.Ping => PingResponse(),
                DiscordInteractionRequestType.ApplicationCommand => await _commandHandler.HandleSlashCommandAsync(new DiscordInteraction(interactionData)).ConfigureAwait(false),
                DiscordInteractionRequestType.MessageComponent => throw new NotImplementedException(),
                _ => throw new ArgumentOutOfRangeException()
            };

            // Send the response back to discord.
            if (!_configuration.AcknowledgeInteractions || interactionData.RequestType == DiscordInteractionRequestType.Ping)
            {
                return SerializeResult(response);
            }

            // Send an edit request.
            var responseData = response.Data?.ToDataModel();
            var editResponse = new DiscordEditWebhookMessage
            {
                Content = responseData?.Content,
                Embeds = responseData?.Embeds,
                Components = responseData?.Components,
                AllowedMentions = responseData?.AllowedMentions
            };

            var responseResult = await _restApplication.EditOriginalInteractionResponseAsync(_discordTokens.ApplicationId, interactionData.Token, editResponse).ConfigureAwait(false);
            if (responseResult.IsSuccessful) return Ok();

            // Send an error response.
            _logger.LogWarning("Failed to edit interaction response {Id}, reason: {Message}", interactionData.Id.ToString(), responseResult.ErrorResult?.ErrorMessage);
            return StatusCode(StatusCodes.Status500InternalServerError, responseResult.ErrorResult);
        }

        /// <summary>
        ///     Get a ping response.
        /// </summary>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse" /> containing a ping response.
        /// </returns>
        private static IDiscordInteractionResponse PingResponse()
        {
            return new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.Pong,
                Data = null
            };
        }

        /// <summary>
        ///     Serializes a <see cref="IDiscordInteractionResponse" /> to a <see cref="DiscordInteractionResponseData" />.
        /// </summary>
        /// <param name="result">The <see cref="IDiscordInteractionResponse" /> that will be serialized.</param>
        /// <returns>
        ///     The serialized <see cref="IDiscordInteractionResponse" />.
        /// </returns>
        private ContentResult SerializeResult(IDiscordInteractionResponse result)
        {
            var data = result.ToDataModel();
            return Content(JsonSerializer.Serialize(data, data.GetType(), _serializerOptions), ReturnContentType, Encoding.UTF8);
        }
    }
}