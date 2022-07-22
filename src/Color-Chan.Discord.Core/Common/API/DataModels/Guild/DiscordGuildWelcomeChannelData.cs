using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <inheritdoc cref="IDiscordGuildWelcomeChannel" />
public record DiscordGuildWelcomeChannelData
{
    /// <inheritdoc cref="IDiscordGuildWelcomeChannel.ChannelId" />
    [JsonPropertyName("channel_id")]
    public ulong ChannelId { get; init; }

    /// <inheritdoc cref="IDiscordGuildWelcomeChannel.Description" />
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <inheritdoc cref="IDiscordGuildWelcomeChannel.EmojiId" />
    [JsonPropertyName("emoji_id")]
    public string? EmojiId { get; init; }

    /// <inheritdoc cref="IDiscordGuildWelcomeChannel.EmojiName" />
    [JsonPropertyName("emoji_name")]
    public string? EmojiName { get; init; }
}