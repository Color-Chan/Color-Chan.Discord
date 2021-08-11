using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    /// <summary>
    ///     Represents a discord Welcome Screen Channel Structure API model.
    ///     https://discord.com/developers/docs/resources/guild#welcome-screen-object-welcome-screen-channel-structure
    /// </summary>
    public record DiscordGuildWelcomeChannelData
    {
        /// <summary>
        ///     The channel's id.
        /// </summary>
        [JsonPropertyName("channel_id")]
        public ulong ChannelId { get; init; }

        /// <summary>
        ///     The description shown for the channel
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        ///     The emoji id, if the emoji is custom.
        /// </summary>
        [JsonPropertyName("emoji_id")]
        public string? EmojiId { get; init; }

        /// <summary>
        ///     The emoji name if custom, the unicode character if standard, or null if no emoji is set.
        /// </summary>
        [JsonPropertyName("emoji_name")]
        public string? EmojiName { get; init; }
    }
}