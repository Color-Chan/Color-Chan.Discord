using System.Collections.Generic;

namespace Color_Chan.Discord.Core.Common.Models.Application;

/// <summary>
///     Represents a discord Application Command Structure API model.
///     Docs:
///     https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-structure
/// </summary>
public interface IDiscordApplicationCommand
{
    /// <summary>
    ///     Unique id of the command.
    /// </summary>
    ulong Id { get; init; }

    /// <summary>
    ///     Unique id of the parent application.
    /// </summary>
    ulong ApplicationId { get; init; }

    /// <summary>
    ///     Guild id of the command, if not global.
    /// </summary>
    ulong? GuildId { get; init; }

    /// <summary>
    ///     1-32 lowercase character name matching ^[\w-]{1,32}$.
    /// </summary>
    string Name { get; init; }

    /// <summary>
    ///     1-100 character description.
    /// </summary>
    string Description { get; init; }

    /// <summary>
    ///     the parameters for the command.
    /// </summary>
    IEnumerable<IDiscordApplicationCommandOption>? Options { get; init; }

    /// <summary>
    ///     Whether the command is enabled by default when the app is added to a guild.
    /// </summary>
    bool? DefaultPermission { get; init; }
}