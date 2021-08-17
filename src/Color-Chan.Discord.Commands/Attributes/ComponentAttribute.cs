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
        /// <param name="acknowledge">
        ///     Whether or not the component interaction should be automatically acknowledge to prevent the token
        ///     from turning inactive after 3 seconds.
        /// </param>
        /// <param name="editOriginalMessage">
        ///     Whether or not the original message of the component should be edited with the returned response. Ignored if <paramref name="acknowledge"/> is set to false.
        /// </param>
        public ComponentAttribute(string customId, DiscordComponentType type, bool acknowledge = false, bool editOriginalMessage = false)
        {
            CustomId = customId;
            Type = type;
            Acknowledge = acknowledge;
            EditOriginalMessage = editOriginalMessage;
        }

        /// <summary>
        ///     The developer-defined identifier for the component, max 100 characters
        /// </summary>
        public string CustomId { get; }

        /// <summary>
        ///     The type of the component that will be handled.
        /// </summary>
        public DiscordComponentType Type { get; }

        /// <summary>
        ///     Whether or not the component interaction should be automatically acknowledge to prevent the token
        ///     from turning inactive after 3 seconds.
        /// </summary>
        public bool Acknowledge { get; }

        /// <summary>
        ///     Whether or not the original message of the component should be edited with the returned response.
        /// </summary>
        public bool EditOriginalMessage { get; }
    }
}