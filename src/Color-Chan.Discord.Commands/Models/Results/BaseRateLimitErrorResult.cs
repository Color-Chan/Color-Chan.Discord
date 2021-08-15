using System;

namespace Color_Chan.Discord.Commands.Models.Results
{
    /// <summary>
    ///     An error result describing a slash command rate limit.
    /// </summary>
    public record BaseRateLimitErrorResult : SlashCommandRequirementErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="BaseRateLimitErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        /// <param name="max">The max amount of time the command could be used during the <paramref name="absoluteExpiration" />.</param>
        /// <param name="absoluteExpiration">The absolute expiration date for the cache entry.</param>
        internal BaseRateLimitErrorResult(string errorMessage, int max, DateTimeOffset absoluteExpiration) : base(errorMessage)
        {
            Max = max;
            AbsoluteExpiration = absoluteExpiration;
        }

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