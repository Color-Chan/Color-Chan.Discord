using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    /// <summary>
    ///     Represents a discord Embed Provider Structure API model.
    ///     https://discord.com/developers/docs/resources/channel#embed-object-embed-provider-structure
    /// </summary>
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