using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Configurations;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Rest.Models.Interaction;
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
        private const string ReturnContentType = "application/json";
        private readonly IDiscordInteractionAuthService _authService;
        private readonly IDiscordSlashCommandHandler _commandHandler;
        private readonly InteractionsConfiguration _configuration;
        private readonly IDiscordRestApplication _restApplication;
        private readonly ILogger<DiscordInteractionController> _logger;
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
        public DiscordInteractionController(IDiscordInteractionAuthService authService, ILogger<DiscordInteractionController> logger, IOptions<JsonSerializerOptions> serializerOptions,
                                            IDiscordSlashCommandHandler commandHandler, InteractionsConfiguration configuration, IDiscordRestApplication restApplication)
        {
            _authService = authService;
            _logger = logger;
            _commandHandler = commandHandler;
            _configuration = configuration;
            _restApplication = restApplication;
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
            // Verify the interaction request.
            var signature = Request.Headers["X-Signature-Ed25519"].ToString();
            var timeStamp = Request.Headers["X-Signature-Timestamp"].ToString();
            if (!await _authService.VerifySignatureAsync(signature, timeStamp, Request.Body).ConfigureAwait(false))
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
            if (_configuration.AcknowledgeInteractions)
            {
                var response = new DiscordInteractionResponseData
                {
                    Type = DiscordInteractionResponseType.DeferredChannelMessageWithSource
                };
                await _restApplication.CreateInteractionResponse(interactionData.Id, interactionData.Token, response).ConfigureAwait(false);
            }
            
            // Execute the correct interaction type.
            switch (interactionData.Type)
            {
                case DiscordInteractionType.Ping:
                    return SerializeResult(PingResponse());
                case DiscordInteractionType.ApplicationCommand:
                    var slashResult = await _commandHandler.HandleSlashCommandAsync(new DiscordInteraction(interactionData)).ConfigureAwait(false);
                    return SerializeResult(slashResult);
                case DiscordInteractionType.MessageComponent:
                    throw new NotSupportedException("MessageComponent interactions are currently not supported yet!");
                default:
                    throw new NotSupportedException("This interaction type is currently not supported!");
            }
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