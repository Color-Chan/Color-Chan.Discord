using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <inheritdoc cref="IDiscordGuildMember" />
public record DiscordGuildMemberData
{
    /// <inheritdoc cref="IDiscordGuildMember.User" />
    [JsonPropertyName("user")]
    public DiscordUserData? User { get; init; } = null!;

    /// <inheritdoc cref="IDiscordGuildMember.NickName" />
    [JsonPropertyName("nick")]
    public string? NickName { get; init; }

    /// <inheritdoc cref="IDiscordGuildMember.Roles" />
    [JsonPropertyName("roles")]
    public IEnumerable<ulong> Roles { get; init; } = new List<ulong>();

    /// <inheritdoc cref="IDiscordGuildMember.JoinedAt" />
    [JsonPropertyName("joined_at")]
    public DateTimeOffset JoinedAt { get; init; }

    /// <inheritdoc cref="IDiscordGuildMember.PremiumSince" />
    [JsonPropertyName("premium_since")]
    public DateTimeOffset? PremiumSince { get; init; }

    /// <inheritdoc cref="IDiscordGuildMember.Deaf" />
    [JsonPropertyName("deaf")]
    public bool Deaf { get; init; }

    /// <inheritdoc cref="IDiscordGuildMember.Mute" />
    [JsonPropertyName("mute")]
    public bool Mute { get; init; }

    /// <inheritdoc cref="IDiscordGuildMember.Pending" />
    [JsonPropertyName("pending")]
    public bool? Pending { get; init; }

    /// <inheritdoc cref="IDiscordGuildMember.Permissions" />
    [JsonPropertyName("permissions")]
    public DiscordPermission? Permissions { get; init; }

    /// <inheritdoc cref="IDiscordGuildMember.CommunicationDisabledUntil" />
    [JsonPropertyName("communication_disabled_until")]
    public DateTimeOffset? CommunicationDisabledUntil { get; set; }
}