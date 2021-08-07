using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Services;
using Color_Chan.Discord.Commands.Results;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     Specifies a rate limit for a command or command group.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class SlashRateLimitAttribute : SlashCommandRequirementAttribute
    {
        private readonly TimeSpan _absoluteTimeSpan;
        private readonly int _max;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashRateLimitAttribute" />.
        /// </summary>
        /// <param name="max">The max amount of time the command could be used during the time period.</param>
        /// <param name="resetAfterSeconds">The timeframe in which the command can be used a certain amount of times.</param>
        public SlashRateLimitAttribute(int max, int resetAfterSeconds)
        {
            _max = max;
            _absoluteTimeSpan = TimeSpan.FromSeconds(resetAfterSeconds);
        }

        /// <inheritdoc />
        public override async Task<Result> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services)
        {
            var cacheService = services.GetRequiredService<ICacheService>();

            var key = $"{context.CommandRequest.Id.ToString()}{context.MethodName}{context.User.Id.ToString()}";
            var userRateLimitResult = await cacheService.GetValueAsync<int>(key).ConfigureAwait(false);

            if (!userRateLimitResult.IsSuccessful)
            {
                var slidingTimeSpan = TimeSpan.FromMilliseconds(_absoluteTimeSpan.TotalMilliseconds / _max);
                await cacheService.CacheValueAsync(key, _max - 1, slidingTimeSpan, _absoluteTimeSpan);
                return Result.FromSuccess();
            }

            var remaining = userRateLimitResult.Entity;

            if (remaining > 0)
            {
                remaining--;

                await cacheService.CacheValueAsync(key, remaining);
                return Result.FromSuccess();
            }

            return Result.FromError(new SlashRateLimitErrorResult("Rate limit hit!", context.User, _max, _absoluteTimeSpan));
        }
    }
}