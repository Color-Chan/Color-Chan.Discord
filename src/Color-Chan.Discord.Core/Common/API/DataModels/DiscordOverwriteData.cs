using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <summary>
    ///     Represents a discord Overwrite Structure API model.
    ///     Docs: https://discord.com/developers/docs/resources/channel#overwrite-object-overwrite-structure
    /// </summary>
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
        public DiscordPermission Allow { get; init; }

        /// <summary>
        ///     Permission bit set.
        /// </summary>
        [JsonPropertyName("deny")]
        public DiscordPermission Deny { get; init; }
    }
}