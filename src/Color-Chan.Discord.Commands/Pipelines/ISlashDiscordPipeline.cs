using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Pipelines
{
    /// <summary>
    ///     Pipeline behavior to surround the the slash command handler.
    ///     Implementations add additional behavior and await the next delegate.
    /// </summary>
    public interface ISlashDiscordPipeline
    {
        /// <summary>
        ///     A pipeline handler to surround the inner command handler.
        /// </summary>
        /// <param name="context">The current <see cref="ISlashCommandContext"/> of the command request.</param>
        /// <param name="next">The delegate for the next action in the pipeline. This will execute the command if all pipelines were executed.</param>
        /// <returns>
        ///     <see cref="Result{T}"/> of <see cref="IDiscordInteractionResponse"/> containing the slash command result.
        /// </returns>
        Task<Result<IDiscordInteractionResponse>> HandleAsync(ISlashCommandContext context, SlashCommandHandlerDelegate next);
    }
    
    /// <summary>
    ///     Represents an async continuation for the next task to execute in the pipeline.
    /// </summary>
    public delegate Task<Result<IDiscordInteractionResponse>> SlashCommandHandlerDelegate();
}