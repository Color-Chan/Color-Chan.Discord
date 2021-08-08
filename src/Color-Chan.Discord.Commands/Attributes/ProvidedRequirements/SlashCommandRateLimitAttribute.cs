using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Services;
using Color_Chan.Discord.Commands.Contexts;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Commands.Results;
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
        private readonly int _max;
        private readonly int _resetAfterSeconds;
        private readonly string _uniqueRateLimitTime;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRateLimitAttribute" />.
        /// </summary>
        /// <param name="max">The max amount of time the command could be used during the time period.</param>
        /// <param name="resetAfterSeconds">The timeframe in which the command can be used a certain amount of times.</param>
        public SlashCommandRateLimitAttribute(int max, int resetAfterSeconds)
        {
            _max = max;
            _resetAfterSeconds = resetAfterSeconds;
            _uniqueRateLimitTime = $"{max.ToString()}{resetAfterSeconds.ToString()}";
        }

        /// <inheritdoc />
        public override async Task<Result> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services)
        {
            var cacheService = services.GetRequiredService<ICacheService>();

            var key = $"{_uniqueRateLimitTime}{context.CommandRequest.Id.ToString()}{context.MethodName}{context.User.Id.ToString()}";
            var userRateLimitResult = await cacheService.GetValueAsync<RateLimitUser>(key).ConfigureAwait(false);

            if (!userRateLimitResult.IsSuccessful)
            {
                var absoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(_resetAfterSeconds);

                await cacheService.CacheValueAsync(key, new RateLimitUser
                {
                    Expiration = absoluteExpiration,
                    Remaining = _max - 1
                }, null, absoluteExpiration);
                return Result.FromSuccess();
            }

            var rateLimitUser = userRateLimitResult.Entity;
            if (rateLimitUser is null)
            {
                throw new NullReferenceException();
            }
            
            if (rateLimitUser.Remaining > 0)
            {
                rateLimitUser.Remaining--;
                
                await cacheService.CacheValueAsync(key, rateLimitUser, null, rateLimitUser.Expiration);
                return Result.FromSuccess();
            }

            return Result.FromError(new SlashCommandRateLimitErrorResult("Rate limit hit!", context.User, _max, rateLimitUser.Expiration));
        }
    }
}