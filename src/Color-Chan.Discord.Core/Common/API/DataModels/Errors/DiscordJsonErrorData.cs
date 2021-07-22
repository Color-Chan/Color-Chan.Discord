using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Errors
{
    public record DiscordJsonErrorData
    {
        /// <summary>
        ///     The error message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        /// <summary>
        ///     The <see cref="DiscordJsonError" /> describing the error.
        /// </summary>
        [JsonPropertyName("code")]
        public DiscordJsonError ErrorType { get; init; }

        //Todo: sub errors
        /// <summary>
        ///     The sub errors.
        /// </summary>
        [JsonPropertyName("errors")]
        public IReadOnlyDictionary<string, PropertyErrorData>? Errors { get; init; }
    }
}