using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands;

/// <summary>
///     Pipeline behavior to surround the the slash command handler.
///     Implementations add additional behavior and await the next delegate.
/// </summary>
/// <example>
///     The following example showcases a simple implementation for a pipeline that measures
///     the performance of the slash commands.
///     <code language="cs">
///     public class PerformancePipeline : ISlashCommandPipeline
///     {
///         public async Task&lt;Result&lt;IDiscordInteractionResponse&gt;&gt; HandleAsync(ISlashCommandContext context, SlashCommandHandlerDelegate next)
///         {
///             var sw = new Stopwatch();
/// 
///             sw.Start();
///             var result = await next().ConfigureAwait(false);
///             sw.Stop();
/// 
///             Console.WriteLine($"Command {context.MethodName} ran for {sw.ElapsedMilliseconds.ToString()}ms.");
///             return result;
///         }
///     }
///     </code>
/// </example>
public interface ISlashCommandPipeline
{
    /// <summary>
    ///     A pipeline handler to surround the inner command handler.
    /// </summary>
    /// <param name="context">The current <see cref="ISlashCommandContext" /> of the command request.</param>
    /// <param name="next">
    ///     The delegate for the next action in the pipeline. This will execute the command if all pipelines
    ///     have been executed.
    /// </param>
    /// <returns>
    ///     <see cref="Result{T}" /> of <see cref="IDiscordInteractionResponse" /> containing the slash command result.
    /// </returns>
    Task<Result<IDiscordInteractionResponse>> HandleAsync(ISlashCommandContext context, SlashCommandHandlerDelegate next);
}

/// <summary>
///     Represents an async continuation for the next task to execute in the pipeline.
/// </summary>
public delegate Task<Result<IDiscordInteractionResponse>> SlashCommandHandlerDelegate();