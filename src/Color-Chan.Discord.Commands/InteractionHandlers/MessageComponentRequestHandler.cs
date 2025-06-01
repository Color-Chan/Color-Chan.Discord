using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Commands.InteractionHandlers;

/// <summary>
///     This handler is used to handle <see cref="DiscordInteractionRequestType.MessageComponent"/> interactions.
/// </summary>
public class MessageComponentRequestHandler(
    ILogger<ApplicationCommandRequestHandler> logger,
    IOptions<SlashCommandConfiguration> slashCommandConfiguration,
    IDiscordRestGuild restGuild,
    IDiscordRestChannel restChannel,
    IDiscordRestApplication restApplication,
    IServiceProvider serviceProvider
) : BaseInteractionHandler(slashCommandConfiguration, restGuild, restChannel, restApplication, logger), IInteractionHandler
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
        
        throw new NotImplementedException();
    }
}