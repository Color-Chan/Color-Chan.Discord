using System;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models.Invites;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Invite;

/// <inheritdoc cref="IDiscordInvite"/>
public record DiscordInviteData
{
    /// <inheritdoc cref="IDiscordInvite.Code"/>
    [JsonPropertyName("code")]
    public string Code { get; set; } = null!;
    
    /// <inheritdoc cref="IDiscordInvite.PartialGuild"/>
    [JsonPropertyName("guild")]
    public DiscordPartialGuildData? PartialGuild { get; set; }

    /// <inheritdoc cref="IDiscordInvite.PartialChannel"/>
    [JsonPropertyName("channel")]
    public DiscordPartialChannelData? PartialChannel { get; set; }

    /// <inheritdoc cref="IDiscordInvite.Inviter"/>
    [JsonPropertyName("inviter")]
    public DiscordUserData? Inviter { get; set; }

    /// <inheritdoc cref="IDiscordInvite.TargetType"/>
    [JsonPropertyName("target_type")]
    public DiscordInviteTargetType? TargetType { get; set; }

    /// <inheritdoc cref="IDiscordInvite.TargetUser"/>
    [JsonPropertyName("target_user")]
    public DiscordUserData? TargetUser { get; set; }

    /// <inheritdoc cref="IDiscordInvite.TargetPartialApplication"/>
    [JsonPropertyName("target_application")]
    public DiscordPartialApplicationData? TargetPartialApplication { get; set; }

    /// <inheritdoc cref="IDiscordInvite.ApproximatePresenceCount"/>
    [JsonPropertyName("approximate_presence_count")]
    public int? ApproximatePresenceCount { get; set; }

    /// <inheritdoc cref="IDiscordInvite.ApproximateMemberCount"/>
    [JsonPropertyName("approximate_member_count")]
    public int? ApproximateMemberCount { get; set; }

    /// <inheritdoc cref="IDiscordInvite.ExpiresAt"/>
    [JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }
    
    /// <inheritdoc cref="IDiscordInvite.GuildScheduledEvent"/>
    [JsonPropertyName("guild_scheduled_event")]
    public DiscordGuildScheduledEventData? GuildScheduledEvent { get; set; }
}