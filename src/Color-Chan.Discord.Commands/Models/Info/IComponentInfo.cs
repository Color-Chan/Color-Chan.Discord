using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Commands.Models.Info
{
    /// <summary>
    ///     Contains all the information about a component.
    /// </summary>
    public interface IComponentInfo
    {
        /// <summary>
        ///     The custom ID of the component.
        /// </summary>
        public string CustomId { get; set; }

        /// <summary>
        ///     The type of the component.
        /// </summary>
        public DiscordComponentType Type { get; set; }

        /// <summary>
        ///     The <see cref="MethodInfo" /> containing the method of the component.
        /// </summary>
        public MethodInfo ComponentMethod { get; set; }

        /// <summary>
        ///     The component module containing the <see cref="ComponentMethod" />.
        /// </summary>
        public TypeInfo ParentModule { get; set; }

        /// <summary>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="InteractionRequirementAttribute" />s containing all the requirements
        ///     to execute the component.
        /// </summary>
        public IEnumerable<InteractionRequirementAttribute>? Requirements { get; set; }

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