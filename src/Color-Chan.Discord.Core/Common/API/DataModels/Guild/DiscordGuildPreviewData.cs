using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <inheritdoc cref="IDiscordGuildPreview" />
public class DiscordGuildPreviewData
{
    /// <inheritdoc cref="IDiscordGuildPreview.Id" />
    [JsonPropertyName("id")]
    public ulong Id { get; init; }

    /// <inheritdoc cref="IDiscordGuildPreview.Name" />
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    /// <inheritdoc cref="IDiscordGuildPreview.Icon" />
    [JsonPropertyName("icon")]
    public string? Icon { get; init; }

    /// <inheritdoc cref="IDiscordGuildPreview.Splash" />
    [JsonPropertyName("splash")]
    public string? Splash { get; init; }

    /// <inheritdoc cref="IDiscordGuildPreview.DiscoverySplash" />
    [JsonPropertyName("discovery_splash")]
    public string? DiscoverySplash { get; init; }

    /// <inheritdoc cref="IDiscordGuildPreview.Roles" />
    [JsonPropertyName("roles")]
    public IEnumerable<DiscordGuildRoleData> Roles { get; init; } = new List<DiscordGuildRoleData>();

    /// <inheritdoc cref="IDiscordGuildPreview.Emojis" />
    [JsonPropertyName("emojis")]
    public IEnumerable<DiscordEmojiData> Emojis { get; init; } = new List<DiscordEmojiData>();

    /// <inheritdoc cref="IDiscordGuildPreview.ApproximateMemberCount" />
    [JsonPropertyName("approximate_member_count")]
    public int? ApproximateMemberCount { get; init; }

    /// <inheritdoc cref="IDiscordGuildPreview.ApproximatePresenceCount" />
    [JsonPropertyName("approximate_presence_count")]
    public int? ApproximatePresenceCount { get; init; }

    /// <inheritdoc cref="IDiscordGuildPreview.Description" />
    [JsonPropertyName("description")]
    public string? Description { get; init; }
}