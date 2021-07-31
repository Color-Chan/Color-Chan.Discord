using System.Drawing;
using System.Threading.Tasks;
using Color_Chan.Discord.Builders;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Pong.Commands
{
    public class PongCommands : SlashCommandModule
    {
        [SlashCommand("ping", "Ping Pong!")]
        public Task<IDiscordInteractionResponse> PongAsync()
        {
            var embedBuilder = new DiscordEmbedBuilder()
                               .WithTitle("Ping response")
                               .WithDescription("Pong!")
                               .WithColor(Color.Aqua)
                               .WithTimeStamp();

            var response = new SlashCommandResponseBuilder()
                           .WithEmbed(embedBuilder.Build())
                           .MakePrivate()
                           .Build();

            return Task.FromResult(response);
        }
    }
}