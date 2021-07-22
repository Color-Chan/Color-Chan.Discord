using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Models.Embed;
using Color_Chan.Discord.Models.Interaction;

namespace Pong.Commands
{
    public class PongCommands : SlashCommandModule
    {
        [SlashCommand("ping", "Ping Pong!")]
        public Task<IDiscordInteractionResponse> PongAsync()
        {
            var response = new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.ChannelMessageWithSource,
                Data = new DiscordInteractionCommandCallback
                {
                    Embeds = new List<IDiscordEmbed>
                    {
                        new DiscordEmbed
                        {
                            Description = "Pong!",
                            Timestamp = DateTimeOffset.UtcNow,
                            Color = Color.Cyan
                        }
                    }
                }
            };

            return Task.FromResult<IDiscordInteractionResponse>(response);
        }
    }
}