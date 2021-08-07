using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Services;
using Color_Chan.Discord.Commands.Results;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements
{
    /// <summary>
    ///     Specifies a rate limit for a command or command group.
    /// </summary>
    /// <remarks>
    ///     This requirement will limit the amount of time a user can request a slash command during a time period.
    /// </remarks>
    /// <example>
    ///     This example limits all the slash commands in the PongCommands slash command module to 2 requests every 10 seconds
    ///     and 4 requests every 30 seconds. You can also put the <see cref="SlashCommandRateLimitAttribute" /> on a method if
    ///     you
    ///     only want to rate limit a specific slash command.
    ///     <code language="cs">
    ///     [SlashCommandRateLimit(2, 10)]
    ///     [SlashCommandRateLimit(4, 30)]
    ///     public class PongCommands : SlashCommandModule
    ///     {
    ///         [SlashCommand("ping", "Ping Pong!")]
    ///         public Task&lt;IDiscordInteractionResponse&gt; PongAsync()
    ///         {
    ///             // Command code...
    ///         }
    ///     }
    ///     </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class SlashCommandRateLimitAttribute : SlashCommandRequirementAttribute
    {
        private readonly TimeSpan _absoluteTimeSpan;
        private readonly int _max;
        private readonly string _uniqueRateLimitTime;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRateLimitAttribute" />.
        /// </summary>
        /// <param name="max">The max amount of time the command could be used during the time period.</param>
        /// <param name="resetAfterSeconds">The timeframe in which the command can be used a certain amount of times.</param>
        public SlashCommandRateLimitAttribute(int max, int resetAfterSeconds)
        {
            _max = max;
            _absoluteTimeSpan = TimeSpan.FromSeconds(resetAfterSeconds);
            _uniqueRateLimitTime = $"{max.ToString()}{resetAfterSeconds.ToString()}";
        }

        /// <inheritdoc />
        public override async Task<Result> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services)
        {
            var cacheService = services.GetRequiredService<ICacheService>();

            var key = $"{_uniqueRateLimitTime}{context.CommandRequest.Id.ToString()}{context.MethodName}{context.User.Id.ToString()}";
            var userRateLimitResult = await cacheService.GetValueAsync<int>(key).ConfigureAwait(false);

            if (!userRateLimitResult.IsSuccessful)
            {
                await cacheService.CacheValueAsync(key, _max - 1, _absoluteTimeSpan, _absoluteTimeSpan);
                return Result.FromSuccess();
            }

            var remaining = userRateLimitResult.Entity;

            if (remaining > 0)
            {
                remaining--;

                await cacheService.CacheValueAsync(key, remaining);
                return Result.FromSuccess();
            }

            return Result.FromError(new SlashCommandRateLimitErrorResult("Rate limit hit!", context.User, _max, _absoluteTimeSpan));
        }
    }
}