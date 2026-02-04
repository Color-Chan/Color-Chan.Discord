using System;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Commands.InteractionHandlers;

/// <summary>
///     This handler is used to handle <see cref="DiscordInteractionRequestType.MessageComponent"/> interactions.
/// </summary>
public class MessageComponentRequestHandler(
    ILogger<MessageComponentRequestHandler> logger,
    IOptions<ComponentInteractionConfiguration> config,
    IDiscordRestGuild restGuild,
    IDiscordRestChannel restChannel,
    IDiscordRestApplication restApplication,
    IComponentService componentService,
    IServiceProvider serviceProvider
) : BaseInteractionHandler(
        config.Value.SendDefaultErrorMessage,
        config.Value.EnableAutoGetGuild,
        config.Value.EnableAutoGetChannel,
        restGuild,
        restChannel,
        restApplication,
        logger
    ),
    IInteractionHandler
{
    /// <inheritdoc />
    public bool CanHandle(IDiscordInteraction interaction)
    {
        return interaction.RequestType == DiscordInteractionRequestType.MessageComponent;
    }

    /// <inheritdoc />
    public async Task<InternalInteractionResponse> HandleInteractionAsync(IDiscordInteraction interaction)
    {
        var context = new ComponentContext(await GetInteractionContextAsync(interaction).ConfigureAwait(false));
        var customId = GetCustomId(interaction, context);

        var componentInfo = componentService.SearchComponent(customId);
        if (componentInfo is null)
        {
            throw new NullReferenceException($"Failed to find the requested interaction component {interaction.Data?.CustomId}");
        }

        var acknowledged = false;
        if (componentInfo.Acknowledge || componentInfo.EditOriginalMessage)
        {
            var callbackType = componentInfo.EditOriginalMessage ? DiscordInteractionCallbackType.DeferredUpdateMessage : DiscordInteractionCallbackType.DeferredChannelMessageWithSource;
            acknowledged = await AcknowledgedIfRequiredAsync(interaction, callbackType).ConfigureAwait(false);
        }

        async Task<Result<IDiscordInteractionResponse>> Handler()
        {
            return await componentService.ExecuteComponentInteractionAsync(componentInfo, context, serviceProvider).ConfigureAwait(false);
        }

        // Execute the pipelines and the component interaction.
        var result = await serviceProvider
            .GetServices<IInteractionPipeline>()
            .Aggregate((InteractionHandlerDelegate)Handler, (next, pipeline) => () => pipeline.HandleAsync(context, next))()
            .ConfigureAwait(false);

        return GetInternalInteractionResponse(result, acknowledged, interaction.Id);
    }

    private string GetCustomId(IDiscordInteraction interaction, IComponentContext context)
    {
        var customId = context.Data.CustomId!;
        if (!customId.Contains(config.Value.CustomIdDataSeparator))
        {
            return customId;
        }

        logger.LogDebug("Interaction: {Id} : Parsing custom id arguments", interaction.Id);
        var customIdData = context.Data.CustomId!.Split(config.Value.CustomIdDataSeparator).ToList();
        customId = customIdData.First();

        customIdData.RemoveAt(0);
        context.Args.AddRange(customIdData);

        return customId;
    }
}