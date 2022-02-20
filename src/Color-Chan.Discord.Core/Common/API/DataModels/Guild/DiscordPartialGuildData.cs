using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    /// <summary>
    ///     Represents a discord Guild Structure API model.
    ///     Docs: https://discord.com/developers/docs/resources/guild#guild-object-guild-structure
    /// </summary>
    public record DiscordPartialGuildData
    {
        /// <inheritdoc cref="IDiscordGuild.Id"/>
        [JsonPropertyName("id")]
        public ulong? Id { get; init; }

        /// <inheritdoc cref="IDiscordGuild.Name"/>
        [JsonPropertyName("name")]
        public string? Name { get; init; } = null!;

        /// <inheritdoc cref="IDiscordGuild.Icon"/>
        [JsonPropertyName("icon")]
        public string? Icon { get; init; }

        /// <inheritdoc cref="IDiscordGuild.IconHash"/>
        [JsonPropertyName("icon_hash")]
        public string? IconHash { get; init; }

        /// <inheritdoc cref="IDiscordGuild.Splash"/>
        [JsonPropertyName("splash")]
        public string? Splash { get; init; }

        /// <inheritdoc cref="IDiscordGuild.DiscoverySplash"/>
        [JsonPropertyName("discovery_splash")]
        public string? DiscoverySplash { get; init; }

        /// <inheritdoc cref="IDiscordGuild.IsOwner"/>
        [JsonPropertyName("owner")]
        public bool? IsOwner { get; init; }

        /// <inheritdoc cref="IDiscordGuild.OwnerId"/>
        [JsonPropertyName("owner_id")]
        public ulong? OwnerId { get; init; }

        /// <inheritdoc cref="IDiscordGuild.CurrentUserPermission"/>
        [JsonPropertyName("permission")]
        public string? CurrentUserPermission { get; init; }

        /// <inheritdoc cref="IDiscordGuild.Region"/>
        [Obsolete("This field is deprecated and will be removed in v9 and is replaced by rtc_region")]
        [JsonPropertyName("region")]

        public string? Region { get; init; }

        /// <inheritdoc cref="IDiscordGuild.AfkChannelId"/>
        [JsonPropertyName("afk_channel_id")]
        public ulong? AfkChannelId { get; init; }

        /// <inheritdoc cref="IDiscordGuild.AfkTimeout"/>
        [JsonPropertyName("afk_timeout")]
        public int? AfkTimeout { get; init; }

        /// <inheritdoc cref="IDiscordGuild.WidgetEnabled"/>
        [JsonPropertyName("widget_enabled")]
        public bool? WidgetEnabled { get; init; }

        /// <inheritdoc cref="IDiscordGuild.WidgetChannelId"/>
        [JsonPropertyName("widget_channel_id")]

        public ulong? WidgetChannelId { get; init; }

        /// <inheritdoc cref="IDiscordGuild.VerificationLevel"/>
        [JsonPropertyName("verification_level")]
        public DiscordGuildVerificationLevel? VerificationLevel { get; init; }

        /// <inheritdoc cref="IDiscordGuild.DefaultMessageNotifications"/>
        [JsonPropertyName("default_message_notifications")]
        public DiscordGuildDefaultMessageNotificationLevel? DefaultMessageNotifications { get; init; }

        /// <inheritdoc cref="IDiscordGuild.ExplicitContentFilter"/>
        [JsonPropertyName("explicit_content_filter")]
        public DiscordGuildExplicitContentFilterLevel? ExplicitContentFilter { get; init; }

        /// <inheritdoc cref="IDiscordGuild.Roles"/>
        [JsonPropertyName("roles")]
        public IEnumerable<DiscordGuildRoleData>? Roles { get; init; } = new List<DiscordGuildRoleData>();

        /// <inheritdoc cref="IDiscordGuild.Emojis"/>
        [JsonPropertyName("emojis")]
        public IEnumerable<DiscordEmojiData>? Emojis { get; init; } = new List<DiscordEmojiData>();

        /// <inheritdoc cref="IDiscordGuild.Features"/>
        [JsonPropertyName("features")]
        public IEnumerable<DiscordGuildFeature>? Features { get; init; } = new List<DiscordGuildFeature>();

        /// <inheritdoc cref="IDiscordGuild.MfaLevel"/>
        [JsonPropertyName("mfa_level")]
        public DiscordGuildMfaLevel? MfaLevel { get; init; }

        /// <inheritdoc cref="IDiscordGuild.ApplicationId"/>
        [JsonPropertyName("application_id")]
        public ulong? ApplicationId { get; init; }

        /// <inheritdoc cref="IDiscordGuild.SystemChannelId"/>
        [JsonPropertyName("system_channel_id")]

        public ulong? SystemChannelId { get; init; }

        /// <inheritdoc cref="IDiscordGuild.SystemChannelFlags"/>
        [JsonPropertyName("system_channel_flags")]
        public DiscordSystemChannelFlags? SystemChannelFlags { get; init; }

        /// <inheritdoc cref="IDiscordGuild.RulesChannelId"/>
        [JsonPropertyName("rules_channel_id")]
        public ulong? RulesChannelId { get; init; }

        /// <inheritdoc cref="IDiscordGuild.JoinedAt"/>
        [JsonPropertyName("joined_at")]
        public DateTimeOffset? JoinedAt { get; init; }
        
        /// <inheritdoc cref="IDiscordGuild.Large"/>
        [JsonPropertyName("large")]
        public bool? Large { get; init; }

        /// <inheritdoc cref="IDiscordGuild.Unavailable"/>
        [JsonPropertyName("unavailable")]
        public bool? Unavailable { get; init; }

        /// <inheritdoc cref="IDiscordGuild.MemberCount"/>
        [JsonPropertyName("member_count")]
        public int? MemberCount { get; init; }

        /// <inheritdoc cref="IDiscordGuild.VoiceStates"/>
        [JsonPropertyName("voice_states")]
        public IEnumerable<DiscordVoiceState>? VoiceStates { get; init; }

        /// <inheritdoc cref="IDiscordGuild.Members"/>
        [JsonPropertyName("members")]
        public IEnumerable<DiscordGuildMemberData>? Members { get; init; }

        /// <inheritdoc cref="IDiscordGuild.Channels"/>
        [JsonPropertyName("channels")]
        public IEnumerable<DiscordChannelData>? Channels { get; init; }

        /// <inheritdoc cref="IDiscordGuild.Threads"/>
        [JsonPropertyName("threads")]
        public IEnumerable<DiscordChannelData>? Threads { get; init; }

        /// <inheritdoc cref="IDiscordGuild.Presences"/>
        [JsonPropertyName("presences")]
        public IEnumerable<DiscordGuildPresenceData>? Presences { get; init; }

        /// <inheritdoc cref="IDiscordGuild.MaxPresences"/>
        [JsonPropertyName("max_presences")]
        public int? MaxPresences { get; init; }

        /// <inheritdoc cref="IDiscordGuild.MaxMembers"/>
        [JsonPropertyName("max_members")]
        public int? MaxMembers { get; init; }
        
        /// <inheritdoc cref="IDiscordGuild.VanityUrlCode"/>
        [JsonPropertyName("vanity_url_code")]
        public string? VanityUrlCode { get; init; }

        /// <inheritdoc cref="IDiscordGuild.Description"/>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <inheritdoc cref="IDiscordGuild.Banner"/>
        [JsonPropertyName("banner")]
        public string? Banner { get; init; }

        /// <inheritdoc cref="IDiscordGuild.PremiumTier"/>
        [JsonPropertyName("premium_tier")]
        public DiscordGuildPremiumTier? PremiumTier { get; init; }

        /// <inheritdoc cref="IDiscordGuild.PremiumSubscriptionCount"/>
        [JsonPropertyName("premium_subscription_count")]
        public int? PremiumSubscriptionCount { get; init; }

        /// <inheritdoc cref="IDiscordGuild.PreferredLocale"/>
        [JsonPropertyName("preferred_locale")]
        public string? PreferredLocale { get; init; } = null!;

        /// <inheritdoc cref="IDiscordGuild.PublicUpdatesChannelId"/>
        [JsonPropertyName("public_updates_channel_id")]
        public ulong? PublicUpdatesChannelId { get; init; }
        
        /// <inheritdoc cref="IDiscordGuild.MaxVideoChannelUsers"/>
        [JsonPropertyName("max_video_channel_users")]
        public int? MaxVideoChannelUsers { get; init; }

        /// <inheritdoc cref="IDiscordGuild.ApproximateMemberCount"/>
        [JsonPropertyName("approximate_member_count")]
        public int? ApproximateMemberCount { get; init; }

        /// <inheritdoc cref="IDiscordGuild.ApproximatePresenceCount"/>
        [JsonPropertyName("approximate_presence_count")]
        public int? ApproximatePresenceCount { get; init; }

        /// <inheritdoc cref="IDiscordGuild.WelcomeScreen"/>
        [JsonPropertyName("welcome_screen")]
        public DiscordGuildWelcomeScreenData? WelcomeScreen { get; init; }

        /// <inheritdoc cref="IDiscordGuild.NsfwLevel"/>
        [JsonPropertyName("nsfw_level")]
        public DiscordGuildNsfwLevel? NsfwLevel { get; init; }

        /// <inheritdoc cref="IDiscordGuild.StageInstances"/>
        [JsonPropertyName("stage_instances")]
        public DiscordStageinstanceData? StageInstances { get; init; }
    }
}