using System;
using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.Models.Guild;

/// <summary>
///     A local representation for <see cref="DiscordPartialGuildData" />.
/// </summary>
public interface IDiscordPartialGuild
{
    /// <inheritdoc cref="IDiscordGuild.Id" />
    ulong? Id { get; init; }

    /// <inheritdoc cref="IDiscordGuild.Name" />
    string? Name { get; init; }

    /// <inheritdoc cref="IDiscordGuild.Icon" />
    string? Icon { get; init; }

    /// <inheritdoc cref="IDiscordGuild.IconHash" />
    string? IconHash { get; init; }

    /// <inheritdoc cref="IDiscordGuild.Splash" />
    string? Splash { get; init; }

    /// <inheritdoc cref="IDiscordGuild.DiscoverySplash" />
    string? DiscoverySplash { get; init; }

    /// <inheritdoc cref="IDiscordGuild.IsOwner" />
    bool? IsOwner { get; init; }

    /// <inheritdoc cref="IDiscordGuild.OwnerId" />
    ulong? OwnerId { get; init; }

    /// <inheritdoc cref="IDiscordGuild.CurrentUserPermission" />
    string? CurrentUserPermission { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Region" />
    [Obsolete("This field is deprecated and will be removed in v9 and is replaced by rtc_region")]
    string? Region { get; set; }

    /// <inheritdoc cref="IDiscordGuild.AfkChannelId" />
    ulong? AfkChannelId { get; set; }

    /// <inheritdoc cref="IDiscordGuild.AfkTimeout" />
    int? AfkTimeout { get; set; }

    /// <inheritdoc cref="IDiscordGuild.WidgetEnabled" />
    bool? WidgetEnabled { get; set; }

    /// <inheritdoc cref="IDiscordGuild.WidgetChannelId" />
    ulong? WidgetChannelId { get; set; }

    /// <inheritdoc cref="IDiscordGuild.VerificationLevel" />
    DiscordGuildVerificationLevel? VerificationLevel { get; set; }

    /// <inheritdoc cref="IDiscordGuild.DefaultMessageNotifications" />
    DiscordGuildDefaultMessageNotificationLevel? DefaultMessageNotifications { get; set; }

    /// <inheritdoc cref="IDiscordGuild.ExplicitContentFilter" />
    DiscordGuildExplicitContentFilterLevel? ExplicitContentFilter { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Roles" />
    IEnumerable<IDiscordGuildRole>? Roles { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Emojis" />
    IEnumerable<IDiscordEmoji>? Emojis { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Features" />
    IEnumerable<DiscordGuildFeature>? Features { get; set; }

    /// <inheritdoc cref="IDiscordGuild.MfaLevel" />
    DiscordGuildMfaLevel? MfaLevel { get; set; }

    /// <inheritdoc cref="IDiscordGuild.ApplicationId" />
    ulong? ApplicationId { get; set; }

    /// <inheritdoc cref="IDiscordGuild.SystemChannelId" />
    ulong? SystemChannelId { get; set; }

    /// <inheritdoc cref="IDiscordGuild.SystemChannelFlags" />
    DiscordSystemChannelFlags? SystemChannelFlags { get; set; }

    /// <inheritdoc cref="IDiscordGuild.RulesChannelId" />
    ulong? RulesChannelId { get; set; }

    /// <inheritdoc cref="IDiscordGuild.JoinedAt" />
    DateTimeOffset? JoinedAt { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Large" />
    bool? Large { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Unavailable" />
    bool? Unavailable { get; set; }

    /// <inheritdoc cref="IDiscordGuild.MemberCount" />

    int? MemberCount { get; set; }

    /// <inheritdoc cref="IDiscordGuild.VoiceStates" />

    IEnumerable<DiscordVoiceState>? VoiceStates { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Members" />
    IEnumerable<IDiscordGuildMember>? Members { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Channels" />
    IEnumerable<IDiscordChannel>? Channels { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Threads" />
    IEnumerable<IDiscordChannel>? Threads { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Presences" />
    IEnumerable<DiscordGuildPresenceData>? Presences { get; set; }

    /// <inheritdoc cref="IDiscordGuild.MaxPresences" />
    int? MaxPresences { get; set; }

    /// <inheritdoc cref="IDiscordGuild.MaxMembers" />
    int? MaxMembers { get; set; }

    /// <inheritdoc cref="IDiscordGuild.VanityUrlCode" />
    string? VanityUrlCode { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Description" />
    string? Description { get; set; }

    /// <inheritdoc cref="IDiscordGuild.Banner" />
    string? Banner { get; set; }

    /// <inheritdoc cref="IDiscordGuild.PremiumTier" />
    DiscordGuildPremiumTier? PremiumTier { get; set; }

    /// <inheritdoc cref="IDiscordGuild.PremiumSubscriptionCount" />
    int? PremiumSubscriptionCount { get; set; }

    /// <inheritdoc cref="IDiscordGuild.PreferredLocale" />
    string? PreferredLocale { get; set; }

    /// <inheritdoc cref="IDiscordGuild.PublicUpdatesChannelId" />
    ulong? PublicUpdatesChannelId { get; set; }

    /// <inheritdoc cref="IDiscordGuild.MaxVideoChannelUsers" />
    int? MaxVideoChannelUsers { get; set; }

    /// <inheritdoc cref="IDiscordGuild.ApproximateMemberCount" />
    int? ApproximateMemberCount { get; set; }

    /// <inheritdoc cref="IDiscordGuild.ApproximatePresenceCount" />
    int? ApproximatePresenceCount { get; set; }

    /// <inheritdoc cref="IDiscordGuild.WelcomeScreen" />
    IDiscordGuildWelcomeScreen? WelcomeScreen { get; set; }

    /// <inheritdoc cref="IDiscordGuild.NsfwLevel" />
    DiscordGuildNsfwLevel? NsfwLevel { get; set; }

    /// <inheritdoc cref="IDiscordGuild.StageInstances" />
    IDiscordStageInstance? StageInstances { get; set; }
}