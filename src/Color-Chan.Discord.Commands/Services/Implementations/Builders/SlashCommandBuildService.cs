using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Commands.Services.Builders;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.Params.Application;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations.Builders
{
    /// <inheritdoc />
    public class SlashCommandBuildService : ISlashCommandBuildService
    {
        private static readonly TypeInfo ModuleTypeInfo = typeof(ISlashCommandModule).GetTypeInfo();
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
                if (IsValidCommandGroupModuleDefinition(parentModule))
                {
                    var groupAttribute = parentModule.GetCustomAttribute<SlashCommandGroupAttribute>();
                    if (groupAttribute is null)
                    {
                        _logger.LogWarning("Can not load command group {ModuleName} since it doesn't have the SlashCommandGroupAttribute attribute", parentModule.Name);
                        continue;
                    }

                    var commandInfoKeyValuePair = BuildCommandGroupInfoKeyValuePair(groupAttribute, parentModule);
                    validCommands.Add(commandInfoKeyValuePair);
                }

                // The command is not a sub command / group.

                foreach (var validMethod in GetValidSlashCommandsMethods(parentModule))
                {
                    var commandInfoKeyValuePair = BuildCommandInfoKeyValuePair(validMethod, parentModule);

                    if (!commandInfoKeyValuePair.HasValue) continue;
                    validCommands.Add(commandInfoKeyValuePair.Value);
                }

                _logger.LogDebug("Found {CommandCount} valid commands in {ClassName}", validCommands.Count.ToString(), parentModule.FullName);
            }

            return validCommands;
        }

        /// <inheritdoc />
        public IEnumerable<TypeInfo> GetSlashCommandModules(Assembly assembly)
        {
            var result = new List<TypeInfo>();

            foreach (var typeInfo in assembly.DefinedTypes)
            {
                if (typeInfo.IsPublic || typeInfo.IsNestedPublic)
                    if (IsValidModuleDefinition(typeInfo))
                        result.Add(typeInfo);
            }

            return result;
        }

        /// <inheritdoc />
        public IEnumerable<DiscordCreateApplicationCommand> BuildSlashCommandsParams(IEnumerable<ISlashCommandInfo> commandInfos)
        {
            var applicationCommandParams = new List<DiscordCreateApplicationCommand>();

            foreach (var commandInfo in commandInfos)
            {
                var options = _optionBuildService.BuildSlashCommandsOptions(commandInfo.CommandOptions);
                applicationCommandParams.Add(new DiscordCreateApplicationCommand
                {
                    Name = commandInfo.CommandName,
                    Description = commandInfo.Description,
                    Options = options,
                    DefaultPermission = commandInfo.DefaultPermission
                });
            }


            return applicationCommandParams;
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
            if (commandAttribute is not null)
            {
                var commandInfo = new SlashCommandInfo(commandAttribute.Name, commandAttribute.Description, commandAttribute.DefaultPermission, validMethod, parentModule)
                {
                    Guilds = _guildBuildService.GetCommandGuilds(validMethod),
                    CommandOptions = _optionBuildService.GetCommandOptions(validMethod).ToList(),
                    Requirements = _requirementBuildService.GetCommandRequirements(validMethod),
                    Permissions = _guildBuildService.GetCommandGuildPermissions(validMethod)
                };
                return new KeyValuePair<string, ISlashCommandInfo>(commandAttribute.Name, commandInfo);
            }

            _logger.LogWarning("Can not load command {CommandName} since it doesn't have the SlashCommandAttribute attribute", validMethod.Name);
            return null;
        }

        private KeyValuePair<string, ISlashCommandInfo> BuildCommandGroupInfoKeyValuePair(SlashCommandGroupAttribute groupAttribute, TypeInfo parentModule)
        {
            // Build the command group.
            var commandGroup = new SlashCommandInfo(groupAttribute.Name, groupAttribute.Description, groupAttribute.DefaultPermission, parentModule)
            {
                CommandOptions = new List<ISlashCommandOptionInfo>(),
                Guilds = _guildBuildService.GetCommandGuilds(parentModule),
                Permissions = _guildBuildService.GetCommandGuildPermissions(parentModule)
            };
            var rawValidCommands = GetValidSubSlashCommandsMethods(parentModule);

            // Get all the sub commands.
            foreach (var rawValidCommand in rawValidCommands)
            {
                var subCommandGroupAttribute = rawValidCommand.GetCustomAttribute<SlashCommandGroupAttribute>();
                var subCommandAttribute = rawValidCommand.GetCustomAttribute<SlashCommandAttribute>();

                if (subCommandAttribute is null)
                {
                    _logger.LogWarning("Can not load command {CommandName} since it doesn't have the SlashCommandAttribute attribute", rawValidCommand.Name);
                    continue;
                }

                if (_guildBuildService.GetCommandGuilds(rawValidCommand, false).Any())
                    throw new InvalidGuildSlashCommandException("A sub command can not be set to a specific guild. Add the attribute to the command module instead.");

                // Build the sub command.
                var commandRequirements = _requirementBuildService.GetCommandRequirements(rawValidCommand);
                var options = _optionBuildService.GetCommandOptions(rawValidCommand);
                var subCommand = new SlashCommandOptionInfo(subCommandAttribute.Name,
                                                            subCommandAttribute.Description,
                                                            rawValidCommand,
                                                            parentModule,
                                                            commandRequirements,
                                                            options.ToList());

                // Check if the command doesn't belong to a sub command group.
                if (subCommandGroupAttribute is null)
                {
                    commandGroup.CommandOptions.Add(subCommand);
                    continue;
                }

                // The command belongs to a sub command group.
                // Check if the sub command group already exists.
                var existingGroup = commandGroup.CommandOptions.FirstOrDefault(x => x.Name.Equals(subCommandGroupAttribute.Name));
                if (existingGroup is not null)
                {
                    // Command group does exist.
                    existingGroup.CommandOptions ??= new List<ISlashCommandOptionInfo>();
                    existingGroup.CommandOptions.Add(subCommand);
                    continue;
                }

                // Command group does not exist.
                var subGroup = new SlashCommandOptionInfo(subCommandGroupAttribute.Name, subCommandGroupAttribute.Description, DiscordApplicationCommandOptionType.SubCommandGroup)
                {
                    CommandOptions = new List<ISlashCommandOptionInfo>
                    {
                        subCommand
                    }
                };

                commandGroup.CommandOptions.Add(subGroup);
            }

            return new KeyValuePair<string, ISlashCommandInfo>(commandGroup.CommandName, commandGroup);
        }

        /// <summary>
        ///     Get a <see cref="IEnumerable{T}" /> of <see cref="MethodInfo" />s containing only valid commands methods.
        /// </summary>
        /// <param name="parentModule">The class that contains the slash commands.</param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="MethodInfo" />s containing only valid commands methods.
        /// </returns>
        private IEnumerable<MethodInfo> GetValidSlashCommandsMethods(Type parentModule)
        {
            if (IsValidCommandGroupModuleDefinition(parentModule)) return new List<MethodInfo>();

            return parentModule
                   .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                   .Where(IsValidCommandDefinition);
        }

        /// <summary>
        ///     Get a <see cref="IEnumerable{T}" /> of <see cref="MethodInfo" />s containing only valid sub commands methods.
        /// </summary>
        /// <param name="parentModule">The class that contains the sub slash commands.</param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="MethodInfo" />s containing only sub valid commands methods.
        /// </returns>
        private IEnumerable<MethodInfo> GetValidSubSlashCommandsMethods(Type parentModule)
        {
            if (!IsValidCommandGroupModuleDefinition(parentModule)) return new List<MethodInfo>();

            return parentModule
                   .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                   .Where(IsValidCommandDefinition);
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
            return methodInfo.IsDefined(typeof(SlashCommandAttribute))
                   && methodInfo.ReturnType == typeof(Task<IDiscordInteractionResponse>)
                   && !methodInfo.IsStatic
                   && !methodInfo.IsGenericMethod;
        }

        /// <summary>
        ///     Checks whether or not a command group is valid.
        /// </summary>
        /// <param name="parentModule">The class that contains the slash commands.</param>
        /// <returns>
        ///     Whether or not a command group is valid.
        /// </returns>
        private static bool IsValidCommandGroupModuleDefinition(Type parentModule)
        {
            return parentModule.IsDefined(typeof(SlashCommandGroupAttribute))
                   && !parentModule.IsGenericType;
        }
    }
}