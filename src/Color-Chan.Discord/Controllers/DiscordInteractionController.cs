using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Commands;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Models.Interaction;
using Color_Chan.Discord.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Controllers
{
    [ApiController]
    [Route("discord")]
    public class DiscordInteractionController : ControllerBase
    {
        private readonly IDiscordInteractionAuthService _authService;
        private readonly ISlashCommandService _slashCommandService;
        private readonly ILogger<DiscordInteractionController> _logger;
        private readonly IServiceProvider _serviceProvider;

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

            // Todo: make a dedicated interaction command handler.
            
            if (interactionData.Type == DiscordInteractionType.Ping)
            {
                return DiscordInteractionResponse
                    .PingResponse()
                    .ToDataModel();
            }

            var interaction = new DiscordInteraction(interactionData);

            if (interaction.Data is not null)
            {
                var context = new SlashCommandContext(interaction.GuildMember, interaction.User!, interaction.Message!, interaction.Data);
                var result = await _slashCommandService.ExecuteSlashCommandAsync(interaction.Data.Name, context, _serviceProvider).ConfigureAwait(false);
                
                if(result.IsSuccessful)
                    return result.Entity!.ToDataModel();
            }

            return new DiscordInteractionResponseData
            {
                Type = DiscordInteractionResponseType.ChannelMessageWithSource,
                Data = new DiscordInteractionCommandCallbackData
                {
                    Embeds = new List<DiscordEmbedData>
                    {
                        new()
                        {
                            Description = "Something went wrong",
                            Footer = new DiscordEmbedFooterData
                            {
                                Text = "This library is still in development!!!!!"
                            }
                        }
                    }
                }
            };
        }
    }
}