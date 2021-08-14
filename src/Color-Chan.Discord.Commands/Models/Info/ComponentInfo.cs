using System.Reflection;
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
        public ComponentInfo(string customId, DiscordComponentType type, MethodInfo componentMethod, TypeInfo parentModule)
        {
            CustomId = customId;
            Type = type;
            ComponentMethod = componentMethod;
            ParentModule = parentModule;
        }

        /// <inheritdoc />
        public string CustomId { get; set; }

        /// <inheritdoc />
        public DiscordComponentType Type { get; set; }

        /// <inheritdoc />
        public MethodInfo ComponentMethod { get; set; }

        /// <inheritdoc />
        public TypeInfo ParentModule { get; set; }
    }
}