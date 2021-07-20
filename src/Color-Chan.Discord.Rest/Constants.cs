using System;

namespace Color_Chan.Discord.Rest
{
    public class Constants
    {
        /// <summary>
        ///     The Version that will be used by <see cref="DiscordApiUrl" />.
        /// </summary>
        public static int DiscordBaseApiVersion => 9;

        /// <summary>
        ///     The URL to the discord api with the version from <see cref="DiscordBaseApiVersion" />.
        /// </summary>
        public static Uri DiscordApiUrl { get; } = new($"https://discord.com/api/v{DiscordBaseApiVersion.ToString()}/");
    }
}