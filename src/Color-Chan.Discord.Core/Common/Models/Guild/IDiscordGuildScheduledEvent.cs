using System;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.Models.Guild;

/// <summary>
///     Represents a discord Guild Scheduled Event Structure API model.
///     Docs: https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-structure
/// </summary>
public interface IDiscordGuildScheduledEvent
{
    /// <summary>
    ///     The id of the scheduled event.
    /// </summary>
    ulong Id { get; set; }
    
    /// <summary>
    ///     The guild id which the scheduled event belongs to.
    /// </summary>
    ulong GuildId { get; set; }
    
    /// <summary>
    ///     The channel id in which the scheduled event will be hosted, or null if scheduled entity type is EXTERNAL.
    /// </summary>
    /// <remarks>
    ///     See field requirements by entity type to understand the relationship between
    ///     <see cref="EntityType"/> and the following fields: <see cref="ChannelId"/>, <see cref="EntityMetadata"/>, and <see cref="ScheduledEndTime"/>
    /// </remarks>
    ulong? ChannelId { get; set; }
    
    /// <summary>
    ///     The id of the user that created the scheduled event.
    /// </summary>
    /// <remarks>
    ///     <see cref="CreatorId"/> will be null and creator will not be included for
    ///     events created before October 25th, 2021, when the concept of <see cref="CreatorId"/> was introduced and tracked.
    /// </remarks>
    ulong? CreatorId { get; set; }

    /// <summary>
    ///     The name of the scheduled event (1-100 characters).
    /// </summary>
    string Name { get; set; }
    
    /// <summary>
    ///     The description of the scheduled event (1-1000 characters).
    /// </summary>
    string? Description { get; set; }
    
    /// <summary>
    ///     The time the scheduled event will start.
    /// </summary>
    DateTimeOffset ScheduledStartTime { get; set; }
    
    /// <summary>
    ///     The time the scheduled event will end, required if entity_type is EXTERNAL.
    /// </summary>
    /// <remarks>
    ///     See field requirements by entity type to understand the relationship between
    ///     <see cref="EntityType"/> and the following fields: <see cref="ChannelId"/>, <see cref="EntityMetadata"/>, and <see cref="ScheduledEndTime"/>
    /// </remarks>
    DateTimeOffset? ScheduledEndTime { get; set; }
    
    /// <summary>
    ///     The privacy level of the scheduled event.
    /// </summary>
    DiscordGuildEventPrivacyLevel PrivacyLevel { get; set; }
    
    /// <summary>
    ///     The status of the scheduled event.
    /// </summary>
    DiscordGuildEventStatus Status { get; set; }
    
    /// <summary>
    ///     The type of the scheduled event.
    /// </summary>
    DiscordGuildEventEntityType EntityType { get; set; }
    
    /// <summary>
    ///     The id of an entity associated with a guild scheduled event.
    /// </summary>
    ulong? EntityId { get; set; }
    
    /// <summary>
    ///     Additional metadata for the guild scheduled event.
    /// </summary>
    /// <remarks>
    ///     See field requirements by entity type to understand the relationship between
    ///     <see cref="EntityType"/> and the following fields: <see cref="ChannelId"/>, <see cref="EntityMetadata"/>, and <see cref="ScheduledEndTime"/>
    /// </remarks>
    IDiscordGuildEventEntityMetadata? EntityMetadata { get; set; }
    
    /// <summary>
    ///     The user that created the scheduled event.
    /// </summary>
    DiscordUserData? Creator { get; set; }
    
    /// <summary>
    ///     The number of users subscribed to the scheduled event.
    /// </summary>
    int? UserCount { get; set; }
    
    /// <summary>
    ///     The cover image hash of the scheduled event.
    /// </summary>
    string? Image { get; set; }
}