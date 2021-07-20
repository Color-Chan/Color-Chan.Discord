namespace Color_Chan.Discord.Tokens
{
    public interface IPublicDiscordToken
    {
        /// <summary>
        ///     The public token of your application.
        ///     This can be found at https://discord.com/developers/applications/{APPLICATION_ID}/information
        /// </summary>
        string Token { get; init; }
    }
}