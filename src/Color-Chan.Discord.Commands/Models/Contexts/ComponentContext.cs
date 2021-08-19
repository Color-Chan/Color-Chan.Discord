using System.Collections.Generic;

namespace Color_Chan.Discord.Commands.Models.Contexts
{
    /// <inheritdoc cref="IComponentContext" />
    public class ComponentContext : InteractionContext, IComponentContext
    {
        /// <inheritdoc />
        public List<string> Args { get; set; } = new();
    }
}