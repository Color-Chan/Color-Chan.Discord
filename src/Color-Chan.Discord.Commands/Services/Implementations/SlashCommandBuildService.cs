using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations
{
    public class SlashCommandBuildService : ISlashCommandBuildService
    {
        private static readonly TypeInfo ModuleTypeInfo = typeof(ISlashCommandModuleBase).GetTypeInfo();
        private readonly ISlashCommandGuildBuildService _guildBuildService;
        private readonly ILogger<SlashCommandBuildService> _logger;
        private readonly ISlashCommandOptionBuildService _optionBuildService;
        private readonly ISlashCommandRequirementBuildService _requirementBuildService;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandBuildService" />.
        /// </summary>
        /// <param name="requirementBuildService">
        ///     The <see cref="ISlashCommandRequirementBuildService" /> that will get and build
        ///     the command requirements.
        /// </param>
        /// <param name="guildBuildService">
        ///     The <see cref="ISlashCommandGuildBuildService" /> that will get and build the guild
        ///     attributes.
        /// </param>
        /// <param name="logger">The <see cref="ILogger" /> for <see cref="SlashCommandBuildService" />.</param>
        /// <param name="optionBuildService">
        ///     The <see cref="ISlashCommandOptionBuildService" /> that will get and build the
        ///     <see cref="ISlashCommandOptionInfo" />s.
        /// </param>
        public SlashCommandBuildService(ISlashCommandRequirementBuildService requirementBuildService, ISlashCommandGuildBuildService guildBuildService, ILogger<SlashCommandBuildService> logger,
            ISlashCommandOptionBuildService optionBuildService)
        {
            _requirementBuildService = requirementBuildService;
            _guildBuildService = guildBuildService;
            _logger = logger;
            _optionBuildService = optionBuildService;
        }

        /// <inheritdoc />
        public IReadOnlyList<KeyValuePair<string, ISlashCommandInfo>> BuildSlashCommandInfos(Assembly assembly)
        {
            _logger.LogInformation("Loading interaction commands for assembly {AssemblyName}", assembly.FullName);
            var validCommands = new List<KeyValuePair<string, ISlashCommandInfo>>();

            foreach (var parentModule in GetSlashCommandModules(assembly))
            {
                foreach (var validMethod in GetValidSlashCommandsMethods(parentModule))
                {
                    var commandInfoKeyValuePair = BuildCommandInfoKeyValuePair(validMethod, parentModule);

                    if (!commandInfoKeyValuePair.HasValue) continue;
                    validCommands.Add(commandInfoKeyValuePair.Value);
                }

                _logger.LogDebug("Found {CommandCount} valid commands in {ClassName}", validCommands.Count, parentModule.FullName);
            }

            return validCommands;
        }

        /// <inheritdoc />
        public IEnumerable<TypeInfo> GetSlashCommandModules(Assembly assembly)
        {
            var result = new List<TypeInfo>();

            foreach (var typeInfo in assembly.DefinedTypes)
                if (typeInfo.IsPublic || typeInfo.IsNestedPublic)
                    if (IsValidModuleDefinition(typeInfo))
                        result.Add(typeInfo);

            return result;
        }

        /// <summary>
        ///     Build a <see cref="KeyValuePair{TKey,TValue}" /> of <see cref="string" />, <see cref="ISlashCommandInfo" /> for a
        ///     command.
        /// </summary>
        /// <param name="validMethod">The command method.</param>
        /// <param name="parentModule">The <see cref="TypeInfo" /> of the module that owns the <paramref name="validMethod" />.</param>
        /// <returns>
        ///     A <see cref="KeyValuePair{TKey,TValue}" /> of <see cref="string" />, <see cref="ISlashCommandInfo" /> for a
        ///     command.
        /// </returns>
        private KeyValuePair<string, ISlashCommandInfo>? BuildCommandInfoKeyValuePair(MethodInfo validMethod, TypeInfo parentModule)
        {
            var commandAttribute = validMethod.GetCustomAttribute<SlashCommandAttribute>();
            if (commandAttribute != null)
            {
                var commandRequirements = _requirementBuildService.GetCommandRequirements(validMethod);
                var guildAttributes = _guildBuildService.GetCommandGuilds(validMethod);
                var options = _optionBuildService.GetCommandOptions(validMethod);

                var commandInfo = new SlashCommandInfo(commandAttribute.Name, commandAttribute.Description, validMethod, parentModule, commandRequirements, options, guildAttributes);
                return new KeyValuePair<string, ISlashCommandInfo>(commandAttribute.Name, commandInfo);
            }

            _logger.LogWarning("Can not load command {CommandName} since it doesn't have the InteractionCommandAttribute attribute", validMethod.Name);
            return null;
        }

        /// <summary>
        ///     Get a <see cref="IEnumerable{T}" /> of <see cref="MethodInfo" />s containing only valid commands methods.
        /// </summary>
        /// <param name="parentModule"></param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="MethodInfo" />s containing only valid commands methods.
        /// </returns>
        private IEnumerable<MethodInfo> GetValidSlashCommandsMethods(IReflect parentModule)
        {
            return parentModule.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(IsValidCommandDefinition);
        }

        /// <summary>
        ///     Checks whether or not a command module is valid.
        /// </summary>
        /// <param name="typeInfo">The command module that will be checked if its valid.</param>
        /// <returns>
        ///     Whether or not a command module is valid.
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
            return methodInfo.IsDefined(typeof(SlashCommandAttribute)) && methodInfo.ReturnType == typeof(Task<IDiscordInteractionResponse>) && !methodInfo.IsStatic &&
                   !methodInfo.IsGenericMethod;
        }
    }
}