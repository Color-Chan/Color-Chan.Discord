using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    public record DiscordEmbedVideoData
    {
        /// <summary>
        ///     Source url of video.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <summary>
        ///     A proxied url of the video.
        /// </summary>
        [JsonPropertyName("proxy_url")]
        public string? ProxyUrl { get; init; }

        /// <summary>
        ///     Height of video.
        /// </summary>
        [JsonPropertyName("height")]
        public int? Height { get; init; }

        /// <summary>
        ///     Width of video.
        /// </summary>
        [JsonPropertyName("width")]
        public int? Width { get; init; }
    }
}