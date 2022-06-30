using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations;

/// <inheritdoc />
public class InteractionRequirementService : ISlashCommandRequirementService
{
    private readonly ILogger<InteractionRequirementService> _logger;

    /// <summary>
    ///     Initializes a new instance of <see cref="InteractionRequirementService" />.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger" /> for <see cref="InteractionRequirementService" />.</param>
    public InteractionRequirementService(ILogger<InteractionRequirementService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<Result> ExecuteRequirementsAsync(IEnumerable<InteractionRequirementAttribute>? requirements, IInteractionContext context, IServiceProvider serviceProvider)
    {
        if (requirements is null)
        {
            return Result.FromSuccess();
        }

        _logger.LogDebug("Interaction {Id} : Executing interaction requirements", context.InteractionId.ToString());

        foreach (var requirement in requirements)
        {
            var result = await requirement.CheckRequirementAsync(context, serviceProvider).ConfigureAwait(false);

            if (result.IsSuccessful) continue;

            _logger.LogDebug("Interaction {Id} : Failed to pass requirement {RequirementName}", context.InteractionId.ToString(), requirement.GetType().Name);
            return Result.FromError(result.ErrorResult!);
        }

        return Result.FromSuccess();
    }
}