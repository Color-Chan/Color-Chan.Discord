using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <inheritdoc cref="IDiscordReaction" />
public record DiscordReactionData
{
    /// <inheritdoc cref="IDiscordReaction.Count" />
    [JsonPropertyName("count")]
    public int Count { get; init; }

    /// <inheritdoc cref="IDiscordReaction.ByMe" />
    [JsonPropertyName("me")]
    public bool ByMe { get; init; }

    /// <inheritdoc cref="IDiscordReaction.Emoji" />
    [JsonPropertyName("emoji")]
    public DiscordEmojiData Emoji { get; init; } = null!;
}