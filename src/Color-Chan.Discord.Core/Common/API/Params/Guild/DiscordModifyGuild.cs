using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.API.Params.Guild
{
    public class DiscordModifyGuild
    {
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
        ///     Id of owner.
        /// </summary>
        [JsonPropertyName("owner_id")]
        public ulong OwnerId { get; init; }

        /// <summary>
        ///     Voice region id for the guild (deprecated).
        /// </summary>
        /// <remarks>
        ///     This field is deprecated and will be removed in v9 and is replaced by rtc_region.
        /// </remarks>
        [Obsolete("This field is deprecated and will be removed in v9 and is replaced by rtc_region")]
        [JsonPropertyName("region")]
        public string? Region { get; set; }

        /// <summary>
        ///     Id of afk channel.
        /// </summary>
        [JsonPropertyName("afk_channel_id")]
        public ulong? AfkChannelId { get; set; }

        /// <summary>
        ///     Ffk timeout in seconds.
        /// </summary>
        [JsonPropertyName("afk_timeout")]
        public int AfkTimeout { get; set; }

        /// <summary>
        ///     Verification level required for the guild.
        /// </summary>
        [JsonPropertyName("verification_level")]
        public DiscordGuildVerificationLevel VerificationLevel { get; set; }

        /// <summary>
        ///     Default message notifications level.
        /// </summary>
        [JsonPropertyName("default_message_notifications")]
        public DiscordGuildDefaultMessageNotificationLevel DefaultMessageNotifications { get; set; }

        /// <summary>
        ///     Explicit content filter level.
        /// </summary>
        [JsonPropertyName("explicit_content_filter")]
        public DiscordGuildExplicitContentFilterLevel ExplicitContentFilter { get; set; }

        /// <summary>
        ///     Enabled guild features.
        /// </summary>
        [JsonPropertyName("features")]
        public IEnumerable<string> Features { get; set; } = new List<string>();

        /// <summary>
        ///     The id of the channel where guild notices such as welcome messages and boost events are posted.
        /// </summary>
        [JsonPropertyName("system_channel_id")]

        public ulong? SystemChannelId { get; set; }

        /// <summary>
        ///     The system channel flags.
        /// </summary>
        [JsonPropertyName("system_channel_flags")]
        public DiscordSystemChannelFlags SystemChannelFlags { get; set; }

        /// <summary>
        ///     The id of the channel where Community guilds can display rules and/or guidelines.
        /// </summary>
        [JsonPropertyName("rules_channel_id")]
        public ulong? RulesChannelId { get; set; }

        /// <summary>
        ///     The description of a Community guild.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     Banner hash.
        /// </summary>
        [JsonPropertyName("banner")]
        public string? Banner { get; set; }

        /// <summary>
        ///     The preferred locale of a Community guild; used in server discovery and notices from Discord; defaults to "en-US".
        /// </summary>
        [JsonPropertyName("preferred_locale")]
        public string PreferredLocale { get; set; } = null!;

        /// <summary>
        ///     The id of the channel where admins and moderators of Community guilds receive notices from Discord.
        /// </summary>
        [JsonPropertyName("public_updates_channel_id")]
        public ulong? PublicUpdatesChannelId { get; set; }
    }
}