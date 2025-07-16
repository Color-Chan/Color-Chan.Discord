using System;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <summary>
///     Represents a discord Guild Role Flags API model.
///     Docs: https://discord.com/developers/docs/topics/permissions#role-object-role-flags
/// </summary>
[Flags]
public enum DiscordGuildRoleFlags
{
    /// <summary>
    ///     No role flags.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Role can be selected by members in an onboarding prompt.
    /// </summary>
    InPrompt = 1 << 0
}