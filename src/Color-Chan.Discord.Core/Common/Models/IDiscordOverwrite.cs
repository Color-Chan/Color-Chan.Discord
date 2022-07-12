using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models;

/// <summary>
///     Represents a discord Overwrite Structure API model.
///     Docs: https://discord.com/developers/docs/resources/channel#overwrite-object-overwrite-structure
/// </summary>
public interface IDiscordOverwrite
{
    /// <summary>
    ///     Role or user id.
    /// </summary>
    ulong TargetId { get; init; }

    /// <summary>
    ///     Either 0 (role) or 1 (member).
    /// </summary>
    DiscordPermissionTargetType TargetType { get; init; }

    /// <summary>
    ///     Permission bit set.
    /// </summary>
    DiscordPermission Allow { get; init; }

    /// <summary>
    ///     Permission bit set.
    /// </summary>
    DiscordPermission Deny { get; init; }
}