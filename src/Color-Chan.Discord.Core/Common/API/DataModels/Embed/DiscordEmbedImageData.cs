using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    /// <inheritdoc cref="IDiscordEmbedImage"/>
    public record DiscordEmbedImageData
    {
        /// <inheritdoc cref="IDiscordEmbedImage.Url"/>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <inheritdoc cref="IDiscordEmbedImage.ProxyUrl"/>
        [JsonPropertyName("proxy_url")]
        public string? ProxyUrl { get; init; }

        /// <inheritdoc cref="IDiscordEmbedImage.Height"/>
        [JsonPropertyName("height")]
        public int? Height { get; init; }

        /// <inheritdoc cref="IDiscordEmbedImage.Width"/>
        [JsonPropertyName("width")]
        public int? Width { get; init; }
    }
}