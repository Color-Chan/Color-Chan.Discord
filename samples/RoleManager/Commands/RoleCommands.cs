using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Builders;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace RoleManager.Commands
{
    /// <summary>
    ///     The command module for all role related commands.
    /// </summary>
    public class RoleCommands : SlashCommandModule
    {
        private readonly IDiscordRestGuild _restGuild;

        /// <summary>
        ///     Initializes a new instance of <see cref="RoleCommands" />.
        /// </summary>
        /// <param name="restGuild">The <see cref="IDiscordRestGuild"/> that will make rest API calls to discord.</param>
        public RoleCommands(IDiscordRestGuild restGuild)
        {
            _restGuild = restGuild;
        }
        
        /// <summary>
        ///     Get a list of roles and send them back to the user.
        /// </summary>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse"/> with an embed containing the role names.
        /// </returns>
        [SlashCommand("roles", "Get a neat little list with all the roles!")]
        public async Task<IDiscordInteractionResponse> ListRolesAsync()
        {
            var guildId = SlashContext.GuildId;

            // Check if the guild id is null, since this could be null if it was used in DMs.
            if (guildId is null)
            {
                //  Build the response embed.
                var errorEmbedBuilder = new DiscordEmbedBuilder()
                                   .WithTitle("Error")
                                   .WithDescription("This command can only be used in a server!")
                                   .WithColor(Color.Red)
                                   .WithTimeStamp();
            
                // Build the response with the embed.
                var errorResponse = new SlashCommandResponseBuilder()
                               .WithEmbed(errorEmbedBuilder.Build())
                               .MakePrivate()
                               .Build();

                //  Return the response to Discord.
                return errorResponse;
            }
            
            // Get the roles from the current guild.
            var rolesResult = await _restGuild.GetGuildRolesAsync(guildId.Value).ConfigureAwait(false);

            // Check if the roles were successfully loaded.
            if (!rolesResult.IsSuccessful)
            {
                var errorResponse = new SlashCommandResponseBuilder()
                                    .WithContent("Failed to get guild roles.")
                                    .MakePrivate()
                                    .Build();

                return errorResponse;
            }
            
            // Build the response.
            var roleNames = rolesResult.Entity!.Select(x => x.Name);
            var description = string.Join(", ", roleNames);

            //  Build the response embed.
            var embedBuilder = new DiscordEmbedBuilder()
                               .WithTitle("Roles")
                               .WithDescription(description)
                               .WithColor(Color.HotPink)
                               .WithTimeStamp();
            
            // Build the response with the embed.
            var response = new SlashCommandResponseBuilder()
                           .WithEmbed(embedBuilder.Build())
                           .MakePrivate()
                           .Build();

            //  Return the response to Discord.
            return response;
        }
    }
}