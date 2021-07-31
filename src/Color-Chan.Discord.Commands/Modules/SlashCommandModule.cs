﻿using System;
using Color_Chan.Discord.Core;

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
        /// <exception cref="ArgumentNullException">When the given context was null.</exception>
        public void SetContext(ISlashCommandContext slashContext)
        {
            SlashContext = slashContext ?? throw new ArgumentNullException(nameof(slashContext));
        }
    }
}