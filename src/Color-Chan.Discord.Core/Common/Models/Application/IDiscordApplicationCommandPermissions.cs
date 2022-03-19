using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Core.Common.Models.Application
{
    /// <summary>
    ///     Represents a discord Application Command Permissions Structure API model.
    ///     Docs: https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permissions-structure
    /// </summary>
    public interface IDiscordApplicationCommandPermissions
    {
        /// <summary>
        ///     The id of the role or user.
        /// </summary>
        ulong Id { get; set; }

        /// <summary>
        ///     The type of the <see cref="IDiscordApplicationCommandPermissions" />.
        /// </summary>
        DiscordApplicationCommandPermissionsType Type { get; set; }

        /// <summary>
        ///     true to allow, false, to disallow.
        /// </summary>
        bool Allow { get; set; }
    }
}