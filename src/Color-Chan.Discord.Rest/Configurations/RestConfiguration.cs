using System;

namespace Color_Chan.Discord.Rest.Configurations
{
    /// <summary>
    ///     Holds the configurations for Color-Chan.Discord.Rest
    /// </summary>
    public class RestConfiguration
    {
        /// <summary>
        ///     Whether or not Color-Chan.Discord.Rest should try to prevent rate limits.
        ///     Default: true
        ///     <remarks>
        ///         Disabling this can be useful if you want to use a rate limited proxy of the Discord API.
        ///     </remarks>
        /// </summary>
        public bool UseRateLimiting { get; set; } = true;

        /// <summary>
        ///     The base <see cref="Uri"/> that will be used with the Discord HTTP client.
        ///     Example: http://localhost:3000/api/v9/
        ///     <remarks>
        ///         Disabling this can be useful if you want to use a rate limited proxy of the Discord API.
        ///     </remarks>
        /// </summary>
        public Uri? DiscordBaseUriOverwrite { get; set; }
    }
}