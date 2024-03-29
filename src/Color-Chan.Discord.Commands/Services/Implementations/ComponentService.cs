using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Commands.Services.Builders;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations;

/// <inheritdoc />
public class ComponentService : IComponentService
{
    private readonly IComponentBuildService _componentBuildService;
    private readonly ConcurrentDictionary<string, IComponentInfo> _components = new();
    private readonly ILogger<ComponentService> _logger;
    private readonly ISlashCommandRequirementService _requirementService;

    /// <summary>
    ///     Initializes a new instance of <see cref="ComponentService" />.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger" /> for <see cref="ComponentService" />.</param>
    /// <param name="componentBuildService">The build service that will build the <see cref="IComponentInfo" />s.</param>
    /// <param name="requirementService">
    ///     The <see cref="ISlashCommandRequirementService" /> that will used to execute the
    ///     requirements.
    /// </param>
    public ComponentService(ILogger<ComponentService> logger, IComponentBuildService componentBuildService, ISlashCommandRequirementService requirementService)
    {
        _logger = logger;
        _componentBuildService = componentBuildService;
        _requirementService = requirementService;
    }

    /// <inheritdoc />
    public Task AddComponentsAsync(Assembly assembly)
    {
        _logger.LogDebug("Registering component interactions...");

        // Build all the component interactions.
        var componentInfos = _componentBuildService.BuildComponentInfos(assembly);
        foreach (var componentInfo in componentInfos)
        {
            if (_components.TryAdd(componentInfo.CustomId, componentInfo)) continue;

            // The component already existed.
            var registeringException = new Exception($"Failed to register component {componentInfo.CustomId}");
            _logger.LogError(registeringException, "Can not register multiple components with the same custom id");
            throw registeringException;
        }

        _logger.LogInformation("Registered {Count} components to the component registry", _components.Count.ToString());
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<Result<IDiscordInteractionResponse>> ExecuteComponentInteractionAsync(IComponentContext context, IServiceProvider serviceProvider)
    {
        if (context.Data.CustomId is null) throw new NullReferenceException(nameof(context.Data.CustomId));

        // Get the component.
        var searchResult = SearchComponent(context.Data.CustomId);
        if (searchResult is null)
            return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult($"Failed to find component with id {context.Data.CustomId}"));

        return await ExecuteComponentInteractionAsync(searchResult, context, serviceProvider).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Result<IDiscordInteractionResponse>> ExecuteComponentInteractionAsync(IComponentInfo componentInfo, IComponentContext context, IServiceProvider serviceProvider)
    {
        if (context.Data.CustomId is null) throw new NullReferenceException(nameof(context.Data.CustomId));

        _logger.LogInformation("Interaction: {Id} : Executing component {ComponentId}, args: {Args}", context.InteractionId.ToString(), componentInfo.CustomId, context.Args);

        // Validate the types.
        if (context.Data.ComponentType != componentInfo.Type)
        {
            var error = new ErrorResult($"The component types do not match for {context.Data.CustomId} {context.Data.ComponentType}:{componentInfo.Type}");
            return Result<IDiscordInteractionResponse>.FromError(default, error);
        }

        var instance = GetComponentInteractionModuleInstance(serviceProvider, componentInfo.ComponentMethod);
        context.MethodName = componentInfo.ComponentMethod.Name;
        instance.SetContext(context);

        // Try to run the requirements for the slash command.
        try
        {
            var requirementsResult = await _requirementService.ExecuteRequirementsAsync(componentInfo.Requirements, context, serviceProvider).ConfigureAwait(false);
            if (!requirementsResult.IsSuccessful)
            {
                return Result<IDiscordInteractionResponse>.FromError(default, requirementsResult.ErrorResult);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception thrown while running component interaction `{Name}` requirements", componentInfo.CustomId);
            return Result<IDiscordInteractionResponse>.FromError(default, new ExceptionResult(e.InnerException!));
        }

        // Try to execute the component interaction.
        try
        {
            // Todo: Pass down parsed arguments instead of having a string array on the context.
            if (componentInfo.ComponentMethod.Invoke(instance, null) is not Task<Result<IDiscordInteractionResponse>> task)
            {
                var errorMessage = $"Failed to cast {componentInfo.ComponentMethod.Name} to Task<Result<IDiscordInteractionResponse>>";
                // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                _logger.LogWarning(errorMessage);
                return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult(errorMessage));
            }

            return await task.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception thrown while running component interaction {Name}", componentInfo.ComponentMethod.Name);
            return Result<IDiscordInteractionResponse>.FromError(default, new ExceptionResult(e.InnerException ?? e));
        }
    }

    /// <inheritdoc />
    public IComponentInfo? SearchComponent(string customId)
    {
        return _components.TryGetValue(customId, out var componentInfo) ? componentInfo : null;
    }

    /// <summary>
    ///     Get a new instance of a <see cref="IComponentInteractionModule" /> with its required dependencies.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider" /> containing the required dependencies.</param>
    /// <param name="command">The command method that will be executed.</param>
    /// <returns>
    ///     A new instance of the <see cref="IComponentInteractionModule" />.
    /// </returns>
    /// <exception cref="ModuleCastNullReferenceException"></exception>
    private static IComponentInteractionModule GetComponentInteractionModuleInstance(IServiceProvider serviceProvider, MemberInfo command)
    {
        if (ActivatorUtilities.CreateInstance(serviceProvider, command.DeclaringType!) is not IComponentInteractionModule instance)
            throw new ModuleCastNullReferenceException("Failed to cast component module to IComponentInteractionModule");

        return instance;
    }
}