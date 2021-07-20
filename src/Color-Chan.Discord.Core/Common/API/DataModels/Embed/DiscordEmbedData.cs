using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    public record DiscordEmbedData
    {
        /// <summary>
        ///     Title of embed.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        /// <summary>
        ///     Type of embed (always "rich" for webhook embeds).
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; init; }

        /// <summary>
        ///     Description of embed.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        ///     Url of embed.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <summary>
        ///     Timestamp of embed content.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTimeOffset? Timestamp { get; init; }

        /// <summary>
        ///     Color code of the embed.
        /// </summary>
        [JsonPropertyName("color")]
        public uint? Color { get; init; }

        /// <summary>
        ///     Footer information.
        /// </summary>
        [JsonPropertyName("footer")]
        public DiscordEmbedFooterData? Footer { get; init; }

        /// <summary>
        ///     Image information.
        /// </summary>
        [JsonPropertyName("image")]
        public DiscordEmbedImageData? Image { get; init; }

        /// <summary>
        ///     Thumbnail information.
        /// </summary>
        [JsonPropertyName("thumbnail")]
        public DiscordEmbedThumbnailData? Thumbnail { get; init; }

        /// <summary>
        ///     Video information.
        /// </summary>
        [JsonPropertyName("video")]
        public DiscordEmbedVideoData? Video { get; init; }

        /// <summary>
        ///     Provider information.
        /// </summary>
        [JsonPropertyName("provider")]
        public DiscordEmbedProviderData? Provider { get; init; }

        /// <summary>
        ///     Author information.
        /// </summary>
        [JsonPropertyName("author")]
        public DiscordEmbedAuthorData? Author { get; init; }

        /// <summary>
        ///     Fields information.
        /// </summary>
        [JsonPropertyName("fields")]
        public IEnumerable<DiscordEmbedFieldData>? Fields { get; init; }
    }
}