using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    /// <summary>
    ///     Represents a discord Embed Author  Structure API model.
    ///     https://discord.com/developers/docs/resources/channel#embed-object-embed-author-structure
    /// </summary>
    public record DiscordEmbedAuthorData
    {
        /// <summary>
        ///     Name of author.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        ///     Url of author.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <summary>
        ///     Url of author icon (only supports http(s) and attachments).
        /// </summary>
        [JsonPropertyName("icon_url")]
        public string? IconUrl { get; init; }

        /// <summary>
        ///     A proxied url of author icon.
        /// </summary>
        [JsonPropertyName("proxy_icon_url")]
        public string? ProxyIconUrl { get; init; }
    }
}