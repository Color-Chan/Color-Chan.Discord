using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Message;

/// <inheritdoc cref="IDiscordMessageReference" />
public record DiscordMessageReferenceData
{
    /// <inheritdoc cref="IDiscordMessageReference.MessageId" />
    [JsonPropertyName("message_id")]
    public ulong? MessageId { get; init; }

    /// <inheritdoc cref="IDiscordMessageReference.ChannelId" />
    [JsonPropertyName("channel_id")]
    public ulong? ChannelId { get; init; }

    /// <inheritdoc cref="IDiscordMessageReference.GuildId" />
    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; init; }

    /// <inheritdoc cref="IDiscordMessageReference.FailIfNotExists" />
    [JsonPropertyName("fail_if_not_exists")]
    public ulong? FailIfNotExists { get; init; }
}