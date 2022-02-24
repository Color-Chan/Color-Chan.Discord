using System;
using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Guild
{
    /// <inheritdoc />
    public class DiscordGuild : IDiscordGuild
    {
        /// <summary>
        ///     Initializes a new <see cref="DiscordGuild"/>
        /// </summary>
        /// <param name="data">The data needed to create the <see cref="DiscordGuild"/>.</param>
        public DiscordGuild(DiscordGuildData data)
        {
            Id = data.Id;
            Name = data.Name;
            Icon = data.Icon;
            IconHash = data.IconHash;
            Splash = data.Splash;
            DiscoverySplash = data.DiscoverySplash;
            IsOwner = data.IsOwner;
            OwnerId = data.OwnerId;
            CurrentUserPermission = data.CurrentUserPermission;
            //Region = data.Region;
            AfkChannelId = data.AfkChannelId;
            AfkTimeout = data.AfkTimeout;
            WidgetEnabled = data.WidgetEnabled;
            WidgetChannelId = data.WidgetChannelId;
            VerificationLevel = data.VerificationLevel;
            DefaultMessageNotifications = data.DefaultMessageNotifications;
            ExplicitContentFilter = data.ExplicitContentFilter;
            Roles = data.Roles.Select(roleData => new DiscordGuildRole(roleData));
            Emojis = data.Emojis.Select(emojiData => new DiscordEmoji(emojiData));
            Features = data.Features;
            MfaLevel = data.MfaLevel;
            ApplicationId = data.ApplicationId;
            SystemChannelId = data.SystemChannelId;
            SystemChannelFlags = data.SystemChannelFlags;
            RulesChannelId = data.RulesChannelId;
            JoinedAt = data.JoinedAt;
            Large = data.Large;
            Unavailable = data.Unavailable;
            MemberCount = data.MemberCount;
            VoiceStates = data.VoiceStates;
            Members = data.Members?.Select(memberData => new DiscordGuildMember(memberData));
            Channels = data.Channels?.Select(channelData => new DiscordChannel(channelData));
            Threads = data.Threads?.Select(channelData => new DiscordChannel(channelData));
            Presences = data.Presences;
            MaxPresences = data.MaxPresences;
            MaxMembers = data.MaxMembers;
            VanityUrlCode = data.VanityUrlCode;
            Description = data.Description;
            Banner = data.Banner;
            PremiumTier = data.PremiumTier;
            PremiumSubscriptionCount = data.PremiumSubscriptionCount;
            PreferredLocale = data.PreferredLocale;
            PublicUpdatesChannelId = data.PublicUpdatesChannelId;
            MaxVideoChannelUsers = data.MaxVideoChannelUsers;
            ApproximateMemberCount = data.ApproximateMemberCount;
            ApproximatePresenceCount = data.ApproximatePresenceCount;
            WelcomeScreen = data.WelcomeScreen is not null ? new DiscordGuildWelcomeScreen(data.WelcomeScreen) : null;
            NsfwLevel = data.NsfwLevel;
            StageInstances = data.StageInstances is not null ? new DiscordStageInstance(data.StageInstances) : null;
        }

        /// <inheritdoc />
        public ulong Id { get; init; }

        /// <inheritdoc />
        public string Name { get; init; }

        /// <inheritdoc />
        public string? Icon { get; init; }

        /// <inheritdoc />
        public string? IconHash { get; init; }

        /// <inheritdoc />
        public string? Splash { get; init; }

        /// <inheritdoc />
        public string? DiscoverySplash { get; init; }

        /// <inheritdoc />
        public bool? IsOwner { get; init; }

        /// <inheritdoc />
        public ulong OwnerId { get; init; }

        /// <inheritdoc />
        public string? CurrentUserPermission { get; set; }

        /// <inheritdoc />
        public string? Region { get; set; }

        /// <inheritdoc />
        public ulong? AfkChannelId { get; set; }

        /// <inheritdoc />
        public int AfkTimeout { get; set; }

        /// <inheritdoc />
        public bool? WidgetEnabled { get; set; }

        /// <inheritdoc />
        public ulong? WidgetChannelId { get; set; }

        /// <inheritdoc />
        public DiscordGuildVerificationLevel VerificationLevel { get; set; }

        /// <inheritdoc />
        public DiscordGuildDefaultMessageNotificationLevel DefaultMessageNotifications { get; set; }

        /// <inheritdoc />
        public DiscordGuildExplicitContentFilterLevel ExplicitContentFilter { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordGuildRole> Roles { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordEmoji> Emojis { get; set; }

        /// <inheritdoc />
        public IEnumerable<DiscordGuildFeature> Features { get; set; }

        /// <inheritdoc />
        public DiscordGuildMfaLevel MfaLevel { get; set; }

        /// <inheritdoc />
        public ulong? ApplicationId { get; set; }

        /// <inheritdoc />
        public ulong? SystemChannelId { get; set; }

        /// <inheritdoc />
        public DiscordSystemChannelFlags SystemChannelFlags { get; set; }

        /// <inheritdoc />
        public ulong? RulesChannelId { get; set; }

        /// <inheritdoc />
        public DateTimeOffset? JoinedAt { get; set; }

        /// <inheritdoc />
        public bool? Large { get; set; }

        /// <inheritdoc />
        public bool? Unavailable { get; set; }

        /// <inheritdoc />
        public int? MemberCount { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordVoiceState>? VoiceStates { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordGuildMember>? Members { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordChannel>? Channels { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordChannel>? Threads { get; set; }

        /// <inheritdoc />
        public IEnumerable<DiscordGuildPresenceData>? Presences { get; set; }

        /// <inheritdoc />
        public int? MaxPresences { get; set; }

        /// <inheritdoc />
        public int? MaxMembers { get; set; }

        /// <inheritdoc />
        public string? VanityUrlCode { get; set; }

        /// <inheritdoc />
        public string? Description { get; set; }

        /// <inheritdoc />
        public string? Banner { get; set; }

        /// <inheritdoc />
        public DiscordGuildPremiumTier PremiumTier { get; set; }

        /// <inheritdoc />
        public int? PremiumSubscriptionCount { get; set; }

        /// <inheritdoc />
        public string PreferredLocale { get; set; }

        /// <inheritdoc />
        public ulong? PublicUpdatesChannelId { get; set; }

        /// <inheritdoc />
        public int? MaxVideoChannelUsers { get; set; }

        /// <inheritdoc />
        public int? ApproximateMemberCount { get; set; }

        /// <inheritdoc />
        public int? ApproximatePresenceCount { get; set; }

        /// <inheritdoc />
        public IDiscordGuildWelcomeScreen? WelcomeScreen { get; set; }

        /// <inheritdoc />
        public DiscordGuildNsfwLevel NsfwLevel { get; set; }

        /// <inheritdoc />
        public IDiscordStageInstance? StageInstances { get; set; }
    }
}