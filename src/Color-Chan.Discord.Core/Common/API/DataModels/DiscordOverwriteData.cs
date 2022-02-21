using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <inheritdoc cref="IDiscordOverwrite"/>
    public record DiscordOverwriteData
    {
        /// <inheritdoc cref="IDiscordOverwrite.TargetId"/>
        [JsonPropertyName("id")]
        public ulong TargetId { get; init; }

        /// <inheritdoc cref="IDiscordOverwrite.TargetType"/>
        [JsonPropertyName("type")]
        public DiscordPermissionTargetType TargetType { get; init; }

        /// <inheritdoc cref="IDiscordOverwrite.Allow"/>
        [JsonPropertyName("allow")]
        public DiscordPermission Allow { get; init; }

        /// <inheritdoc cref="IDiscordOverwrite.Deny"/>
        [JsonPropertyName("deny")]
        public DiscordPermission Deny { get; init; }
    }
}