using System.Drawing;
using System.Threading.Tasks;
using Color_Chan.Discord.Builders;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Pong.Commands
{
    /// <summary>
    ///     The command module for all pong commands.
    /// </summary>
    public class PongCommands : SlashCommandModule
    {
        /// <summary>
        ///     A simple Ping Pong command.
        /// </summary>
        /// <returns>
        ///     An embedded response with "Pong!".
        /// </returns>
        [SlashCommand("ping", "Ping Pong!")]
        public Task<IDiscordInteractionResponse> PongAsync()
        {
            //  Build the response embed.
            var embedBuilder = new DiscordEmbedBuilder()
                               .WithTitle("Ping response")
                               .WithDescription("Pong!")
                               .WithColor(Color.Aqua)
                               .WithTimeStamp();

            // Build the response with the embed.
            var response = new SlashCommandResponseBuilder()
                           .WithEmbed(embedBuilder.Build())
                           .MakePrivate()
                           .Build();

            //  Return the response to Discord.
            return Task.FromResult(response);
        }
    }
}