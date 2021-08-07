using System;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Results
{
    /// <summary>
    ///     An error result describing a slash command rate limit.
    /// </summary>
    public record SlashRateLimitErrorResult : ErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashRateLimitErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        /// <param name="contextUser">The user that hit the rate limit.</param>
        /// <param name="max">The max amount of time the command could be used during the <paramref name="absoluteTimeSpan" />.</param>
        /// <param name="absoluteTimeSpan">The timeframe in which the command can be used a certain amount of times.</param>
        public SlashRateLimitErrorResult(string errorMessage, IDiscordUser contextUser, int max, TimeSpan absoluteTimeSpan) : base(errorMessage)
        {
            ContextUser = contextUser;
            Max = max;
            AbsoluteTimeSpan = absoluteTimeSpan;
        }

        /// <summary>
        ///     The user that hit the rate limit..
        /// </summary>
        public IDiscordUser ContextUser { get; }

        /// <summary>
        ///     The max amount of time the command could be used during the <see cref="AbsoluteTimeSpan" />.
        /// </summary>
        public int Max { get; }

        /// <summary>
        ///     The timeframe in which the command can be used a certain amount of times
        /// </summary>
        public TimeSpan AbsoluteTimeSpan { get; }
    }
}