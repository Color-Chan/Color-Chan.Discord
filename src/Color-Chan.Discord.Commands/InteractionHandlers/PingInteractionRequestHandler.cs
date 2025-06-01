using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Commands.InteractionHandlers;

/// <summary>
///     This handler is used to handle <see cref="DiscordInteractionRequestType.Ping"/> interactions.
/// </summary>
public class PingInteractionRequestHandler : IInteractionHandler
{
    /// <inheritdoc />
    public bool CanHandle(IDiscordInteraction interaction)
    {
        return interaction.RequestType == DiscordInteractionRequestType.Ping;
    }

    /// <inheritdoc />
    public Task<InternalInteractionResponse> HandleInteractionAsync(IDiscordInteraction interaction)
    {
        return Task.FromResult(InternalInteractionResponse.PingResponse());
    }
}