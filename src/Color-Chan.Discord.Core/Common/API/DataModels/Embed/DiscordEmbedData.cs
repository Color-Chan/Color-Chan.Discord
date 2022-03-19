using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    /// <inheritdoc cref="IDiscordEmbed"/>
    public record DiscordEmbedData
    {
        /// <inheritdoc cref="IDiscordEmbed.Title"/>
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Type"/>
        [JsonPropertyName("type")]
        public DiscordEmbedType? Type { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Description"/>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Url"/>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Timestamp"/>
        [JsonPropertyName("timestamp")]
        public DateTimeOffset? Timestamp { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Color"/>
        [JsonPropertyName("color")]
        public Color? Color { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Footer"/>
        [JsonPropertyName("footer")]
        public DiscordEmbedFooterData? Footer { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Image"/>
        [JsonPropertyName("image")]
        public DiscordEmbedImageData? Image { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Thumbnail"/>
        [JsonPropertyName("thumbnail")]
        public DiscordEmbedThumbnailData? Thumbnail { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Video"/>
        [JsonPropertyName("video")]
        public DiscordEmbedVideoData? Video { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Provider"/>
        [JsonPropertyName("provider")]
        public DiscordEmbedProviderData? Provider { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Author"/>
        [JsonPropertyName("author")]
        public DiscordEmbedAuthorData? Author { get; init; }

        /// <inheritdoc cref="IDiscordEmbed.Fields"/>
        [JsonPropertyName("fields")]
        public IEnumerable<DiscordEmbedFieldData>? Fields { get; init; }
    }
}