using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    /// <inheritdoc cref="IDiscordEmbedAuthor"/>
    public record DiscordEmbedAuthorData
    {
        /// <inheritdoc cref="IDiscordEmbedAuthor.Name"/>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <inheritdoc cref="IDiscordEmbedAuthor.Url"/>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <inheritdoc cref="IDiscordEmbedAuthor.IconUrl"/>
        [JsonPropertyName("icon_url")]
        public string? IconUrl { get; init; }

        /// <inheritdoc cref="IDiscordEmbedAuthor.ProxyIconUrl"/>
        [JsonPropertyName("proxy_icon_url")]
        public string? ProxyIconUrl { get; init; }
    }
}