namespace Color_Chan.Discord.Host.Tokens
{
    public record PublicDiscordToken : IPublicDiscordToken
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="PublicDiscordToken" />.
        /// </summary>
        /// <param name="token">
        ///     The public token of your application.
        ///     This can be found at https://discord.com/developers/applications/{APPLICATION_ID}/information
        /// </param>
        public PublicDiscordToken(string token)
        {
            Token = token;
        }

        /// <inheritdoc />
        public string Token { get; init; }
    }
}