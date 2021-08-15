using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Commands.Services.Builders;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations.Builders
{
    /// <inheritdoc />
    public class ComponentBuildService : IComponentBuildService
    {
        private static readonly TypeInfo ModuleTypeInfo = typeof(IComponentInteractionModule).GetTypeInfo();
        private readonly ILogger<ComponentBuildService> _logger;
        private readonly ISlashCommandRequirementBuildService _requirementBuildService;

        /// <summary>
        ///     Initializes a new instance of <see cref="ComponentBuildService" />.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" /> for <see cref="ComponentBuildService" />.</param>
        /// <param name="requirementBuildService">
        ///     The <see cref="ISlashCommandRequirementBuildService" /> that will get and build the component requirements.
        /// </param>
        public ComponentBuildService(ILogger<ComponentBuildService> logger, ISlashCommandRequirementBuildService requirementBuildService)
        {
            _logger = logger;
            _requirementBuildService = requirementBuildService;
        }

        /// <inheritdoc />
        public IEnumerable<IComponentInfo> BuildComponentInfos(Assembly assembly)
        {
            _logger.LogInformation("Loading components for assembly {AssemblyName}", assembly.FullName);
            var validComponents = new List<IComponentInfo>();

            foreach (var parentModule in GetSlashComponentModules(assembly))
            {
                foreach (var validMethod in GetValidComponentMethods(parentModule))
                {
                    var componentAttribute = validMethod.GetCustomAttribute<ComponentAttribute>();
                    if (componentAttribute is null) throw new NullReferenceException(nameof(componentAttribute));

                    var componentInfo = new ComponentInfo(componentAttribute.CustomId, componentAttribute.Type, validMethod, parentModule)
                    {
                        Requirements = _requirementBuildService.GetCommandRequirements(validMethod)
                    };
                    validComponents.Add(componentInfo);
                    _logger.LogDebug("Found valid Component {Id} in Component module {ModuleName}", componentInfo.CustomId, parentModule.FullName);
                }
            }

            return validComponents;
        }

        /// <summary>
        ///     Gets the <see cref="TypeInfo" />s of all the classes that extend <see cref="IComponentInteractionModule" />.
        /// </summary>
        /// <param name="assembly">The assembly where the <see cref="IComponentInteractionModule" /> are located.</param>
        /// <returns>
        ///     The <see cref="TypeInfo" />s of all the classes that extend <see cref="IComponentInteractionModule" />.
        /// </returns>
        private IEnumerable<TypeInfo> GetSlashComponentModules(Assembly assembly)
        {
            var result = new List<TypeInfo>();

            foreach (var typeInfo in assembly.DefinedTypes)
            {
                if (!typeInfo.IsPublic && !typeInfo.IsNestedPublic)
                    continue;

                if (IsValidModuleDefinition(typeInfo))
                    result.Add(typeInfo);
            }

            return result;
        }

        /// <summary>
        ///     Get a <see cref="IEnumerable{T}" /> of <see cref="MethodInfo" />s containing only valid component methods.
        /// </summary>
        /// <param name="parentModule">The class that contains the components.</param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="MethodInfo" />s containing only valid component methods.
        /// </returns>
        private static IEnumerable<MethodInfo> GetValidComponentMethods(IReflect parentModule)
        {
            return parentModule
                   .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                   .Where(IsValidCommandDefinition);
        }

        /// <summary>
        ///     Checks whether or not a component module is valid.
        /// </summary>
        /// <param name="typeInfo">The component module that will be checked if its valid.</param>
        /// <returns>
        ///     Whether or not a component module is valid.
        /// </returns>
        private static bool IsValidModuleDefinition(TypeInfo typeInfo)
        {
            return ModuleTypeInfo.IsAssignableFrom(typeInfo) && !typeInfo.IsAbstract && !typeInfo.ContainsGenericParameters;
        }

        /// <summary>
        ///     Checks whether or not a command method is valid.
        /// </summary>
        /// <param name="methodInfo">The method that will be checked if it valid.</param>
        /// <returns>
        ///     Whether or not a command method is valid.
        /// </returns>
        private static bool IsValidCommandDefinition(MethodInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(ComponentAttribute))
                   && methodInfo.ReturnType == typeof(Task<Result<IDiscordInteractionResponse>>)
                   && !methodInfo.IsStatic
                   && !methodInfo.IsGenericMethod;
        }
    }
}