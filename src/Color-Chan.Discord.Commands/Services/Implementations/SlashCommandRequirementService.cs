using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations
{
    /// <inheritdoc />
    public class SlashCommandRequirementService : ISlashCommandRequirementService
    {
        private readonly ILogger<SlashCommandRequirementService> _logger;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRequirementService" />.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" /> for <see cref="SlashCommandRequirementService" />.</param>
        public SlashCommandRequirementService(ILogger<SlashCommandRequirementService> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<Result> ExecuteSlashCommandRequirementsAsync(IEnumerable<SlashCommandRequirementAttribute>? requirements, ISlashCommandContext context, IServiceProvider serviceProvider)
        {
            if (requirements is null)
            {
                return Result.FromSuccess();
            }
            
            foreach (var requirement in requirements)
            {
                var result = await requirement.CheckRequirementAsync(context, serviceProvider).ConfigureAwait(false);

                if (result.IsSuccessful) continue;

                _logger.LogDebug("Failed to pass requirement {RequirementName}", requirement.GetType().Name);
                return Result.FromError(result.ErrorResult!);
            }

            return Result.FromSuccess();
        }
    }
}