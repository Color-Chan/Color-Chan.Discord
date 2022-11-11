using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <inheritdoc cref="IDiscordChannel" />
public record DiscordChannelData
{
    // Shared
    /// <inheritdoc cref="IDiscordChannel.Id" />
    [JsonPropertyName("id")]
    public ulong Id { get; init; }

    /// <inheritdoc cref="IDiscordChannel.Type" />
    [JsonPropertyName("type")]
    public DiscordChannelType Type { get; init; }

    /// <inheritdoc cref="IDiscordChannel.LastMessageId" />
    [JsonPropertyName("last_message_id")]
    public ulong? LastMessageId { get; init; }

    //GuildChannel
    /// <inheritdoc cref="IDiscordChannel.GuildId" />
    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; init; }

    /// <inheritdoc cref="IDiscordChannel.Name" />
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <inheritdoc cref="IDiscordChannel.Position" />
    [JsonPropertyName("position")]
    public int? Position { get; init; }

    /// <inheritdoc cref="IDiscordChannel.PermissionOverwrites" />
    [JsonPropertyName("permission_overwrites")]

    public IEnumerable<DiscordOverwriteData>? PermissionOverwrites { get; init; }

    /// <inheritdoc cref="IDiscordChannel.CategoryId" />
    [JsonPropertyName("parent_id")]
    public ulong? CategoryId { get; init; }

    //TextChannel
    /// <inheritdoc cref="IDiscordChannel.Topic" />
    [JsonPropertyName("topic")]
    public string? Topic { get; init; }

    /// <inheritdoc cref="IDiscordChannel.LastPinTimestamp" />
    [JsonPropertyName("last_pin_timestamp")]

    public DateTimeOffset? LastPinTimestamp { get; init; }

    /// <inheritdoc cref="IDiscordChannel.Nsfw" />
    [JsonPropertyName("nsfw")]
    public bool? Nsfw { get; init; }

    /// <inheritdoc cref="IDiscordChannel.SlowMode" />
    [JsonPropertyName("rate_limit_per_user")]

    public int? SlowMode { get; init; }

    //VoiceChannel
    /// <inheritdoc cref="IDiscordChannel.Bitrate" />
    [JsonPropertyName("bitrate")]
    public int? Bitrate { get; init; }

    /// <inheritdoc cref="IDiscordChannel.UserLimit" />
    [JsonPropertyName("user_limit")]
    public int? UserLimit { get; init; }

    //PrivateChannel
    /// <inheritdoc cref="IDiscordChannel.Recipients" />
    [JsonPropertyName("recipients")]
    public IEnumerable<DiscordUserData>? Recipients { get; init; }

    //GroupChannel
    /// <inheritdoc cref="IDiscordChannel.Icon" />
    [JsonPropertyName("icon")]
    public string? Icon { get; init; }
}