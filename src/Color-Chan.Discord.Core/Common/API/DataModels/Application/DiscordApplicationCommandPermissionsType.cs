namespace Color_Chan.Discord.Core.Common.API.DataModels.Application;

/// <summary>
///     Represents a discord Application Command Permissions Type API model.
///     Docs:
///     https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permission-type
/// </summary>
public enum DiscordApplicationCommandPermissionsType
{
    /// <summary>
    ///     Requires a specific role.
    /// </summary>
    Role = 1,

    /// <summary>
    ///     Requires a specific user.
    /// </summary>
    User = 2
}