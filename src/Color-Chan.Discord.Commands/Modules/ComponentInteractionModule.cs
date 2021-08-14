using System;
using Color_Chan.Discord.Commands.Models.Contexts;

namespace Color_Chan.Discord.Commands.Modules
{
    /// <inheritdoc />
    public class ComponentInteractionModule : IComponentInteractionModule
    {
        /// <summary>
        ///     The current context the component interaction.
        /// </summary>
        protected IInteractionContext Context { get; set; } = null!;

        /// <inheritdoc />
        public void SetContext(IInteractionContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}