using System;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Guild;

/// <inheritdoc />
public class DiscordGuildScheduledEvent : IDiscordGuildScheduledEvent
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordGuildScheduledEvent"/>
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordGuildScheduledEvent"/>.</param>
    public DiscordGuildScheduledEvent(DiscordGuildScheduledEventData data)
    {
        Id = data.Id;
        GuildId = data.GuildId;
        ChannelId = data.ChannelId;
        CreatorId = data.CreatorId;
        Name = data.Name;
        Description = data.Description;
        ScheduledStartTime = data.ScheduledStartTime;
        ScheduledEndTime = data.ScheduledEndTime;
        PrivacyLevel = data.PrivacyLevel;
        Status = data.Status;
        EntityType = data.EntityType;
        EntityId = data.EntityId;
        EntityMetadata = data.EntityMetadata is not null ? new DiscordGuildEventEntityMetadata(data.EntityMetadata) : null;
        Creator = data.Creator;
        UserCount = data.UserCount;
        Image = data.Image;
    }
    
    /// <inheritdoc />
    public ulong Id { get; set; }
    
    /// <inheritdoc />
    public ulong GuildId { get; set; }
    
    /// <inheritdoc />
    public ulong? ChannelId { get; set; }
    
    /// <inheritdoc />
    public ulong? CreatorId { get; set; }
    
    /// <inheritdoc />
    public string Name { get; set; }
    
    /// <inheritdoc />
    public string? Description { get; set; }
    
    /// <inheritdoc />
    public DateTimeOffset ScheduledStartTime { get; set; }
    
    /// <inheritdoc />
    public DateTimeOffset? ScheduledEndTime { get; set; }
    
    /// <inheritdoc />
    public DiscordGuildEventPrivacyLevel PrivacyLevel { get; set; }
    
    /// <inheritdoc />
    public DiscordGuildEventStatus Status { get; set; }
    
    /// <inheritdoc />
    public DiscordGuildEventEntityType EntityType { get; set; }
    
    /// <inheritdoc />
    public ulong? EntityId { get; set; }
    
    /// <inheritdoc />
    public IDiscordGuildEventEntityMetadata? EntityMetadata { get; set; }
    
    /// <inheritdoc />
    public DiscordUserData? Creator { get; set; }
    
    /// <inheritdoc />
    public int? UserCount { get; set; }
    
    /// <inheritdoc />
    public string? Image { get; set; }
}