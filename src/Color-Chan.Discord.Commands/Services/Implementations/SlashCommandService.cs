using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Commands.Services.Builders;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations
{
    /// <inheritdoc />
    public class SlashCommandService : ISlashCommandService
    {
        private readonly ISlashCommandAutoSyncService _commandAutoSyncService;
        private readonly ILogger<SlashCommandService> _logger;
        private readonly ISlashCommandRequirementService _requirementService;
        private readonly ISlashCommandBuildService _slashCommandBuildService;
        private readonly ConcurrentDictionary<string, ISlashCommandInfo> _slashCommands = new();
        private InteractionsConfiguration? _configurations;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandService" />.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" /> for <see cref="SlashCommandService" />.</param>
        /// <param name="slashCommandBuildService">
        ///     The <see cref="ISlashCommandBuildService" /> that will get and build the
        ///     commands.
        /// </param>
        /// <param name="requirementService">
        ///     The <see cref="ISlashCommandRequirementService" /> that will used to execute the
        ///     requirements.
        /// </param>
        /// <param name="commandAutoSyncService">
        ///     The <see cref="ISlashCommandAutoSyncService" /> that handles all the syncing of
        ///     the slash commands.
        /// </param>
        public SlashCommandService(ILogger<SlashCommandService> logger, ISlashCommandBuildService slashCommandBuildService,
                                   ISlashCommandRequirementService requirementService, ISlashCommandAutoSyncService commandAutoSyncService)
        {
            _logger = logger;
            _slashCommandBuildService = slashCommandBuildService;
            _requirementService = requirementService;
            _commandAutoSyncService = commandAutoSyncService;
        }

        /// <inheritdoc />
        public async Task AddInteractionCommandsAsync(Assembly assembly)
        {
            _logger.LogDebug("Registering slash commands...");

            // Build all commands in a specific assembly.
            var commandInfos = _slashCommandBuildService.BuildSlashCommandInfos(assembly);
            foreach (var (key, commandInfo) in commandInfos)
            {
                if (_slashCommands.TryAdd(key, commandInfo)) continue;

                // The command already existed.
                var registeringException = new Exception($"Failed to register {commandInfo.CommandName}");
                _logger.LogError(registeringException, "Can not register multiple commands with the same name");
                throw registeringException;
            }

            _logger.LogInformation("Registered {Count} slash commands to the command registry", _slashCommands.Count.ToString());

            // Default config if no config was set.
            _configurations ??= new InteractionsConfiguration();

            var result = await _commandAutoSyncService.UpdateApplicationCommandsAsync(commandInfos.Select(x => x.Value), _configurations).ConfigureAwait(false);

            if (!result.IsSuccessful) throw new UpdateSlashCommandException(result.ErrorResult?.ErrorMessage ?? "Failed to sync the slash command to discord.");
        }

        /// <inheritdoc />
        public async Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(MethodInfo commandMethod,
                                                                                        IEnumerable<ISlashCommandOptionInfo>? options,
                                                                                        IEnumerable<SlashCommandRequirementAttribute>? requirements,
                                                                                        ISlashCommandContext context,
                                                                                        List<IDiscordInteractionCommandOption>? suppliedOptions = null,
                                                                                        IServiceProvider? serviceProvider = null)
        {
            serviceProvider ??= DefaultServiceProvider.Instance;

            var instance = GetSlashCommandModuleInstance(serviceProvider, commandMethod);
            instance.SetContext(context);

            // Try to run the requirements for the slash command.
            try
            {
                var requirementsResult = await _requirementService.ExecuteSlashCommandRequirementsAsync(requirements, context, serviceProvider).ConfigureAwait(false);
                if (!requirementsResult.IsSuccessful)
                {
                    return Result<IDiscordInteractionResponse>.FromError(default, requirementsResult.ErrorResult);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown while running command {CommandName} requirements", commandMethod.Name);
                return Result<IDiscordInteractionResponse>.FromError(default, new ExceptionResult(e.InnerException!));
            }

            // Get the arguments from the given options.
            var args = new List<object?>();
            if (options is not null)
            {
                foreach (var commandOption in options)
                {
                    var userOption = suppliedOptions?.FirstOrDefault(x => x.Name == commandOption.Name);

                    // Add the argument value if one was supplied or add null.
                    args.Add(userOption?.Value);
                }
            }

            // Try to execute the command.
            try
            {
                if (commandMethod.Invoke(instance, args.ToArray()) is not Task<IDiscordInteractionResponse> task)
                {
                    _logger.LogWarning("Failed to cast {MethodName} to Task<IDiscordInteractionResponse>", commandMethod.Name);
                    return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult("Failed to cast command to Task<IDiscordInteractionResponse>"));
                }

                var result = await task.ConfigureAwait(false);
                return Result<IDiscordInteractionResponse>.FromSuccess(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown while running command {CommandName}", commandMethod.Name);
                return Result<IDiscordInteractionResponse>.FromError(default, new ExceptionResult(e.InnerException!));
            }
        }

        /// <inheritdoc />
        public async Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(ISlashCommandInfo commandInfo, ISlashCommandContext context,
                                                                                        List<IDiscordInteractionCommandOption>? suppliedOptions = null, IServiceProvider? serviceProvider = null)
        {
            if (commandInfo.CommandMethod is not null)
            {
                return await ExecuteSlashCommandAsync(commandInfo.CommandMethod, commandInfo.CommandOptions, commandInfo.Requirements, context, suppliedOptions, serviceProvider).ConfigureAwait(false);
            }

            _logger.LogWarning("Failed to executed {Name} since it was a command group or a sub command group", commandInfo.CommandName);
            return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult("Can not execute command group or sub command group"));
        }

        /// <inheritdoc />
        public async Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(ISlashCommandOptionInfo commandOptionInfo, ISlashCommandContext context,
                                                                                        List<IDiscordInteractionCommandOption>? suppliedOptions = null, IServiceProvider? serviceProvider = null)
        {
            if (commandOptionInfo.CommandMethod is not null)
            {
                return await ExecuteSlashCommandAsync(commandOptionInfo.CommandMethod,
                                                      commandOptionInfo.CommandOptions,
                                                      commandOptionInfo.Requirements,
                                                      context,
                                                      suppliedOptions,
                                                      serviceProvider).ConfigureAwait(false);
            }

            _logger.LogWarning("Failed to executed {Name} since it was a command group or a sub command group", commandOptionInfo.Name);
            return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult("Can not execute command group or sub command group"));
        }

        /// <inheritdoc />
        public async Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(string name, ISlashCommandContext context, IEnumerable<IDiscordInteractionCommandOption>? options = null,
                                                                                        IServiceProvider? serviceProvider = null)
        {
            var commandInfo = SearchSlashCommand(name);

            if (commandInfo is null)
            {
                _logger.LogWarning("Failed to find slash command {Name}", name);
                return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult($"Failed to find command {name}"));
            }

            if (commandInfo.CommandMethod is null)
            {
                _logger.LogWarning("Failed to executed {Name} since it was a command group or a sub command group", commandInfo.CommandName);
                return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult("Can not execute command group or sub command group"));
            }

            return await ExecuteSlashCommandAsync(commandInfo, context, options?.ToList(), serviceProvider).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(string commandGroupName, string commandName, ISlashCommandContext context,
                                                                                        IEnumerable<IDiscordInteractionCommandOption>? options = null, IServiceProvider? serviceProvider = null)
        {
            var searchResult = SearchSlashCommand(commandGroupName, commandName);

            if (searchResult is null)
            {
                _logger.LogWarning("Command {GroupName} {CommandName} is not registered", commandGroupName, commandName);
                return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult($"Failed to find command {commandGroupName} {commandName}"));
            }

            return await ExecuteSlashCommandAsync(searchResult, context, options?.ToList(), serviceProvider).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(string commandGroupName, string subCommandGroupName, string commandName, ISlashCommandContext context,
                                                                                        IEnumerable<IDiscordInteractionCommandOption>? options = null, IServiceProvider? serviceProvider = null)
        {
            var searchResult = SearchSlashCommand(commandGroupName, subCommandGroupName, commandName);

            if (searchResult is null)
            {
                _logger.LogWarning("Command {GroupName} {SubCommandGroupName} {CommandName} is not registered", commandGroupName, subCommandGroupName, commandName);
                return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult($"Failed to find command {commandGroupName} {subCommandGroupName} {commandName}"));
            }

            return await ExecuteSlashCommandAsync(searchResult, context, options?.ToList(), serviceProvider).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public ISlashCommandInfo? SearchSlashCommand(string name)
        {
            return _slashCommands.TryGetValue(name, out var commandInfo) ? commandInfo : null;
        }

        /// <inheritdoc />
        public ISlashCommandOptionInfo? SearchSlashCommand(string groupName, string subCommandName)
        {
            var commandGroupInfo = _slashCommands.TryGetValue(groupName, out var tempCommandInfo) ? tempCommandInfo : null;
            var subCommand = commandGroupInfo?.CommandOptions?.FirstOrDefault(x => x.Name == subCommandName);
            return subCommand;
        }

        /// <inheritdoc />
        public ISlashCommandOptionInfo? SearchSlashCommand(string groupName, string subCommandGroupName, string subCommandName)
        {
            var commandGroupInfo = _slashCommands.TryGetValue(groupName, out var tempCommandInfo) ? tempCommandInfo : null;
            var subCommandGroupInfo = commandGroupInfo?.CommandOptions?.FirstOrDefault(x => x.Name == subCommandGroupName);
            var subCommand = subCommandGroupInfo?.CommandOptions?.FirstOrDefault(x => x.Name == subCommandName);
            return subCommand;
        }

        /// <inheritdoc />
        public InteractionsConfiguration Configure(InteractionsConfiguration options)
        {
            _configurations = options;
            return options;
        }

        /// <summary>
        ///     Get a new instance of a <see cref="ISlashCommandModule" /> with its required dependencies.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider" /> containing the required dependencies.</param>
        /// <param name="command">The command method that will be executed.</param>
        /// <returns>
        ///     A new instance of the <see cref="ISlashCommandModule" />.
        /// </returns>
        /// <exception cref="ModuleCastNullReferenceException"></exception>
        private ISlashCommandModule GetSlashCommandModuleInstance(IServiceProvider serviceProvider, MemberInfo command)
        {
            if (ActivatorUtilities.CreateInstance(serviceProvider, command.DeclaringType!) is not ISlashCommandModule instance)
                throw new ModuleCastNullReferenceException("Failed to cast command module to IInteractionCommandModuleBase");

            return instance;
        }
    }
}