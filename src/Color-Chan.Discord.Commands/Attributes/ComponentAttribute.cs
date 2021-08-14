using System;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     Makes a method available as a component interaction handler.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ComponentAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandAttribute" />.
        /// </summary>
        /// <param name="customId">The custom id of the component that the underlying method will handle click events of.</param>
        public ComponentAttribute(string customId)
        {
            CustomId = customId;
        }
        
        /// <summary>
        ///     The developer-defined identifier for the component, max 100 characters
        /// </summary>
        public string CustomId { get; }
    }
}