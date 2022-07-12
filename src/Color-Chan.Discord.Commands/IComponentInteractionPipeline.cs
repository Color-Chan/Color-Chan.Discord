using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands;

/// <summary>
///     Pipeline behavior to surround the the component interaction handler.
///     Implementations add additional behavior and await the next delegate.
/// </summary>
/// <example>
///     The following example showcases a simple implementation for a pipeline that measures
///     the performance of the component interactions.
///     <code language="cs">
///     public class PerformancePipeline : IComponentInteractionPipeline
///     {
///         public async Task&lt;Result&lt;IDiscordInteractionResponse&gt;&gt; HandleAsync(IInteractionContext context, ComponentInteractionHandlerDelegate next)
///         {
///             var sw = new Stopwatch();
/// 
///             sw.Start();
///             var result = await next().ConfigureAwait(false);
///             sw.Stop();
/// 
///             Console.WriteLine($"Component interaction {context.MethodName} ran for {sw.ElapsedMilliseconds.ToString()}ms.");
///             return result;
///         }
///     }
///     </code>
/// </example>
public interface IComponentInteractionPipeline
{
    /// <summary>
    ///     A pipeline handler to surround the inner component interaction handler.
    /// </summary>
    /// <param name="context">The current <see cref="IInteractionContext" /> of the component interaction request.</param>
    /// <param name="next">
    ///     The delegate for the next action in the pipeline. This will execute the component interaction if all pipelines
    ///     have been executed.
    /// </param>
    /// <returns>
    ///     <see cref="Result" /> of <see cref="IDiscordInteractionResponse" /> containing the component interaction result.
    /// </returns>
    Task<Result<IDiscordInteractionResponse>> HandleAsync(IInteractionContext context, ComponentInteractionHandlerDelegate next);
}

/// <summary>
///     Represents an async continuation for the next task to execute in the pipeline.
/// </summary>
public delegate Task<Result<IDiscordInteractionResponse>> ComponentInteractionHandlerDelegate();