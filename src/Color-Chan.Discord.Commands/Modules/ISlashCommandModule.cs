using System;
using Color_Chan.Discord.Commands.Models.Contexts;

namespace Color_Chan.Discord.Commands.Modules;

/// <summary>
///     The base that should be used for all slash command modules.
/// </summary>
public interface ISlashCommandModule
{
    /// <summary>
    ///     Set the current <see cref="ISlashCommandContext" /> for a command.
    /// </summary>
    /// <param name="context">The new <see cref="ISlashCommandContext" />.</param>
    /// <exception cref="ArgumentNullException">When the given context was null.</exception>
    void SetContext(ISlashCommandContext context);
}