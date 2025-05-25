using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Commands.InteractionHandlers;

/// <summary>
///     An interface that represents an interaction handler.
/// </summary>
public interface IInteractionHandler
{
    /// <summary>
    ///     Whether the handler can handle the given interaction.
    /// </summary>
    /// <param name="interaction">The interaction that will be checked.</param>
    /// <returns>
    ///     True if the handler can handle the interaction, false otherwise.
    /// </returns>
    bool CanHandle(IDiscordInteraction interaction);

    /// <summary>
    ///     Handles the interaction and returns an <see cref="InternalInteractionResponse" />.
    /// </summary>
    /// <param name="interaction">The interaction that will be handled.</param>
    /// <returns>
    ///     The <see cref="InternalInteractionResponse" /> that contains the response to be sent back to Discord.
    /// </returns>
    Task<InternalInteractionResponse> HandleInteractionAsync(IDiscordInteraction interaction);
}