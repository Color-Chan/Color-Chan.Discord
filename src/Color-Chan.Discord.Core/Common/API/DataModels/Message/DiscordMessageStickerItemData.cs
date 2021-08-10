using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Message
{
    public record DiscordMessageStickerItemData
    {
        /// <summary>
        ///     The id of the sticker.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <summary>
        ///     Name of the sticker.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <summary>
        ///     Type of sticker format.
        /// </summary>
        [JsonPropertyName("format_type")]
        public ulong DiscordMessageStickerItemType { get; init; }
    }
}