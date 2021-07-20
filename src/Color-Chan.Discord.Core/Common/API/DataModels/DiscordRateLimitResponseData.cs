using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <summary>
    ///     Only returned in the body of an HTTP 429 response.
    /// </summary>
    public record DiscordRateLimitResponseData
    {
        /// <summary>
        ///     A message saying you are being rate limited.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; init; } = null!;

        /// <summary>
        ///     The number of seconds to wait before submitting another request.
        /// </summary>
        [JsonPropertyName("retry_after")]
        public float RetryAfter { get; init; }

        /// <summary>
        ///     A value indicating if you are being globally rate limited or not
        /// </summary>
        [JsonPropertyName("global")]
        public bool IsGlobal { get; init; }
    }
}