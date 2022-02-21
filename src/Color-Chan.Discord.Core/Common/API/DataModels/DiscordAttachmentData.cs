using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <inheritdoc cref="IDiscordAttachment"/>
    public record DiscordAttachmentData
    {
        /// <inheritdoc cref="IDiscordAttachment.Id"/>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <inheritdoc cref="IDiscordAttachment.FileName"/>
        [JsonPropertyName("filename")]
        public string FileName { get; init; } = null!;

        /// <inheritdoc cref="IDiscordAttachment.Size"/>
        [JsonPropertyName("size")]
        public int Size { get; init; }

        /// <inheritdoc cref="IDiscordAttachment.Url"/>
        [JsonPropertyName("url")]
        public string Url { get; init; } = null!;

        /// <inheritdoc cref="IDiscordAttachment.ProxyUrl"/>
        [JsonPropertyName("proxy_url")]
        public string ProxyUrl { get; init; } = null!;

        /// <inheritdoc cref="IDiscordAttachment.Height"/>
        [JsonPropertyName("height")]
        public int? Height { get; init; }

        /// <inheritdoc cref="IDiscordAttachment.Width"/>
        [JsonPropertyName("width")]
        public int? Width { get; init; }
    }
}