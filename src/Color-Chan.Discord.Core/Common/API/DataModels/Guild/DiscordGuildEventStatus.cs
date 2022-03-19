namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <summary>
///     Represents a discord Guild Scheduled Event Status API model.
///     Docs: https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-status
/// </summary>
public enum DiscordGuildEventStatus
{
    /// <summary>
    ///     The guild event has been scheduled.
    /// </summary>
    Scheduled = 1,
    
    /// <summary>
    ///     The guild even is active.
    /// </summary>
    Active = 2,
    
    /// <summary>
    ///     The Guild even has been completed.
    /// </summary>
    /// <remarks>
    ///     Once status is set to COMPLETED or CANCELED, the status can no longer be updated.
    /// </remarks>
    Completed = 3,
    
    /// <summary>
    ///     The Guild even has been canceled.
    /// </summary>
    /// <remarks>
    ///     Once status is set to COMPLETED or CANCELED, the status can no longer be updated.
    /// </remarks>
    Canceled = 4
}