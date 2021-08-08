using System;
using Color_Chan.Discord.Commands.Contexts;

namespace Color_Chan.Discord.Commands.Modules
{
    /// <inheritdoc />
    public class SlashCommandModule : ISlashCommandModule
    {
        /// <summary>
        ///     The current context the the slash command.
        /// </summary>
        public ISlashCommandContext SlashContext { get; set; } = null!;

        /// <inheritdoc />
        public void SetContext(ISlashCommandContext slashContext)
        {
            SlashContext = slashContext ?? throw new ArgumentNullException(nameof(slashContext));
        }
    }
}