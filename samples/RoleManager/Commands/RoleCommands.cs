using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Params.Guild;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace RoleManager.Commands
{
    /// <summary>
    ///     The command module for all role related commands.
    /// </summary>
    [SlashCommandGroup("role", "Gets, creates or updates roles.")]
    [SlashCommandRateLimit(10, 30)] // Sets the rate limit for this command module to 10 requests per 30 seconds per user.
    [SlashCommandRequireGuild] // Required all commands in this command module to be executed in a guild.
    public class RoleCommands : SlashCommandModule
    {
        private readonly IDiscordRestGuild _restGuild;

        /// <summary>
        ///     Initializes a new instance of <see cref="RoleCommands" />.
        /// </summary>
        /// <param name="restGuild">The <see cref="IDiscordRestGuild" /> that will make rest API calls to discord.</param>
        public RoleCommands(IDiscordRestGuild restGuild)
        {
            _restGuild = restGuild;
        }

        /// <summary>
        ///     Create a new role with a role name and with the default permissions.
        /// </summary>
        /// <param name="roleName">The name of the new role.</param>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse" /> with an embed containing the new role name.
        /// </returns>
        [SlashCommandGroup("add", "Adds a role.")]
        [SlashCommand("default", "Adds a role with the default permissions.")]
        public async Task<Result<IDiscordInteractionResponse>> AddDefaultRoleAsync
        (
            [SlashCommandOption("name", "The name of the new role.", true)]
            string roleName
        )
        {
            // Create the new role.
            var roleConfig = new DiscordCreateGuildRole
            {
                Name = roleName,
                Permissions = DiscordPermission.None
            };
            var newRoleResponse = await _restGuild.CreateGuildRoleAsync(SlashContext.GuildId!.Value, roleConfig).ConfigureAwait(false);

            // Check if the role was successfully created.
            if (!newRoleResponse.IsSuccessful)
            {
                return FromSuccess("Failed to create the role.", true);
            }

            //  Build the response embed.
            var embedBuilder = new DiscordEmbedBuilder()
                               .WithTitle("New role created!")
                               .WithDescription($"Role: {roleName} has been created.")
                               .WithColor(newRoleResponse.Entity!.Color)
                               .WithTimeStamp();
            
            //  Return the response to Discord.
            return FromSuccess(embedBuilder.Build());
        }

        /// <summary>
        ///     Deletes a role.
        /// </summary>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse" /> with an embed containing whether or not the role has been deleted.
        /// </returns>
        [SlashCommand("delete", "Deletes a role!")]
        public async Task<Result<IDiscordInteractionResponse>> DeleteRoleAsync
        (
            [SlashCommandOption("role", "The role that will be deleted.", true, DiscordApplicationCommandOptionType.Role)]
            ulong roleId
        )
        {
            // Get the role object.
            var role = SlashContext.CommandRequest.Resolved?.Roles?.FirstOrDefault(x => x.Key == roleId).Value;

            // Delete the role.
            var auditLog = $"User: {SlashContext.Member?.User?.Username} requested this action";
            var deleteResponse = await _restGuild.DeleteGuildRoleAsync(SlashContext.GuildId!.Value, roleId, auditLog).ConfigureAwait(false);

            // Check if the role were successfully deleted.
            if (!deleteResponse.IsSuccessful)
            {
                return FromSuccess("Failed to delete the role.", true);
            }

            //  Build the response embed.
            var embedBuilder = new DiscordEmbedBuilder()
                               .WithTitle($"Role {role?.Name} deleted")
                               .WithDescription("The role has been successfully deleted!")
                               .WithColor(role?.Color ?? Color.FromArgb(0))
                               .WithTimeStamp();
            
            //  Return the response to Discord.
            return FromSuccess(embedBuilder.Build());
        }

        /// <summary>
        ///     Get a list of roles and send them back to the user.
        /// </summary>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse" /> with an embed containing the role names.
        /// </returns>
        [SlashCommand("lists", "Get a neat little list with all the roles!")]
        public async Task<Result<IDiscordInteractionResponse>> ListRolesAsync()
        {
            // Get the roles from the current guild.
            var rolesResult = await _restGuild.GetGuildRolesAsync(SlashContext.GuildId!.Value).ConfigureAwait(false);

            // Check if the roles were successfully loaded.
            if (!rolesResult.IsSuccessful)
            {
                return FromSuccess("Failed to get guild roles.", true);
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
            
            //  Return the response to Discord.
            return FromSuccess(embedBuilder.Build());
        }
    }
}