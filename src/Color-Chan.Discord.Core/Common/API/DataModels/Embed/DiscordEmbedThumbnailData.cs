﻿using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    public record DiscordEmbedThumbnailData
    {
        /// <summary>
        ///     Source url of thumbnail (only supports http(s) and attachments).
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <summary>
        ///     A proxied url of the image.
        /// </summary>
        [JsonPropertyName("proxy_url")]
        public string? ProxyUrl { get; init; }

        /// <summary>
        ///     Height of image.
        /// </summary>
        [JsonPropertyName("height")]
        public int? Height { get; init; }

        /// <summary>
        ///     Width of image.
        /// </summary>
        [JsonPropertyName("width")]
        public int? Width { get; init; }
    }
}