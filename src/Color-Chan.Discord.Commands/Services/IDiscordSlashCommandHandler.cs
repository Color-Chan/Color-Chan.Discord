using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Services
{
    /// <summary>
    ///     Handles all incoming slash command requests.
    /// </summary>
    public interface IDiscordSlashCommandHandler
    {
        /// <summary>
        ///     Registers an error handler middleware.
        /// </summary>
        /// <param name="errorHandler">The new error handler middleware.</param>
        void RegisterErrorHandler(Func<IErrorResult, Task<Result<IDiscordInteractionResponse>>> errorHandler);

        /// <summary>
        ///     Handles a interaction slash command request.
        /// </summary>
        /// <param name="interaction">The interaction that was used.</param>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse" /> containing the result of the slash command.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <see cref="IDiscordInteraction.Data" /> is null.</exception>
        Task<IDiscordInteractionResponse> HandleSlashCommandAsync(IDiscordInteraction interaction);
    }
}