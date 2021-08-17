using System;
using Color_Chan.Discord.Commands.Models.Contexts;

namespace Color_Chan.Discord.Commands.Modules
{
    /// <summary>
    ///     The base that should be used for all component modules.
    /// </summary>
    public interface IComponentInteractionModule
    {
        /// <summary>
        ///     Set the current <see cref="IComponentContext" /> for a component.
        /// </summary>
        /// <param name="context">The new <see cref="IComponentContext" />.</param>
        /// <exception cref="ArgumentNullException">When the given context was null.</exception>
        void SetContext(IComponentContext context);
    }
}