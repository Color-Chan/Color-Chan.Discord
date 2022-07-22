using System;
using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.Params.Application;

namespace Color_Chan.Discord.Commands.Services.Builders;

/// <summary>
///     Holds all the methods to build <see cref="ISlashCommandInfo" /> for guilds only.
/// </summary>
public interface ISlashCommandGuildBuildService
{
    /// <summary>
    ///     Get a <see cref="IEnumerable{T}" /> of <see cref="SlashCommandGuildAttribute" /> for a specific
    ///     <paramref name="command" />.
    /// </summary>
    /// <param name="command">The command method that will be used to find the <see cref="SlashCommandGuildAttribute" />s.</param>
    /// <param name="includeParentAttributes">Whether or not to include the parents attributes.</param>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandGuildAttribute" /> for the given
    ///     <paramref name="command" />.
    /// </returns>
    IEnumerable<SlashCommandGuildAttribute> GetCommandGuilds(MethodInfo command, bool includeParentAttributes = true);

    /// <summary>
    ///     Get a <see cref="IEnumerable{T}" /> of <see cref="SlashCommandGuildAttribute" /> for a specific
    ///     <see cref="SlashCommandModule" />
    /// </summary>
    /// <param name="commandModule">
    ///     The command module that will be used to find the <see cref="SlashCommandGuildAttribute" />
    ///     s.
    /// </param>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandGuildAttribute" /> for the given
    ///     <see cref="SlashCommandModule" />
    /// </returns>
    IEnumerable<SlashCommandGuildAttribute> GetCommandGuilds(Type commandModule);

    /// <summary>
    ///     Get a <see cref="IEnumerable{T}" /> of <see cref="SlashCommandPermissionAttribute" /> for a specific
    ///     <paramref name="command" />.
    /// </summary>
    /// <param name="command">The command method that will be used to find the <see cref="SlashCommandPermissionAttribute" />s.</param>
    /// <param name="includeParentAttributes">Whether or not to include the parents attributes.</param>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandPermissionAttribute" /> for the given
    ///     <paramref name="command" />.
    /// </returns>
    IEnumerable<SlashCommandPermissionAttribute> GetCommandGuildPermissions(MethodInfo command, bool includeParentAttributes = true);

    /// <summary>
    ///     Get a <see cref="IEnumerable{T}" /> of <see cref="SlashCommandPermissionAttribute" /> for a specific
    ///     <see cref="SlashCommandModule" />
    /// </summary>
    /// <param name="commandModule">
    ///     The command module that will be used to find the <see cref="SlashCommandPermissionAttribute" />
    ///     s.
    /// </param>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandPermissionAttribute" /> for the given
    ///     <see cref="SlashCommandModule" />
    /// </returns>
    IEnumerable<SlashCommandPermissionAttribute> GetCommandGuildPermissions(Type commandModule);

    /// <summary>
    ///     Builds the <see cref="DiscordBatchEditApplicationCommandPermissions" /> from the
    ///     <see cref="SlashCommandPermissionAttribute" /> data.
    /// </summary>
    /// <param name="attributePairs">
    ///     A dictionary where the <see cref="SlashCommandPermissionAttribute" /> are ordered by the
    ///     command ID.
    /// </param>
    /// <returns>
    ///     The converted <see cref="DiscordBatchEditApplicationCommandPermissions" />.
    /// </returns>
    IEnumerable<DiscordBatchEditApplicationCommandPermissions> BuildGuildPermissions(Dictionary<ulong, IEnumerable<SlashCommandPermissionAttribute>> attributePairs);
}