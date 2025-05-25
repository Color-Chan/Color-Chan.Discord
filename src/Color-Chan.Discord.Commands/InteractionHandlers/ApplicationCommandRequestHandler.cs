using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Commands.InteractionHandlers;

/// <summary>
///     This handler is used to handle <see cref="DiscordInteractionRequestType.ApplicationCommand"/> interactions.
/// </summary>
public class ApplicationCommandRequestHandler(
    IOptions<SlashCommandConfiguration> slashCommandConfiguration,
    IDiscordRestGuild restGuild,
    IDiscordRestChannel restChannel
) : BaseInteractionHandler(slashCommandConfiguration, restGuild, restChannel), IInteractionHandler
{
    /// <inheritdoc />
    public bool CanHandle(IDiscordInteraction interaction)
    {
        return interaction.RequestType == DiscordInteractionRequestType.ApplicationCommand;
    }

    /// <inheritdoc />
    public async Task<InternalInteractionResponse> HandleInteractionAsync(IDiscordInteraction interaction)
    {
        var guild = await GetGuildAsync(interaction).ConfigureAwait(false);
        var channel = await GetChannelAsync(interaction).ConfigureAwait(false);
        
        throw new NotImplementedException();
    }
}