using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Commands.Models.Results
{
    /// <summary>
    ///     An error result describing a slash command rate limit.
    /// </summary>
    public record SlashCommandUserRateLimitErrorResult : BaseSlashCommandRateLimitErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandUserRateLimitErrorResult" />.
        /// </summary>
        /// <param name="baseError">The <see cref="BaseSlashCommandRateLimitErrorResult" />.</param>
        /// <param name="user">The user that hit the rate limit.</param>
        public SlashCommandUserRateLimitErrorResult(BaseSlashCommandRateLimitErrorResult baseError, IDiscordUser user) : base(baseError.ErrorMessage, baseError.Max, baseError.AbsoluteExpiration)
        {
            User = user;
        }

        /// <summary>
        ///     The user that hit the rate limit.
        /// </summary>
        public IDiscordUser User { get; }
    }
}