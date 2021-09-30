using System.Diagnostics;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Logging;

namespace RoleManager.Pipelines
{
    /// <summary>
    ///     A pipeline that will measure the performance of the slash commands.
    /// </summary>
    public class PerformancePipeline : ISlashCommandPipeline
    {
        private readonly ILogger<PerformancePipeline> _logger;

        /// <summary>
        ///     Initializes a new instance of <see cref="PerformancePipeline" />.
        /// </summary>
        /// <param name="logger">The logger that will log the performance of the slash commands to the console.</param>
        public PerformancePipeline(ILogger<PerformancePipeline> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<Result<IDiscordInteractionResponse>> HandleAsync(ISlashCommandContext context, SlashCommandHandlerDelegate next)
        {
            var sw = new Stopwatch();

            sw.Start();

            // Run the command
            var result = await next().ConfigureAwait(false);

            sw.Stop();

            _logger.Log(sw.ElapsedMilliseconds > 500 ? LogLevel.Warning : LogLevel.Information, "Command {Name} ran for {Time}ms", context.MethodName, sw.ElapsedMilliseconds.ToString());
            return result;
        }
    }
}