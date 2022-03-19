namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <summary>
///     Represents a discord Guild Scheduled Event Entity type API model.
///     Docs: https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-status
/// </summary>
public enum DiscordGuildEventEntityType
{
    /// <summary>
    ///     A Stage guild event.
    /// </summary>
    StageInstance = 1,
    
    /// <summary>
    ///     A voice chat guild event.
    /// </summary>
    Voice = 2,
    
    /// <summary>
    ///     An external guild event.
    /// </summary>
    External = 3
}