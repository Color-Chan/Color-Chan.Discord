using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    /// <inheritdoc cref="IDiscordEmbedProvider"/>
    public record DiscordEmbedProviderData
    {
        /// <inheritdoc cref="IDiscordEmbedProvider.Name"/>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <inheritdoc cref="IDiscordEmbedProvider.Url"/>
        [JsonPropertyName("url")]
        public string? Url { get; init; }
    }
}