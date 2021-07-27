using System;
using System.IO;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Commands;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Rest.Models.Interaction;
using Color_Chan.Discord.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{apiVersion}/discord")]
    public class DiscordInteractionController : ControllerBase
    {
        private readonly IDiscordInteractionAuthService _authService;
        private readonly ILogger<DiscordInteractionController> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISlashCommandService _slashCommandService;

        public DiscordInteractionController(IDiscordInteractionAuthService authService, ISlashCommandService slashCommandService,
                                            ILogger<DiscordInteractionController> logger, IServiceProvider serviceProvider)
        {
            _authService = authService;
            _slashCommandService = slashCommandService;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        [HttpPost("interaction")]
        public async Task<ActionResult<DiscordInteractionResponseData>> HandleInteractionRequestAsync([FromBody] DiscordInteractionData interactionData)
        {
            // Get the raw body data.
            using var reader = new StreamReader(Request.Body);
            if (reader.BaseStream.CanSeek) reader.BaseStream.Seek(0, SeekOrigin.Begin);
            string rawBody = await reader.ReadToEndAsync().ConfigureAwait(false);

            // Verify the interaction request.
            var signature = Request.Headers["X-Signature-Ed25519"].ToString();
            var timeStamp = Request.Headers["X-Signature-Timestamp"].ToString();
            if (!_authService.VerifySignature(signature, timeStamp, rawBody))
            {
                _logger.LogWarning("Failed to verify interaction command {Id}", interactionData.Id.ToString());
                return Unauthorized("Failed to verify signatures");
            }

            _logger.LogDebug("Verified Interaction {Id}", interactionData.Id.ToString());

            return interactionData.Type switch
            {
                DiscordInteractionType.Ping => DiscordInteractionResponse.PingResponse().ToDataModel(),
                DiscordInteractionType.MessageComponent => throw new NotSupportedException("MessageComponent interactions are currently not supported yet!"),
                DiscordInteractionType.ApplicationCommand => await HandleSlashCommandAsync(new DiscordInteraction(interactionData)).ConfigureAwait(false),
                _ => throw new NotSupportedException("This interaction type is currently not supported!")
            };
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