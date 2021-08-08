using System;
using Color_Chan.Discord.Commands.Contexts;
using Color_Chan.Discord.Core.Common.Models.Embed;
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
        ///     Create a result object from a <see cref="IDiscordInteractionResponse"/>.
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
        ///     Create a result object from a content string.
        /// </summary>
        /// <param name="content">The content of the message.</param>
        /// <param name="isPrivate">Whether or not the message should be private.</param>
        /// <returns>
        ///     <see cref="Result{T}"/> of <see cref="IDiscordInteractionResponse"/> containing the response.
        /// </returns>
        public static Result<IDiscordInteractionResponse> FromSuccess(string content, bool isPrivate = false)
        {
            // Build the response with the content.
            var responseBuilder = new SlashCommandResponseBuilder().WithContent(content);

            if (isPrivate) responseBuilder.MakePrivate();
            
            //  Return the response to Discord.
            return FromSuccess(responseBuilder.Build());
        }
        
        /// <summary>
        ///     Create a result object from a <see cref="IDiscordEmbed"/>.
        /// </summary>
        /// <param name="embed">The embed that will be returned to discord.</param>
        /// <param name="isPrivate">Whether or not the message should be private.</param>
        /// <returns>
        ///     <see cref="Result{T}"/> of <see cref="IDiscordInteractionResponse"/> containing the response.
        /// </returns>
        public static Result<IDiscordInteractionResponse> FromSuccess(IDiscordEmbed embed, bool isPrivate = false)
        {
            // Build the response with the embed.
            var responseBuilder = new SlashCommandResponseBuilder().WithEmbed(embed);
            
            //  Return the response to Discord.
            return FromSuccess(responseBuilder.Build());
        }
        
        /// <summary>
        ///     Create a result object describing an error.
        /// </summary>
        /// <param name="errorResult">The <see cref="ErrorResult"/> containing the details of the error.</param>
        /// <returns>
        ///     <see cref="Result{T}"/> of <see cref="IDiscordInteractionResponse"/> containing the error result.
        /// </returns>
        public static Result<IDiscordInteractionResponse> FromError(ErrorResult errorResult)
        {
            return Result<IDiscordInteractionResponse>.FromError(default, errorResult);
        }
        
        /// <summary>
        ///     Creates a result object with an <see cref="ErrorResult"/> containing the given <paramref name="errorMessage"/>.
        /// </summary>
        /// <param name="errorMessage">The error details.</param>
        /// <returns>
        ///     <see cref="Result{T}"/> of <see cref="IDiscordInteractionResponse"/> containing the error result.
        /// </returns>
        public static Result<IDiscordInteractionResponse> FromError(string errorMessage)
        {
            return Result<IDiscordInteractionResponse>.FromError(default, new ErrorResult(errorMessage));
        }
    }
}