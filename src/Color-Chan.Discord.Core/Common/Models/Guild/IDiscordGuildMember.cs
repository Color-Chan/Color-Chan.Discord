using System;
using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models.Guild;

/// <summary>
///     Represents a discord Guild Member Structure API model.
///     Docs: https://discord.com/developers/docs/resources/guild#guild-member-object-guild-member-structure
/// </summary>
public interface IDiscordGuildMember
{
    /// <summary>
    ///     The user this guild member represents.
    /// </summary>
    IDiscordUser? User { get; init; }

    /// <summary>
    ///     This users guild nickname.
    /// </summary>
    string? NickName { get; init; }

    /// <summary>
    ///     A list of role ids that are assigned the this guild member.
    /// </summary>
    IEnumerable<ulong> Roles { get; init; }

    /// <summary>
    ///     When the user joined the guild.
    /// </summary>
    DateTimeOffset JoinedAt { get; init; }

    /// <summary>
    ///     When the user started boosting the guild.
    /// </summary>
    DateTimeOffset? PremiumSince { get; init; }

    /// <summary>
    ///     Whether the user is deafened in voice channels.
    /// </summary>
    bool Deaf { get; init; }

    /// <summary>
    ///     Whether the user is muted in voice channels.
    /// </summary>
    bool Mute { get; init; }

    /// <summary>
    ///     Whether the user has not yet passed the guild's Membership Screening requirements.
    /// </summary>
    bool? Pending { get; init; }

    /// <summary>
    ///     Total permissions of the member in the channel, including overwrites, returned when in the interaction object.
    /// </summary>
    DiscordPermission? Permissions { get; init; }
}