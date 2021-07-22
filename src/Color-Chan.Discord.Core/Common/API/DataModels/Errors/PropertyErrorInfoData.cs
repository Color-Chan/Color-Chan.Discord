using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Errors
{
    public class PropertyErrorInfoData
    {
        /// <summary>
        ///     The error code.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = null!;

        /// <summary>
        ///     The error message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;
    }
}