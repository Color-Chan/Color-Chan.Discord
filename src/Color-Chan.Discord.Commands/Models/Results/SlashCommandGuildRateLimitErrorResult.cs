namespace Color_Chan.Discord.Commands.Models.Results
{
    /// <summary>
    ///     An error result describing a slash command rate limit.
    /// </summary>
    public record SlashCommandGuildRateLimitErrorResult : BaseSlashCommandRateLimitErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandGuildRateLimitErrorResult" />.
        /// </summary>
        /// <param name="baseError">The <see cref="BaseSlashCommandRateLimitErrorResult"/>.</param>
        /// <param name="guildId">The server that hit the rate limit.</param>
        public SlashCommandGuildRateLimitErrorResult(BaseSlashCommandRateLimitErrorResult baseError, ulong guildId) : base(baseError.ErrorMessage, baseError.Max, baseError.AbsoluteExpiration)
        {
            GuildId = guildId;
        }

        /// <summary>
        ///     The Server that hit the rate limit.
        /// </summary>
        public ulong GuildId { get; }
    }
}