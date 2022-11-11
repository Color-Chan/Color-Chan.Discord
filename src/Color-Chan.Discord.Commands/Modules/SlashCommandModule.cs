using System;
using Color_Chan.Discord.Commands.Models.Contexts;

namespace Color_Chan.Discord.Commands.Modules;

/// <inheritdoc cref="ISlashCommandModule" />
public class SlashCommandModule : InteractionModuleBase, ISlashCommandModule
{
    /// <summary>
    ///     The current context the slash command.
    /// </summary>
    protected ISlashCommandContext Context { get; set; } = null!;

    /// <inheritdoc />
    public void SetContext(ISlashCommandContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
}