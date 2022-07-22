namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <summary>
///     Represents a discord Application Command Permission Type API model.
///     Docs:
///     https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permission-type
/// </summary>
public enum DiscordPermissionTargetType
{
    /// <summary>
    ///     The target of the permission is a role.
    /// </summary>
    Role = 0,

    /// <summary>
    ///     The target of the permission is a user.
    /// </summary>
    User = 1
}