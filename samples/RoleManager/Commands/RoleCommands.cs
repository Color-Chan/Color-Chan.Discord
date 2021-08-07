using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Params.Guild;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace RoleManager.Commands
{
    /// <summary>
    ///     The command module for all role related commands.
    /// </summary>
    [SlashCommandGroup("roles", "Gets, creates or updates roles.")]
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
        public async Task<IDiscordInteractionResponse> AddDefaultRoleAsync
        (
            [SlashCommandOption("name", "The name of the new role.", true)]
            string roleName
        )
        {
            var guildId = SlashContext.GuildId;

            // Send an error message if the command was used in DMs.
            var dmErrorResponse = CheckIfGuildIdExists();
            if (dmErrorResponse is not null) return dmErrorResponse;

            // Create the new role.
            var roleConfig = new DiscordCreateGuildRole
            {
                Name = roleName,
                Permissions = DiscordGuildPermission.None
            };
            var newRoleResponse = await _restGuild.CreateGuildRoleAsync(guildId!.Value, roleConfig).ConfigureAwait(false);

            // Check if the role was successfully created.
            if (!newRoleResponse.IsSuccessful)
            {
                var errorResponse = new SlashCommandResponseBuilder()
                                    .WithContent("Failed to create the role.")
                                    .MakePrivate()
                                    .Build();

                return errorResponse;
            }

            //  Build the response embed.
            var embedBuilder = new DiscordEmbedBuilder()
                               .WithTitle("New role created!")
                               .WithDescription($"Role: {roleName} has been created.")
                               .WithColor(newRoleResponse.Entity!.Color)
                               .WithTimeStamp();

            // Build the response with the embed.
            var response = new SlashCommandResponseBuilder()
                           .WithEmbed(embedBuilder.Build())
                           .Build();

            //  Return the response to Discord.
            return response;
        }

        /// <summary>
        ///     Deletes a role.
        /// </summary>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse" /> with an embed containing whether or not the role has been deleted.
        /// </returns>
        [SlashCommand("delete", "Deletes a role!")]
        public async Task<IDiscordInteractionResponse> DeleteRoleAsync
        (
            [SlashCommandOption("role", "The role that will be deleted.", true, DiscordApplicationCommandOptionType.Role)]
            ulong roleId
        )
        {
            var guildId = SlashContext.GuildId;

            // Send an error message if the command was used in DMs.
            var dmErrorResponse = CheckIfGuildIdExists();
            if (dmErrorResponse is not null) return dmErrorResponse;

            // Get the role object.
            var role = SlashContext.CommandRequest.Resolved?.Roles?.FirstOrDefault(x => x.Key == roleId).Value;

            // Delete the role.
            var auditLog = $"User: {SlashContext.Member?.User?.Username} requested this action";
            var deleteResponse = await _restGuild.DeleteGuildRoleAsync(guildId!.Value, roleId, auditLog).ConfigureAwait(false);

            // Check if the role were successfully deleted.
            if (!deleteResponse.IsSuccessful)
            {
                var errorResponse = new SlashCommandResponseBuilder()
                                    .WithContent("Failed to delete the role.")
                                    .MakePrivate()
                                    .Build();

                return errorResponse;
            }

            //  Build the response embed.
            var embedBuilder = new DiscordEmbedBuilder()
                               .WithTitle($"Role {role?.Name} deleted")
                               .WithDescription("The role has been successfully deleted!")
                               .WithColor(role?.Color ?? Color.FromArgb(0))
                               .WithTimeStamp();

            // Build the response with the embed.
            var response = new SlashCommandResponseBuilder()
                           .WithEmbed(embedBuilder.Build())
                           .Build();

            //  Return the response to Discord.
            return response;
        }

        /// <summary>
        ///     Get a list of roles and send them back to the user.
        /// </summary>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse" /> with an embed containing the role names.
        /// </returns>
        [SlashCommand("lists", "Get a neat little list with all the roles!")]
        public async Task<IDiscordInteractionResponse> ListRolesAsync()
        {
            var guildId = SlashContext.GuildId;

            // Send an error message if the command was used in DMs.
            var dmErrorResponse = CheckIfGuildIdExists();
            if (dmErrorResponse is not null) return dmErrorResponse;

            // Get the roles from the current guild.
            var rolesResult = await _restGuild.GetGuildRolesAsync(guildId!.Value).ConfigureAwait(false);

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
                           .Build();

            //  Return the response to Discord.
            return response;
        }

        /// <summary>
        ///     Checks if the guild id is null, since this could be null if it was used in DMs.
        /// </summary>
        /// <returns>
        ///     True if the commands was used in a guild.
        /// </returns>
        private bool RequestIsFromGuild()
        {
            return SlashContext.GuildId is not null;
        }

        /// <summary>
        ///     Checks if the command was used in a guild.
        /// </summary>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse" /> if the commands was used in DMs.
        ///     null if the commands was used in a guild.
        /// </returns>
        private IDiscordInteractionResponse? CheckIfGuildIdExists()
        {
            if (RequestIsFromGuild()) return null;

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
    }
}