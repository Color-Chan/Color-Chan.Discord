using System;
using Color_Chan.Discord.Core.Common.API.DataModels;

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
        /// <param name="customId">The custom id of the component that the underlying method will handle.</param>
        /// <param name="type">The type of the component that will be handled.</param>
        public ComponentAttribute(string customId, DiscordComponentType type)
        {
            CustomId = customId;
            Type = type;
        }
        
        /// <summary>
        ///     The developer-defined identifier for the component, max 100 characters
        /// </summary>
        public string CustomId { get; }

        /// <summary>
        ///     The type of the component that will be handled.
        /// </summary>
        public DiscordComponentType Type { get; }
    }
}