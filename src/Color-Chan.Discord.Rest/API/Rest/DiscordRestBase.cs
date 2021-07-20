namespace Color_Chan.Discord.Rest.API.Rest
{
    public abstract class DiscordRestBase
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRestBase" />.
        /// </summary>
        /// <param name="httpClient">The <see cref="IDiscordHttpClient" /> that will be used to send all the requests.</param>
        protected DiscordRestBase(IDiscordHttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        /// <summary>
        ///     The <see cref="IDiscordHttpClient" /> that will be used to send all the requests.
        /// </summary>
        protected IDiscordHttpClient HttpClient { get; }
    }
}