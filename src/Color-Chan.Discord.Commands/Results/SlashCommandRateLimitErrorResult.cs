using System;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Commands.Results
{
    /// <summary>
    ///     An error result describing a slash command rate limit.
    /// </summary>
    public record SlashCommandRateLimitErrorResult : SlashCommandRequirementErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRateLimitErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        /// <param name="contextUser">The user that hit the rate limit.</param>
        /// <param name="max">The max amount of time the command could be used during the <paramref name="absoluteExpiration" />.</param>
        /// <param name="absoluteExpiration">The absolute expiration date for the cache entry.</param>
        public SlashCommandRateLimitErrorResult(string errorMessage, IDiscordUser contextUser, int max, DateTimeOffset absoluteExpiration) : base(errorMessage)
        {
            ContextUser = contextUser;
            Max = max;
            AbsoluteExpiration = absoluteExpiration;
        }

        /// <summary>
        ///     The user that hit the rate limit..
        /// </summary>
        public IDiscordUser ContextUser { get; }

        /// <summary>
        ///     The max amount of time the command could be used during the <see cref="AbsoluteExpiration" />.
        /// </summary>
        public int Max { get; }

        /// <summary>
        ///     The timeframe in which the command can be used a certain amount of times
        /// </summary>
        public DateTimeOffset AbsoluteExpiration { get; }
    }
}