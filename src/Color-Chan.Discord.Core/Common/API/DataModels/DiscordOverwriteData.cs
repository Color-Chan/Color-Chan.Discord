﻿using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

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
        public DiscordPermission Allow { get; init; }

        /// <summary>
        ///     Permission bit set.
        /// </summary>
        [JsonPropertyName("deny")]
        public DiscordPermission Deny { get; init; }
    }
}