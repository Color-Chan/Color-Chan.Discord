using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Commands.Services
{
    /// <summary>
    ///     Handles all incoming component interactions requests.
    /// </summary>
    public interface IComponentInteractionHandler
    {
        /// <summary>
        ///     Handles a component interaction request.
        /// </summary>
        /// <param name="interaction">The <see cref="IDiscordInteraction"/> that was requested.</param>
        /// <returns>
        ///     A <see cref="InternalInteractionResponse" /> containing the result of the component interaction.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="IDiscordInteraction.Data"/> is null.</exception>
        /// <exception cref="NullReferenceException">Thrown when no user or member was found.</exception>
        /// <exception cref="ComponentInteractionResultException">Thrown when no channel id was found.</exception>
        Task<InternalInteractionResponse> HandleComponentInteractionAsync(IDiscordInteraction interaction);
    }
}