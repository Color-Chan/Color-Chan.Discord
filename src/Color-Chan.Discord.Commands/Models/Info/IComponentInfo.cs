using System.Reflection;
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
    }
}