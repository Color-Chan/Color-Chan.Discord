using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Commands.Services.Builders;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations
{
    /// <inheritdoc />
    public class ComponentService : IComponentService
    {
        private readonly IComponentBuildService _componentBuildService;
        private readonly ConcurrentDictionary<string, IComponentInfo> _components = new();
        private readonly ILogger<ComponentService> _logger;

        /// <summary>
        ///     Initializes a new instance of <see cref="ComponentService" />.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" /> for <see cref="ComponentService" />.</param>
        /// <param name="componentBuildService">The build service that will build the <see cref="IComponentInfo" />s.</param>
        public ComponentService(ILogger<ComponentService> logger, IComponentBuildService componentBuildService)
        {
            _logger = logger;
            _componentBuildService = componentBuildService;
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
    }
}