using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <summary>
    ///     Represents a discord Reaction Structure API model.
    ///     Docs: https://discord.com/developers/docs/resources/channel#reaction-object-reaction-structure
    /// </summary>
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
}