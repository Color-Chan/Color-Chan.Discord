using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    public record DiscordEmbedProviderData
    {
        /// <summary>
        ///     Name of provider.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        ///     Url of provider.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }
    }
}