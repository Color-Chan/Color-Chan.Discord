using System;
using Color_Chan.Discord.Core.Common.API.DataModels.Invite;
using Color_Chan.Discord.Core.Common.Models.Application;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.Models.Invites;

/// <summary>
///     Represents a discord Invite Structure API model.
///     Docs: https://discord.com/developers/docs/resources/invite#invite-object-invite-structure
/// </summary>
public interface IDiscordInvite
{
    /// <summary>
    ///     The invite code (unique ID).
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    ///     The guild this invite is for.
    /// </summary>
    public IDiscordPartialGuild? PartialGuild { get; set; }

    /// <summary>
    ///     The channel this invite is for.
    /// </summary>
    public IDiscordPartialChannel? PartialChannel { get; set; }

    /// <summary>
    ///     The user who created the invite.
    /// </summary>
    public IDiscordUser? Inviter { get; set; }

    /// <summary>
    ///     The type of target for this voice channel invite.
    /// </summary>
    public DiscordInviteTargetType? TargetType { get; set; }

    /// <summary>
    ///     The user whose stream to display for this voice channel stream invite.
    /// </summary>
    public IDiscordUser? TargetUser { get; set; }

    /// <summary>
    ///     The embedded application to open for this voice channel embedded application invite.
    /// </summary>
    public IDiscordPartialApplication? TargetPartialApplication { get; set; }

    /// <summary>
    ///     Approximate count of online members.
    /// </summary>
    /// <remarks>
    ///     returned from the GET /invites/CODE endpoint when with_counts is true.
    /// </remarks>
    public int? ApproximatePresenceCount { get; set; }

    /// <summary>
    ///     Approximate count of total members.
    /// </summary>
    /// <remarks>
    ///     returned from the GET /invites/CODE endpoint when with_counts is true.
    /// </remarks>
    public int? ApproximateMemberCount { get; set; }

    /// <summary>
    ///     The expiration date of this invite.
    /// </summary>
    /// <remarks>
    ///     returned from the GET /invites/CODE endpoint when with_expiration is true.
    /// </remarks>
    public DateTimeOffset? ExpiresAt { get; set; }

    /// <summary>
    ///     Guild scheduled event data, only included if guild_scheduled_event_id contains a valid guild scheduled event id.
    /// </summary>
    public IDiscordGuildScheduledEvent? GuildScheduledEvent { get; set; }
}