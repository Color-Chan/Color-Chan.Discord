using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    public record DiscordEmojiData
    {
        /// <summary>
        ///     Emoji id.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <summary>
        ///     Emoji name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <summary>
        ///     Roles allowed to use this emoji
        /// </summary>
        [JsonPropertyName("roles")]
        public IEnumerable<ulong>? RoleIds { get; init; }

        /// <summary>
        ///     User that created this emoji
        /// </summary>
        [JsonPropertyName("user")]
        public DiscordUserData? User { get; init; }

        /// <summary>
        ///     Whether this emoji must be wrapped in colons.
        /// </summary>
        [JsonPropertyName("require_colons")]
        public bool? RequireColons { get; init; }

        /// <summary>
        ///     Whether this emoji is managed.
        /// </summary>
        [JsonPropertyName("managed")]
        public bool? IsManaged { get; init; }

        /// <summary>
        ///     Whether this emoji is animated.
        /// </summary>
        [JsonPropertyName("animated")]
        public bool? IsAnimated { get; init; }

        /// <summary>
        ///     Whether this emoji can be used, may be false due to loss of Server Boosts.
        /// </summary>
        [JsonPropertyName("available")]
        public bool? IsAvailable { get; init; }
    }
}