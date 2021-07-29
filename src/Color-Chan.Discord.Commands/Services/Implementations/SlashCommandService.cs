using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Commands.Modules;
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
        private SlashCommandConfiguration? _configurations;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandService" />.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" /> for <see cref="SlashCommandService" />.</param>
        /// <param name="slashCommandBuildService">
        ///     The <see cref="ISlashCommandBuildService" /> that will get and build the
        ///     commands.
        /// </param>
        /// <param name="requirementService">The <see cref="ISlashCommandRequirementService" /> that will used to execute the requirements.</param>
        /// <param name="commandAutoSyncService">The <see cref="ISlashCommandAutoSyncService"/> that handles all the syncing of the slash commands.</param>
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
            _logger.LogDebug("Loading interaction commands");

            // Build all commands in a specific assembly.
            var commandInfos = _slashCommandBuildService.BuildSlashCommandInfos(assembly);
            foreach (var (key, commandInfo) in commandInfos)
                if (!_slashCommands.TryAdd(key.ToLower(), commandInfo))
                    throw new Exception($"Failed to register {commandInfo.CommandName}");

            _logger.LogInformation("Finished adding {Count} commands to the command registry", _slashCommands.Count.ToString());

            // Default config if no config was set.
            _configurations ??= new SlashCommandConfiguration();

            var result = await _commandAutoSyncService.UpdateApplicationCommandsAsync(commandInfos.Select(x => x.Value), _configurations).ConfigureAwait(false);

            if (!result.IsSuccessful) throw new UpdateSlashCommandException(result.ErrorResult?.ErrorMessage ?? "Failed to sync the slash command to discord.");
        }

        /// <inheritdoc />
        public async Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(ISlashCommandInfo commandInfo, ISlashCommandContext context, IServiceProvider? serviceProvider = null)
        {
            serviceProvider ??= DefaultServiceProvider.Instance;

            var instance = GetSlashCommandModuleInstance(serviceProvider, commandInfo.CommandMethod);
            instance.SetContext(context);

            var requirementErrors = await _requirementService.ExecuteSlashCommandRequirementsAsync(commandInfo, context, serviceProvider).ConfigureAwait(false);
            if (requirementErrors.Any()) return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult(string.Join(", ", requirementErrors)));

            // Try to execute the command.
            try
            {
                if (commandInfo.CommandMethod.Invoke(instance, null) is not Task<IDiscordInteractionResponse> task)
                {
                    _logger.LogWarning("Failed to cast {MethodName} to Task<IDiscordInteractionResponse>", commandInfo.CommandMethod.Name);
                    return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult("Failed to cast command to Task<IDiscordInteractionResponse>"));
                }

                var result = await task.ConfigureAwait(false);
                return Result<IDiscordInteractionResponse>.FromSuccess(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown while running command {CommandName}", commandInfo.CommandMethod.Name);
                return Result<IDiscordInteractionResponse>.FromError(default, new ExceptionResult(e.InnerException!));
            }
        }

        /// <inheritdoc />
        public async Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(string name, ISlashCommandContext context, IServiceProvider? serviceProvider = null)
        {
            var commandInfo = SearchSlashCommand(name);
            if (commandInfo is null) return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult("Failed to find command."));
            return await ExecuteSlashCommandAsync(commandInfo, context, serviceProvider).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public ISlashCommandInfo? SearchSlashCommand(string name)
        {
            return _slashCommands.TryGetValue(name.ToLower(), out var commandInfo) ? commandInfo : null;
        }

        /// <inheritdoc />
        public SlashCommandConfiguration Configure(SlashCommandConfiguration options)
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
                throw new ModuleCastNullReferenceException("Failed to cast module to IInteractionCommandModuleBase");

            return instance;
        }
    }
}