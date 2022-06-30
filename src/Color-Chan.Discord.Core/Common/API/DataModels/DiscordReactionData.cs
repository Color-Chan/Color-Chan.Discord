using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

public record DiscordReactionData
{
    /// <summary>
    ///     Times this emoji has been used to react.
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; init; }

    /// <summary>
    ///     Whether the current user reacted using this emoji.
    /// </summary>
    [JsonPropertyName("me")]
    public bool ByMe { get; init; }

    /// <summary>
    ///     Emoji information.
    /// </summary>
    [JsonPropertyName("emoji")]
    public DiscordEmojiData Emoji { get; init; } = null!;
}