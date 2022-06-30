using System.Collections.Generic;

namespace Color_Chan.Discord.Commands.Models.Contexts;

/// <inheritdoc cref="ISlashCommandContext" />
public class SlashCommandContext : InteractionContext, ISlashCommandContext
{
    /// <inheritdoc />
    public IEnumerable<string> SlashCommandName { get; set; } = null!;
}