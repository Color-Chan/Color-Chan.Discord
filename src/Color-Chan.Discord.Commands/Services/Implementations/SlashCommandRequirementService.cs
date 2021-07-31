using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Core;
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
        public async Task<List<string>> ExecuteSlashCommandRequirementsAsync(ISlashCommandInfo commandInfo, ISlashCommandContext context, IServiceProvider serviceProvider)
        {
            List<string> errorMessages = new();

            if (commandInfo.Requirements is not null)
            {
                foreach (var requirement in commandInfo.Requirements)
                {
                    var result = await requirement.CheckRequirementAsync(context, serviceProvider).ConfigureAwait(false);

                    if (result.Passed) continue;

                    _logger.LogDebug("Failed to pass requirement {RequirementName}", requirement.GetType().Name);
                    errorMessages.Add(result.ErrorMessage ?? string.Empty);
                }
            }

            return errorMessages;
        }
    }
}