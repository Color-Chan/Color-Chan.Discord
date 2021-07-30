using System;
using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.Models.Guild
{
    /// <summary>
    ///     A local representation for <see cref="DiscordGuildData" />.
    /// </summary>
    public interface IDiscordGuild
    {
        /// <summary>
        ///     The Discord provided snowflake id.
        /// </summary>
        ulong Id { get; init; }

        /// <summary>
        ///     Guild name (2-100 characters, excluding trailing and leading whitespace).
        /// </summary>
        string Name { get; init; }

        /// <summary>
        ///     Icon hash.
        /// </summary>
        string? Icon { get; init; }

        /// <summary>
        ///     Icon hash, returned when in the template object.
        /// </summary>
        string? IconHash { get; init; }

        /// <summary>
        ///     Splash hash.
        /// </summary>
        string? Splash { get; init; }

        /// <summary>
        ///     Discovery splash hash; only present for guilds with the "DISCOVERABLE" feature.
        /// </summary>
        string? DiscoverySplash { get; init; }

        /// <summary>
        ///     True if the user is the owner of the guild.
        /// </summary>
        /// <remarks>
        ///     Only sent when using the GET Current User Guilds endpoint and are relative to the requested user.
        /// </remarks>
        bool? IsOwner { get; init; }

        /// <summary>
        ///     Id of owner.
        /// </summary>
        ulong OwnerId { get; init; }

        /// <summary>
        ///     Total permissions for the user in the guild (excludes overwrites).
        /// </summary>
        string? CurrentUserPermission { get; set; }

        /// <summary>
        ///     Voice region id for the guild (deprecated).
        /// </summary>
        /// <remarks>
        ///     This field is deprecated and will be removed in v9 and is replaced by rtc_region.
        /// </remarks>
        [Obsolete("This field is deprecated and will be removed in v9 and is replaced by rtc_region")]
        string? Region { get; set; }

        /// <summary>
        ///     Id of afk channel.
        /// </summary>
        ulong? AfkChannelId { get; set; }

        /// <summary>
        ///     Ffk timeout in seconds.
        /// </summary>
        int AfkTimeout { get; set; }

        /// <summary>
        ///     True if the server widget is enabled.
        /// </summary>
        bool? WidgetEnabled { get; set; }

        /// <summary>
        ///     The channel id that the widget will generate an invite to, or null if set to no invite.
        /// </summary>
        ulong? WidgetChannelId { get; set; }

        /// <summary>
        ///     Verification level required for the guild.
        /// </summary>
        DiscordGuildVerificationLevel VerificationLevel { get; set; }

        /// <summary>
        ///     Default message notifications level.
        /// </summary>
        DiscordGuildDefaultMessageNotificationLevel DefaultMessageNotifications { get; set; }

        /// <summary>
        ///     Explicit content filter level.
        /// </summary>
        DiscordGuildExplicitContentFilterLevel ExplicitContentFilter { get; set; }

        /// <summary>
        ///     Roles in the guild.
        /// </summary>
        IEnumerable<IDiscordGuildRole> Roles { get; set; }

        /// <summary>
        ///     Custom guild emojis.
        /// </summary>
        IEnumerable<IDiscordEmoji> Emojis { get; set; }

        /// <summary>
        ///     Enabled guild features.
        /// </summary>
        IEnumerable<string> Features { get; set; }

        /// <summary>
        ///     Required MFA level for the guild.
        /// </summary>
        DiscordGuildMfaLevel MfaLevel { get; set; }

        /// <summary>
        ///     Application id of the guild creator if it is bot-created.
        /// </summary>
        ulong? ApplicationId { get; set; }

        /// <summary>
        ///     The id of the channel where guild notices such as welcome messages and boost events are posted.
        /// </summary>
        ulong? SystemChannelId { get; set; }

        /// <summary>
        ///     The system channel flags.
        /// </summary>
        DiscordSystemChannelFlags SystemChannelFlags { get; set; }

        /// <summary>
        ///     The id of the channel where Community guilds can display rules and/or guidelines.
        /// </summary>
        ulong? RulesChannelId { get; set; }

        /// <summary>
        ///     When the current user joined this guild.
        /// </summary>
        /// <remarks>
        ///     Only send on the 'GUILD_CREATE' event.
        /// </remarks>
        DateTimeOffset? JoinedAt { get; set; }

        /// <summary>
        ///     True if this is considered a large guild.
        /// </summary>
        /// <remarks>
        ///     Only send on the 'GUILD_CREATE' event.
        /// </remarks>
        bool? Large { get; set; }

        /// <summary>
        ///     True if this guild is unavailable due to an outage.
        /// </summary>
        /// <remarks>
        ///     Only send on the 'GUILD_CREATE' event.
        /// </remarks>
        bool? Unavailable { get; set; }

        /// <summary>
        ///     Total number of members in this guild.
        /// </summary>
        /// <remarks>
        ///     Only send on the 'GUILD_CREATE' event.
        /// </remarks>
        int? MemberCount { get; set; }

        /// <summary>
        ///     States of members currently in voice channels; lacks the guild_id key.
        /// </summary>
        /// <remarks>
        ///     Only send on the 'GUILD_CREATE' event.
        /// </remarks>
        IEnumerable<DiscordVoiceState>? VoiceStates { get; set; }
        
        /// <summary>
        ///     Users in the guild.
        /// </summary>
        /// <remarks>
        ///     Only send on the 'GUILD_CREATE' event.
        /// </remarks>
        IEnumerable<IDiscordGuildMember>? Members { get; set; }
        
        /// <summary>
        ///     Channels in the guild.
        /// </summary>
        /// <remarks>
        ///     Only send on the 'GUILD_CREATE' event.
        /// </remarks>
        IEnumerable<IDiscordChannel>? Channels { get; set; }
        
        /// <summary>
        ///     All active threads in the guild that current user has permission to view.
        /// </summary>
        /// <remarks>
        ///     Only send on the 'GUILD_CREATE' event.
        /// </remarks>
        IEnumerable<IDiscordChannel>? Threads { get; set; }

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
        IEnumerable<DiscordGuildPresenceData>? Presences { get; set; }

        /// <summary>
        ///     The maximum number of presences for the guild (null is always returned, apart from the largest of guilds).
        /// </summary>
        int? MaxPresences { get; set; }

        /// <summary>
        ///     The maximum number of members for the guild.
        /// </summary>
        int? MaxMembers { get; set; }

        /// <summary>
        ///     The vanity url code for the guild.
        /// </summary>
        string? VanityUrlCode { get; set; }

        /// <summary>
        ///     The description of a Community guild.
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        ///     Banner hash.
        /// </summary>
        string? Banner { get; set; }

        /// <summary>
        ///     Premium tier (Server Boost level).
        /// </summary>
        DiscordGuildPremiumTier PremiumTier { get; set; }

        /// <summary>
        ///     The number of boosts this guild currently has.
        /// </summary>
        int? PremiumSubscriptionCount { get; set; }

        /// <summary>
        ///     The preferred locale of a Community guild; used in server discovery and notices from Discord; defaults to "en-US".
        /// </summary>
        string PreferredLocale { get; set; }

        /// <summary>
        ///     The id of the channel where admins and moderators of Community guilds receive notices from Discord.
        /// </summary>
        ulong? PublicUpdatesChannelId { get; set; }

        /// <summary>
        ///     The maximum amount of users in a video channel.
        /// </summary>
        int? MaxVideoChannelUsers { get; set; }

        /// <summary>
        ///     Approximate number of members in this guild, returned from the GET /guilds/{id} endpoint when with_counts is true.
        /// </summary>
        int? ApproximateMemberCount { get; set; }

        /// <summary>
        ///     Approximate number of non-offline members in this guild, returned from the GET /guilds/{id} endpoint when
        ///     with_counts is true.
        /// </summary>
        int? ApproximatePresenceCount { get; set; }
        
        /// <summary>
        ///     The welcome screen of a Community guild, shown to new members, returned in an Invite's guild object.
        /// </summary>
        IDiscordGuildWelcomeScreen? WelcomeScreen { get; set; }

        /// <summary>
        ///     Guild NSFW level.
        /// </summary>
        DiscordGuildNsfwLevel NsfwLevel { get; set; }
        
        /// <summary>
        ///     Stage instances in the guild.
        /// </summary>
        /// <remarks>
        ///     Only send on the 'GUILD_CREATE' event.
        /// </remarks>
        IDiscordStageInstance? StageInstances { get; set; }
    }
}