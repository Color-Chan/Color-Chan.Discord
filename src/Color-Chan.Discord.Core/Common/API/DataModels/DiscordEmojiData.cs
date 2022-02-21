using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <inheritdoc cref="IDiscordEmoji"/>
    public record DiscordEmojiData
    {
        /// <inheritdoc cref="IDiscordEmoji.Id"/>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <inheritdoc cref="IDiscordEmoji.Name"/>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <inheritdoc cref="IDiscordEmoji.RoleIds"/>
        [JsonPropertyName("roles")]
        public IEnumerable<ulong>? RoleIds { get; init; }
        
        /// <inheritdoc cref="IDiscordEmoji.User"/>
        [JsonPropertyName("user")]
        public DiscordUserData? User { get; init; }

        /// <inheritdoc cref="IDiscordEmoji.RequireColons"/>
        [JsonPropertyName("require_colons")]
        public bool? RequireColons { get; init; }

        /// <inheritdoc cref="IDiscordEmoji.IsManaged"/>
        [JsonPropertyName("managed")]
        public bool? IsManaged { get; init; }

        /// <inheritdoc cref="IDiscordEmoji.IsAnimated"/>
        [JsonPropertyName("animated")]
        public bool? IsAnimated { get; init; }

        /// <inheritdoc cref="IDiscordEmoji.IsAvailable"/>
        [JsonPropertyName("available")]
        public bool? IsAvailable { get; init; }
    }
}