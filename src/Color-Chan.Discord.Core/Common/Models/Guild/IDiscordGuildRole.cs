using System;
using System.Drawing;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.Models.Guild;

/// <summary>
///     Represents a discord Role Structure API model.
///     Docs: https://discord.com/developers/docs/topics/permissions#role-object-role-structure
/// </summary>
public interface IDiscordGuildRole
{
    /// <summary>
    ///     Role id.
    /// </summary>
    ulong Id { get; init; }

    /// <summary>
    ///     Role name.
    /// </summary>
    string Name { get; init; }

    /// <summary>
    ///     Integer representation of hexadecimal color code.
    /// </summary>
    [Obsolete("Use Colors instead.")]
    Color Color { get; init; }
    
    /// <summary>
    ///     The role's colors.     
    /// </summary>
    public IDiscordGuildRoleColors Colors { get; init; }

    /// <summary>
    ///     If this role is pinned in the user listing.
    /// </summary>
    bool IsHoisted { get; init; }
    
    /// <summary>
    ///     Role icon hash.
    /// </summary>
    string? Icon { get; init; }
    
    /// <summary>
    ///     Role unicode emoji.
    /// </summary>
    string? UnicodeEmoji { get; init; }

    /// <summary>
    ///     Position of this role.
    /// </summary>
    int Position { get; init; }

    /// <summary>
    ///     Permission bit set.
    /// </summary>
    DiscordPermission Permissions { get; init; }

    /// <summary>
    ///     Whether this role is managed by an integration
    /// </summary>
    bool Managed { get; init; }

    /// <summary>
    ///     Whether this role is mentionable
    /// </summary>
    bool Mentionable { get; init; }
    
    /// <summary>
    ///     The tags this role has
    /// </summary>
    IDiscordGuildRoleTags? Tags { get; init; }
    
    /// <summary>
    ///     Role can be selected by members in an onboarding prompt
    /// </summary>
    DiscordGuildRoleFlags Flags { get; init; }

    /// <summary>
    ///     Converts the model back to a discord data model so that it can be send to discord.
    /// </summary>
    /// <returns>
    ///     The converted <see cref="DiscordGuildRoleData" />.
    /// </returns>
    DiscordGuildRoleData ToDataModel();
}