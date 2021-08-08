using System;
using Color_Chan.Discord.Commands.Contexts;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Modules
{
    /// <inheritdoc />
    public class SlashCommandModule : ISlashCommandModule
    {
        /// <summary>
        ///     The current context the the slash command.
        /// </summary>
        public ISlashCommandContext SlashContext { get; set; } = null!;

        /// <inheritdoc />
        public void SetContext(ISlashCommandContext slashContext)
        {
            SlashContext = slashContext ?? throw new ArgumentNullException(nameof(slashContext));
        }

        /// <summary>
        ///     Create a result object with a response.
        /// </summary>
        /// <param name="response">The response that will be send to discord.</param>
        /// <returns>
        ///     <see cref="Result{T}"/> of <see cref="IDiscordInteractionResponse"/> containing the response.
        /// </returns>
        public static Result<IDiscordInteractionResponse> FromSuccess(IDiscordInteractionResponse response)
        {
            return Result<IDiscordInteractionResponse>.FromSuccess(response);
        }
        
        /// <summary>
        ///     Create a result object describing an error.
        /// </summary>
        /// <param name="errorResult">The <see cref="ErrorResult"/> containing the details of the error.</param>
        /// <returns>
        ///     <see cref="Result{T}"/> of <see cref="IDiscordInteractionResponse"/> containing the error result..
        /// </returns>
        public static Result<IDiscordInteractionResponse> FromError(ErrorResult errorResult)
        {
            return Result<IDiscordInteractionResponse>.FromError(default, errorResult);
        }
    }
}