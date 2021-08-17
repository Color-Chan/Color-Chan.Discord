using System;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Modules
{
    /// <inheritdoc cref="ISlashCommandModule"/>
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
}