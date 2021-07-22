using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
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
                    Content = "Pong!"
                }
            };

            return Task.FromResult<IDiscordInteractionResponse>(response);
        }
    }
}