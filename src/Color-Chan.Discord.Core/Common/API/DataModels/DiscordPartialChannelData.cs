using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <inheritdoc cref="IDiscordPartialChannel"/>
public record DiscordPartialChannelData
{
    // Shared
    /// <inheritdoc cref="IDiscordPartialChannel.Id"/>
    [JsonPropertyName("id")]
    public ulong? Id { get; init; }

    /// <inheritdoc cref="System.Type"/>
    [JsonPropertyName("type")]
    public DiscordChannelType? Type { get; init; }

    /// <inheritdoc cref="IDiscordPartialChannel.LastMessageId"/>
    [JsonPropertyName("last_message_id")]
    public ulong? LastMessageId { get; init; }

    //GuildChannel
    /// <inheritdoc cref="IDiscordPartialChannel.GuildId"/>
    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; init; }

    /// <inheritdoc cref="IDiscordPartialChannel.Name"/>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <inheritdoc cref="IDiscordPartialChannel.Position"/>
    [JsonPropertyName("position")]
    public int? Position { get; init; }

    /// <inheritdoc cref="IDiscordPartialChannel.PermissionOverwrites"/>
    [JsonPropertyName("permission_overwrites")]

    public IEnumerable<DiscordOverwriteData>? PermissionOverwrites { get; init; }

    /// <inheritdoc cref="IDiscordPartialChannel.CategoryId"/>
    [JsonPropertyName("parent_id")]
    public ulong? CategoryId { get; init; }

    //TextChannel
    /// <inheritdoc cref="IDiscordPartialChannel.Topic"/>
    [JsonPropertyName("topic")]
    public string? Topic { get; init; }

    /// <inheritdoc cref="IDiscordPartialChannel.LastPinTimestamp"/>
    [JsonPropertyName("last_pin_timestamp")]
    public DateTimeOffset? LastPinTimestamp { get; init; }

    /// <inheritdoc cref="IDiscordPartialChannel.Nsfw"/>
    [JsonPropertyName("nsfw")]
    public bool? Nsfw { get; init; }

    /// <inheritdoc cref="IDiscordPartialChannel.SlowMode"/>
    [JsonPropertyName("rate_limit_per_user")]
    public int? SlowMode { get; init; }

    //VoiceChannel
    /// <inheritdoc cref="IDiscordPartialChannel.Bitrate"/>
    [JsonPropertyName("bitrate")]
    public int? Bitrate { get; init; }

    /// <inheritdoc cref="IDiscordPartialChannel.UserLimit"/>
    [JsonPropertyName("user_limit")]
    public int? UserLimit { get; init; }

    //PrivateChannel
    /// <inheritdoc cref="IDiscordPartialChannel.Recipients"/>
    [JsonPropertyName("recipients")]
    public IEnumerable<DiscordUserData>? Recipients { get; init; }

    //GroupChannel
    /// <inheritdoc cref="IDiscordPartialChannel.Icon"/>
    [JsonPropertyName("icon")]
    public string? Icon { get; init; }
}