using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Commands;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Rest.Models.Interaction;
using Color_Chan.Discord.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{apiVersion}/discord")]
    public class DiscordInteractionController : ControllerBase
    {
        private const string ReturnContentType = "application/json";
        private readonly IDiscordInteractionAuthService _authService;
        private readonly ILogger<DiscordInteractionController> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ISlashCommandService _slashCommandService;

        public DiscordInteractionController(IDiscordInteractionAuthService authService, ISlashCommandService slashCommandService, ILogger<DiscordInteractionController> logger, 
                                            IServiceProvider serviceProvider , IOptions<JsonSerializerOptions> serializerOptions)
        {
            _authService = authService;
            _slashCommandService = slashCommandService;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _serializerOptions = serializerOptions.Value;
        }

        [HttpPost("interaction")]
        public async Task<ActionResult> HandleInteractionRequestAsync()
        {
            // Todo: Benchmark
            
            // Get the raw body data.
            using var bodyReader = new StreamReader(Request.Body);
            if (bodyReader.BaseStream.CanSeek) bodyReader.BaseStream.Seek(0, SeekOrigin.Begin);
            string rawBody = await bodyReader.ReadToEndAsync().ConfigureAwait(false);

            // Verify the interaction request.
            var signature = Request.Headers["X-Signature-Ed25519"].ToString();
            var timeStamp = Request.Headers["X-Signature-Timestamp"].ToString();
            if (!_authService.VerifySignature(signature, timeStamp, rawBody))
            {
                _logger.LogWarning("Failed to verify interaction command");
                return Unauthorized("Failed to verify signatures");
            }

            // Convert the JSON body to a DiscordInteractionData object.
            if (Request.Body.CanSeek) Request.Body.Seek(0, SeekOrigin.Begin);
            var interactionData = await JsonSerializer.DeserializeAsync<DiscordInteractionData>(Request.Body, _serializerOptions).ConfigureAwait(false);
            if (interactionData is null)
            {
                throw new JsonException("Failed to deserialize JSON body to DiscordInteractionData");
            }

            _logger.LogDebug("Verified Interaction {Id}", interactionData.Id.ToString());

            var result = interactionData.Type switch
            {
                DiscordInteractionType.Ping => DiscordInteractionResponse.PingResponse().ToDataModel(),
                DiscordInteractionType.MessageComponent => throw new NotSupportedException("MessageComponent interactions are currently not supported yet!"),
                DiscordInteractionType.ApplicationCommand => await HandleSlashCommandAsync(new DiscordInteraction(interactionData)).ConfigureAwait(false),
                _ => throw new NotSupportedException("This interaction type is currently not supported!")
            };

            return Content(JsonSerializer.Serialize(result, result.GetType(), _serializerOptions), ReturnContentType, Encoding.UTF8);
        }

        private async Task<DiscordInteractionResponseData> HandleSlashCommandAsync(IDiscordInteraction interaction)
        {
            // Todo: make a dedicated interaction slash command handler.
            
            if (interaction.Data is null)
            {
                throw new NullReferenceException("Interaction data can not be null for a slash command!");
            }
            
            var context = new SlashCommandContext(interaction.User!, interaction.Message!, interaction.Data)
            {
                Member = interaction.GuildMember,
                GuildId = interaction.GuildId,
                ChannelId = interaction.ChannelId!.Value,
                ApplicationId = interaction.ApplicationId
            };
            
            var result = await _slashCommandService.ExecuteSlashCommandAsync(interaction.Data.Name, context, _serviceProvider).ConfigureAwait(false);
            if (result.IsSuccessful) return result.Entity!.ToDataModel();
            return new DiscordInteractionResponseData();
        }
    }
}