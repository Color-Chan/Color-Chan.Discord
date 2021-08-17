using System;
using Color_Chan.Discord.Commands.Models.Contexts;

namespace Color_Chan.Discord.Commands.Modules
{
    /// <inheritdoc cref="IComponentInteractionModule" />
    public class ComponentInteractionModule : InteractionModuleBase, IComponentInteractionModule
    {
        /// <summary>
        ///     The current context the component interaction.
        /// </summary>
        protected IComponentContext Context { get; set; } = null!;

        /// <inheritdoc />
        public void SetContext(IComponentContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}