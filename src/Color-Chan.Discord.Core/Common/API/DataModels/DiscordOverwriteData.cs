using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    public record DiscordOverwriteData
    {
        /// <summary>
        ///     Role or user id.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong TargetId { get; init; }

        /// <summary>
        ///     Either 0 (role) or 1 (member).
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordPermissionTargetType TargetType { get; init; }

        /// <summary>
        ///     Permission bit set.
        /// </summary>
        [JsonPropertyName("allow")]
        public string Allow { get; init; } = null!;

        /// <summary>
        ///     Permission bit set.
        /// </summary>
        [JsonPropertyName("deny")]
        public string Deny { get; init; } = null!;
    }
}