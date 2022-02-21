using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    /// <inheritdoc cref="IDiscordEmbedThumbnail"/>
    public record DiscordEmbedThumbnailData
    {
        /// <inheritdoc cref="IDiscordEmbedThumbnail.Url"/>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <inheritdoc cref="IDiscordEmbedThumbnail.ProxyUrl"/>
        [JsonPropertyName("proxy_url")]
        public string? ProxyUrl { get; init; }

        /// <inheritdoc cref="IDiscordEmbedThumbnail.Height"/>
        [JsonPropertyName("height")]
        public int? Height { get; init; }

        /// <inheritdoc cref="IDiscordEmbedThumbnail.Width"/>
        [JsonPropertyName("width")]
        public int? Width { get; init; }
    }
}