using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Errors
{
    /// <summary>
    ///     Represents a discord Json error response info.
    /// </summary>
    public class PropertyErrorInfoData
    {
        /// <summary>
        ///     The error code.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; init; } = null!;

        /// <summary>
        ///     The error message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; init; } = null!;
    }
}