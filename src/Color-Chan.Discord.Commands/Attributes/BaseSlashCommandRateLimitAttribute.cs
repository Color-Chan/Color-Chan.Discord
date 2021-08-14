using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Services;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Results;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     A base for all the rate limit attributes.
    /// </summary>
    public abstract class BaseSlashCommandRateLimitAttribute : SlashCommandRequirementAttribute
    {
        private readonly int _max;
        private readonly int _resetAfterSeconds;
        private readonly string _uniqueRateLimitTime;

        /// <summary>
        ///     Initializes a new instance of <see cref="BaseSlashCommandRateLimitAttribute" />.
        /// </summary>
        /// <param name="max">The max amount of time the command could be used during the time period.</param>
        /// <param name="resetAfterSeconds">The timeframe in which the command can be used a certain amount of times.</param>
        protected BaseSlashCommandRateLimitAttribute(int max, int resetAfterSeconds)
        {
            _max = max;
            _resetAfterSeconds = resetAfterSeconds;
            _uniqueRateLimitTime = $"{max.ToString()}{resetAfterSeconds.ToString()}";
        }

        /// <summary>
        ///     Check whether or not a rate limit has been hit.
        /// </summary>
        /// <param name="context">The <see cref="ISlashCommandContext" /> of the current request.</param>
        /// <param name="services">The service.</param>
        /// <param name="uniqueId">A unique ID that will be used to store the rate limit objects.</param>
        /// <returns>
        ///     A <see cref="Result" /> containing if the rate limit was successfully passed or not.
        /// </returns>
        /// <exception cref="NullReferenceException">When the <see cref="RateLimitInfo" /> is not found after creating it.</exception>
        protected async Task<Result> CheckRateLimitAsync(ISlashCommandContext context, IServiceProvider services, ulong uniqueId)
        {
            var cacheService = services.GetRequiredService<ICacheService>();

            var key = $"{_uniqueRateLimitTime}{context.Data.Id.ToString()}{context.MethodName}{uniqueId.ToString()}";
            var rateLimitResult = await cacheService.GetValueAsync<RateLimitInfo>(key).ConfigureAwait(false);

            if (!rateLimitResult.IsSuccessful)
            {
                var absoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(_resetAfterSeconds);

                await cacheService.CacheValueAsync(key, new RateLimitInfo
                {
                    Expiration = absoluteExpiration,
                    Remaining = _max - 1
                }, null, absoluteExpiration);
                return Result.FromSuccess();
            }

            var rateLimit = rateLimitResult.Entity;
            if (rateLimit is null)
            {
                throw new NullReferenceException();
            }

            if (rateLimit.Remaining > 0)
            {
                rateLimit.Remaining--;

                await cacheService.CacheValueAsync(key, rateLimit, null, rateLimit.Expiration);
                return Result.FromSuccess();
            }

            return Result.FromError(new BaseSlashCommandRateLimitErrorResult("Rate limit hit!", _max, rateLimit.Expiration));
        }
    }
}