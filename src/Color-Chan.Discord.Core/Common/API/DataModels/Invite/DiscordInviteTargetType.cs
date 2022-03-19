namespace Color_Chan.Discord.Core.Common.API.DataModels.Invite;

/// <summary>
///     Represents a discord Invite Target Type API model.
///     Docs: https://discord.com/developers/docs/resources/invite#invite-object-invite-target-types
/// </summary>
public enum DiscordInviteTargetType
{
    /// <summary>
    ///     A Stream invite target.
    /// </summary>
    Stream = 1,
    
    /// <summary>
    ///     An Embedded application invite target type.
    /// </summary>
    EmbeddedApplication = 2
}