using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Commands.Models.Info
{
    /// <inheritdoc />
    public class ComponentInfo : IComponentInfo
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandInfo" />.
        /// </summary>
        /// <param name="customId">The custom id of the component.</param>
        /// <param name="type">The type of the component.</param>
        /// <param name="componentMethod">The method of the component.</param>
        /// <param name="parentModule">The parent module of the component.</param>
        /// <param name="acknowledge">
        ///     Whether or not the component interaction should be automatically acknowledge to prevent the token
        ///     from turning inactive after 3 seconds.
        /// </param>
        /// <param name="editOriginalMessage">
        ///     Whether or not the original message of the component should be edited with the
        ///     returned response.
        /// </param>
        public ComponentInfo(string customId, DiscordComponentType type, MethodInfo componentMethod, TypeInfo parentModule, bool acknowledge, bool editOriginalMessage)
        {
            CustomId = customId;
            Type = type;
            ComponentMethod = componentMethod;
            ParentModule = parentModule;
            Acknowledge = acknowledge;
            EditOriginalMessage = editOriginalMessage;
        }

        /// <inheritdoc />
        public string CustomId { get; set; }

        /// <inheritdoc />
        public DiscordComponentType Type { get; set; }

        /// <inheritdoc />
        public MethodInfo ComponentMethod { get; set; }

        /// <inheritdoc />
        public TypeInfo ParentModule { get; set; }

        /// <inheritdoc />
        public IEnumerable<InteractionRequirementAttribute>? Requirements { get; set; }

        /// <inheritdoc />
        public bool Acknowledge { get; }

        /// <inheritdoc />
        public bool EditOriginalMessage { get; }
    }
}