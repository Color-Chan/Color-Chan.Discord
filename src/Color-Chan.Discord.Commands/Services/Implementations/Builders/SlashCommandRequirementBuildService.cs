using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Services.Builders;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations.Builders
{
    /// <inheritdoc />
    public class SlashCommandRequirementBuildService : ISlashCommandRequirementBuildService
    {
        private readonly ILogger<SlashCommandRequirementBuildService> _logger;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRequirementBuildService" />.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" /> for <see cref="SlashCommandRequirementBuildService" />.</param>
        public SlashCommandRequirementBuildService(ILogger<SlashCommandRequirementBuildService> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public IEnumerable<InteractionRequirementAttribute> GetCommandRequirements(MethodInfo command)
        {
            var attributes = new List<InteractionRequirementAttribute>();

            var parentAttributes = command.DeclaringType?.GetCustomAttributes<InteractionRequirementAttribute>();
            if (parentAttributes != null) attributes.AddRange(parentAttributes);

            var methodAttributes = command.GetCustomAttributes<InteractionRequirementAttribute>();
            attributes.AddRange(methodAttributes);

            _logger.LogDebug("Found {Count} requirements for command {MethodName}", attributes.Count.ToString(), command.Name);
            return attributes;
        }
    }
}