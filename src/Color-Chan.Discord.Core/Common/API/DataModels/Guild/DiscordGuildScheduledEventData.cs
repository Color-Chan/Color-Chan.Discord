using System;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <inheritdoc cref="IDiscordGuildScheduledEvent"/>
public class DiscordGuildScheduledEventData
{
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.Id"/>
    [JsonPropertyName("id")]
    public ulong Id { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.GuildId"/>
    [JsonPropertyName("guild_id")]
    public ulong GuildId { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.ChannelId"/>
    [JsonPropertyName("channel_id")]
    public ulong? ChannelId { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.CreatorId"/>
    [JsonPropertyName("creator_id")]
    public ulong? CreatorId { get; set; }

    /// <inheritdoc cref="IDiscordGuildScheduledEvent.Name"/>
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.Description"/>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.ScheduledStartTime"/>
    [JsonPropertyName("scheduled_start_time")]
    public DateTimeOffset ScheduledStartTime { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.ScheduledEndTime"/>
    [JsonPropertyName("scheduled_end_time")]
    public DateTimeOffset? ScheduledEndTime { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.PrivacyLevel"/>
    [JsonPropertyName("privacy_level")]
    public DiscordGuildEventPrivacyLevel PrivacyLevel { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.Status"/>
    [JsonPropertyName("status")]
    public DiscordGuildEventStatus Status { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.EntityType"/>
    [JsonPropertyName("entity_type")]
    public DiscordGuildEventEntityType EntityType { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.EntityId"/>
    [JsonPropertyName("entity_id")]
    public ulong? EntityId { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.EntityMetadata"/>
    [JsonPropertyName("entity_metadata")]
    public DiscordGuildEventEntityMetadataData? EntityMetadata { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.Creator"/>
    [JsonPropertyName("creator")]
    public DiscordUserData? Creator { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.UserCount"/>
    [JsonPropertyName("user_count")]
    public int? UserCount { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildScheduledEvent.Image"/>
    [JsonPropertyName("image")]
    public string? Image { get; set; }
}