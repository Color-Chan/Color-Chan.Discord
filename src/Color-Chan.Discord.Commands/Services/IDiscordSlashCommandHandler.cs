using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Commands.Services;

/// <summary>
///     Handles all incoming slash command requests.
/// </summary>
public interface IDiscordSlashCommandHandler
{
    /// <summary>
    ///     Handles a interaction slash command request.
    /// </summary>
    /// <param name="interaction">The interaction that was used.</param>
    /// <returns>
    ///     A <see cref="InternalInteractionResponse" /> containing the result of the slash command.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <see cref="IDiscordInteraction.Data" /> is null.</exception>
    Task<InternalInteractionResponse> HandleSlashCommandAsync(IDiscordInteraction interaction);
}