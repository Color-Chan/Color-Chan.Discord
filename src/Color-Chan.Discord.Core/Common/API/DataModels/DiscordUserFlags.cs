﻿using System;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <summary>
///     Represents a discord User Flags API model.
///     Docs: https://discord.com/developers/docs/resources/user#user-object-user-flags
/// </summary>
[Flags]
public enum DiscordUserFlags
{
    /// <summary>
    ///     Default value for flags, when none are given to an account.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Flag given to users who are a Discord employee.
    /// </summary>
    Staff = 1 << 0,

    /// <summary>
    ///     Flag given to users who are owners of a partnered Discord server.
    /// </summary>
    Partner = 1 << 1,

    /// <summary>
    ///     Flag given to users in HypeSquad events.
    /// </summary>
    HypeSquadEvents = 1 << 2,

    /// <summary>
    ///     Flag given to users who have participated in the report program and are level 1.
    /// </summary>
    BugHunterLevel1 = 1 << 3,

    /// <summary>
    ///     Flag given to users who are in the HypeSquad House of Bravery.
    /// </summary>
    HypeSquadBravery = 1 << 6,

    /// <summary>
    ///     Flag given to users who are in the HypeSquad House of Brilliance.
    /// </summary>
    HypeSquadBrilliance = 1 << 7,

    /// <summary>
    ///     Flag given to users who are in the HypeSquad House of Balance.
    /// </summary>
    HypeSquadBalance = 1 << 8,

    /// <summary>
    ///     Flag given to users who subscribed to Nitro before games were added.
    /// </summary>
    EarlySupporter = 1 << 9,

    /// <summary>
    ///     Flag given to users who are part of a team.
    /// </summary>
    TeamUser = 1 << 10,

    /// <summary>
    ///     Flag given to users who represent Discord (System).
    /// </summary>
    System = 1 << 12,

    /// <summary>
    ///     Flag given to users who have participated in the report program and are level 2.
    /// </summary>
    BugHunterLevel2 = 1 << 14,

    /// <summary>
    ///     Flag given to users who are verified bots.
    /// </summary>
    VerifiedBot = 1 << 16,

    /// <summary>
    ///     Flag given to users that developed bots and early verified their accounts.
    /// </summary>
    EarlyVerifiedBotDeveloper = 1 << 17,

    /// <summary>
    ///     Flag given to users that are discord certified moderators who has give discord's exam.
    /// </summary>
    DiscordCertifiedModerator = 1 << 18,

    /// <summary>
    ///     Bot uses only HTTP interactions and is shown in the online member list.
    /// </summary>
    BotHttpInteractions = 1 << 19
}