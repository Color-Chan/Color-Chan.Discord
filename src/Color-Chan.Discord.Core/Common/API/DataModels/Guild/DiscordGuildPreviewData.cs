using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    public class DiscordGuildPreviewData
    {
        /// <summary>
        ///     Guild id.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <summary>
        ///     Guild name (2-100 characters, excluding trailing and leading whitespace).
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <summary>
        ///     Icon hash.
        /// </summary>
        [JsonPropertyName("icon")]
        public string? Icon { get; init; }

        /// <summary>
        ///     Splash hash.
        /// </summary>
        [JsonPropertyName("splash")]
        public string? Splash { get; init; }

        /// <summary>
        ///     Discovery splash hash; only present for guilds with the "DISCOVERABLE" feature.
        /// </summary>
        [JsonPropertyName("discovery_splash")]
        public string? DiscoverySplash { get; init; }

        /// <summary>
        ///     Roles in the guild.
        /// </summary>
        [JsonPropertyName("roles")]
        public IEnumerable<DiscordGuildRoleData> Roles { get; set; } = new List<DiscordGuildRoleData>();

        /// <summary>
        ///     Custom guild emojis.
        /// </summary>
        [JsonPropertyName("emojis")]
        public IEnumerable<DiscordEmojiData> Emojis { get; set; } = new List<DiscordEmojiData>();

        /// <summary>
        ///     Approximate number of members in this guild, returned from the GET /guilds/{id} endpoint when with_counts is true.
        /// </summary>
        [JsonPropertyName("approximate_member_count")]
        public int? ApproximateMemberCount { get; set; }

        /// <summary>
        ///     Approximate number of non-offline members in this guild, returned from the GET /guilds/{id} endpoint when
        ///     with_counts is true.
        /// </summary>
        [JsonPropertyName("approximate_presence_count")]
        public int? ApproximatePresenceCount { get; set; }

        /// <summary>
        ///     The description for the guild, if the guild is discoverable.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }
    }
}