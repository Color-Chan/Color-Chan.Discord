using System.Collections.Generic;

namespace Color_Chan.Discord.Commands.Models.Contexts;

/// <summary>
///     The context for a slash command.
/// </summary>
public interface ISlashCommandContext : IInteractionContext
{
    /// <summary>
    ///     The full slash command name.
    /// </summary>
    IEnumerable<string> SlashCommandName { get; set; }
}