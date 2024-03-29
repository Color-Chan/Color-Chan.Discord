﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <summary>
///     Represents a discord Guild Structure API model.
///     Docs: https://discord.com/developers/docs/resources/guild#guild-object-guild-structure
/// </summary>
public record DiscordGuildData
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
    ///     Icon hash, returned when in the template object.
    /// </summary>
    [JsonPropertyName("icon_hash")]
    public string? IconHash { get; init; }

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
    ///     True if the user is the owner of the guild.
    /// </summary>
    /// <remarks>
    ///     Only sent when using the GET Current User Guilds endpoint and are relative to the requested user.
    /// </remarks>
    [JsonPropertyName("owner")]
    public bool? IsOwner { get; init; }

    /// <summary>
    ///     Id of owner.
    /// </summary>
    [JsonPropertyName("owner_id")]
    public ulong OwnerId { get; init; }

    /// <summary>
    ///     Total permissions for the user in the guild (excludes overwrites).
    /// </summary>
    [JsonPropertyName("permission")]
    public string? CurrentUserPermission { get; init; }

    /// <summary>
    ///     Voice region id for the guild (deprecated).
    /// </summary>
    /// <remarks>
    ///     This field is deprecated and will be removed in v9 and is replaced by rtc_region.
    /// </remarks>
    [Obsolete("This field is deprecated and will be removed in v9 and is replaced by rtc_region")]
    [JsonPropertyName("region")]

    public string? Region { get; init; }

    /// <summary>
    ///     Id of afk channel.
    /// </summary>
    [JsonPropertyName("afk_channel_id")]
    public ulong? AfkChannelId { get; init; }

    /// <summary>
    ///     Afk timeout in seconds.
    /// </summary>
    [JsonPropertyName("afk_timeout")]
    public int AfkTimeout { get; init; }

    /// <summary>
    ///     True if the server widget is enabled.
    /// </summary>
    [JsonPropertyName("widget_enabled")]
    public bool? WidgetEnabled { get; init; }

    /// <summary>
    ///     The channel id that the widget will generate an invite to, or null if set to no invite.
    /// </summary>
    [JsonPropertyName("widget_channel_id")]

    public ulong? WidgetChannelId { get; init; }

    /// <summary>
    ///     Verification level required for the guild.
    /// </summary>
    [JsonPropertyName("verification_level")]
    public DiscordGuildVerificationLevel VerificationLevel { get; init; }

    /// <summary>
    ///     Default message notifications level.
    /// </summary>
    [JsonPropertyName("default_message_notifications")]
    public DiscordGuildDefaultMessageNotificationLevel DefaultMessageNotifications { get; init; }

    /// <summary>
    ///     Explicit content filter level.
    /// </summary>
    [JsonPropertyName("explicit_content_filter")]
    public DiscordGuildExplicitContentFilterLevel ExplicitContentFilter { get; init; }

    /// <summary>
    ///     Roles in the guild.
    /// </summary>
    [JsonPropertyName("roles")]
    public IEnumerable<DiscordGuildRoleData> Roles { get; init; } = new List<DiscordGuildRoleData>();

    /// <summary>
    ///     Custom guild emojis.
    /// </summary>
    [JsonPropertyName("emojis")]
    public IEnumerable<DiscordEmojiData> Emojis { get; init; } = new List<DiscordEmojiData>();

    /// <summary>
    ///     Enabled guild features.
    /// </summary>
    [JsonPropertyName("features")]
    public IEnumerable<DiscordGuildFeature> Features { get; init; } = new List<DiscordGuildFeature>();

    /// <summary>
    ///     Required MFA level for the guild.
    /// </summary>
    [JsonPropertyName("mfa_level")]
    public DiscordGuildMfaLevel MfaLevel { get; init; }

    /// <summary>
    ///     Application id of the guild creator if it is bot-created.
    /// </summary>
    [JsonPropertyName("application_id")]
    public ulong? ApplicationId { get; init; }

    /// <summary>
    ///     The id of the channel where guild notices such as welcome messages and boost events are posted.
    /// </summary>
    [JsonPropertyName("system_channel_id")]

    public ulong? SystemChannelId { get; init; }

    /// <summary>
    ///     The system channel flags.
    /// </summary>
    [JsonPropertyName("system_channel_flags")]
    public DiscordSystemChannelFlags SystemChannelFlags { get; init; }

    /// <summary>
    ///     The id of the channel where Community guilds can display rules and/or guidelines.
    /// </summary>
    [JsonPropertyName("rules_channel_id")]
    public ulong? RulesChannelId { get; init; }

    /// <summary>
    ///     When the current user joined this guild.
    /// </summary>
    /// <remarks>
    ///     Only send on the 'GUILD_CREATE' event.
    /// </remarks>
    [JsonPropertyName("joined_at")]
    public DateTimeOffset? JoinedAt { get; init; }

    /// <summary>
    ///     True if this is considered a large guild.
    /// </summary>
    /// <remarks>
    ///     Only send on the 'GUILD_CREATE' event.
    /// </remarks>
    [JsonPropertyName("large")]
    public bool? Large { get; init; }

    /// <summary>
    ///     True if this guild is unavailable due to an outage.
    /// </summary>
    /// <remarks>
    ///     Only send on the 'GUILD_CREATE' event.
    /// </remarks>
    [JsonPropertyName("unavailable")]
    public bool? Unavailable { get; init; }

    /// <summary>
    ///     Total number of members in this guild.
    /// </summary>
    /// <remarks>
    ///     Only send on the 'GUILD_CREATE' event.
    /// </remarks>
    [JsonPropertyName("member_count")]
    public int? MemberCount { get; init; }

    /// <summary>
    ///     States of members currently in voice channels; lacks the guild_id key.
    /// </summary>
    /// <remarks>
    ///     Only send on the 'GUILD_CREATE' event.
    /// </remarks>
    [JsonPropertyName("voice_states")]
    public IEnumerable<DiscordVoiceState>? VoiceStates { get; init; }

    /// <summary>
    ///     Users in the guild.
    /// </summary>
    /// <remarks>
    ///     Only send on the 'GUILD_CREATE' event.
    /// </remarks>
    [JsonPropertyName("members")]
    public IEnumerable<DiscordGuildMemberData>? Members { get; init; }

    /// <summary>
    ///     Channels in the guild.
    /// </summary>
    /// <remarks>
    ///     Only send on the 'GUILD_CREATE' event.
    /// </remarks>
    [JsonPropertyName("channels")]
    public IEnumerable<DiscordChannelData>? Channels { get; init; }

    /// <summary>
    ///     All active threads in the guild that current user has permission to view.
    /// </summary>
    /// <remarks>
    ///     Only send on the 'GUILD_CREATE' event.
    /// </remarks>
    [JsonPropertyName("threads")]
    public IEnumerable<DiscordChannelData>? Threads { get; init; }

    /// <summary>
    ///     presences of the members in the guild, will only include non-offline members if the size is greater than large
    ///     threshold.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Only send on the 'GUILD_CREATE' event.
    ///     </para>
    ///     <para>
    ///         If you are using Gateway Intents, you must specify the GUILD_PRESENCES intent in order to receive Presence
    ///         Update events.
    ///     </para>
    /// </remarks>
    [JsonPropertyName("presences")]
    public IEnumerable<DiscordGuildPresenceData>? Presences { get; init; }

    /// <summary>
    ///     The maximum number of presences for the guild (null is always returned, apart from the largest of guilds).
    /// </summary>
    [JsonPropertyName("max_presences")]
    public int? MaxPresences { get; init; }

    /// <summary>
    ///     The maximum number of members for the guild.
    /// </summary>
    [JsonPropertyName("max_members")]
    public int? MaxMembers { get; init; }

    /// <summary>
    ///     The vanity url code for the guild.
    /// </summary>
    [JsonPropertyName("vanity_url_code")]
    public string? VanityUrlCode { get; init; }

    /// <summary>
    ///     The description of a Community guild.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    ///     Banner hash.
    /// </summary>
    [JsonPropertyName("banner")]
    public string? Banner { get; init; }

    /// <summary>
    ///     Premium tier (Server Boost level).
    /// </summary>
    [JsonPropertyName("premium_tier")]
    public DiscordGuildPremiumTier PremiumTier { get; init; }

    /// <summary>
    ///     The number of boosts this guild currently has.
    /// </summary>
    [JsonPropertyName("premium_subscription_count")]

    public int? PremiumSubscriptionCount { get; init; }

    /// <summary>
    ///     The preferred locale of a Community guild; used in server discovery and notices from Discord; defaults to "en-US".
    /// </summary>
    [JsonPropertyName("preferred_locale")]
    public string PreferredLocale { get; init; } = null!;

    /// <summary>
    ///     The id of the channel where admins and moderators of Community guilds receive notices from Discord.
    /// </summary>
    [JsonPropertyName("public_updates_channel_id")]
    public ulong? PublicUpdatesChannelId { get; init; }

    /// <summary>
    ///     The maximum amount of users in a video channel.
    /// </summary>
    [JsonPropertyName("max_video_channel_users")]
    public int? MaxVideoChannelUsers { get; init; }

    /// <summary>
    ///     Approximate number of members in this guild, returned from the GET /guilds/{id} endpoint when with_counts is true.
    /// </summary>
    [JsonPropertyName("approximate_member_count")]
    public int? ApproximateMemberCount { get; init; }

    /// <summary>
    ///     Approximate number of non-offline members in this guild, returned from the GET /guilds/{id} endpoint when
    ///     with_counts is true.
    /// </summary>
    [JsonPropertyName("approximate_presence_count")]
    public int? ApproximatePresenceCount { get; init; }

    /// <summary>
    ///     The welcome screen of a Community guild, shown to new members, returned in an Invite's guild object.
    /// </summary>
    [JsonPropertyName("welcome_screen")]
    public DiscordGuildWelcomeScreenData? WelcomeScreen { get; init; }

    /// <summary>
    ///     Guild NSFW level.
    /// </summary>
    [JsonPropertyName("nsfw_level")]
    public DiscordGuildNsfwLevel NsfwLevel { get; init; }

    /// <summary>
    ///     Stage instances in the guild.
    /// </summary>
    /// <remarks>
    ///     Only send on the 'GUILD_CREATE' event.
    /// </remarks>
    [JsonPropertyName("stage_instances")]
    public DiscordStageinstanceData? StageInstances { get; init; }
}