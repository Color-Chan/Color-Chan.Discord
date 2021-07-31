namespace Color_Chan.Discord.Core
{
    /// <summary>
    ///     Contains the discord token and IDs.
    /// </summary>
    public record DiscordTokens
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordTokens" />.
        /// </summary>
        /// <param name="botToken">
        ///     The bot token of your application.
        ///     This can be found at https://discord.com/developers/applications/{APPLICATION_ID}/bot
        /// </param>
        /// <param name="publicToken">
        ///     The public token of your application.
        ///     This can be found at https://discord.com/developers/applications/{APPLICATION_ID}/information
        /// </param>
        /// <param name="applicationId">
        ///     The ID of your application.
        ///     This can be found at https://discord.com/developers/applications/{APPLICATION_ID}/information
        /// </param>
        public DiscordTokens(string botToken, string publicToken, ulong applicationId)
        {
            BotToken = botToken;
            PublicToken = publicToken;
            ApplicationId = applicationId;
        }

        /// <summary>
        ///     The bot token of your application.
        ///     This can be found at https://discord.com/developers/applications/{APPLICATION_ID}/bot
        /// </summary>
        public string BotToken { get; }

        /// <summary>
        ///     The public token of your application.
        ///     This can be found at https://discord.com/developers/applications/{APPLICATION_ID}/information
        /// </summary>
        public string PublicToken { get; init; }

        /// <summary>
        ///     The ID of your application.
        ///     This can be found at https://discord.com/developers/applications/{APPLICATION_ID}/information
        /// </summary>
        public ulong ApplicationId { get; }
    }
}