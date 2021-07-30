using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Color-Chan.Discord.Rest.Tests")]

namespace Color_Chan.Discord.Rest
{
    internal static class Constants
    {
        /// <summary>
        ///     The Version that will be used by <see cref="DiscordApiUrl" />.
        /// </summary>
        internal static int DiscordBaseApiVersion => 9;

        /// <summary>
        ///     The URL to the discord api with the version from <see cref="DiscordBaseApiVersion" />.
        /// </summary>
        internal static Uri DiscordApiUrl { get; } = new($"https://discord.com/api/v{DiscordBaseApiVersion.ToString()}/");

        internal static class Headers
        {
            /// <summary>
            ///     The query name to limit a request to a certain number of results.
            /// </summary>
            internal static string LimitQueryName { get; } = "limit";

            /// <summary>
            ///     The query name to get result after a certain value. Can be used for paging.
            /// </summary>
            internal static string AfterQueryName { get; } = "after";

            /// <summary>
            ///     The query name to get result before a certain value. Can be used for paging.
            /// </summary>
            internal static string BeforeQueryName { get; } = "before";

            /// <summary>
            ///     The query name to search by a value.
            /// </summary>
            internal static string SearchQueryName { get; } = "query";

            /// <summary>
            ///     The query name to get a result back with counts.
            /// </summary>
            internal static string WithCountsQueryName { get; } = "with_counts";

            /// <summary>
            ///     The query name to get a result around a certain value.
            /// </summary>
            internal static string AroundQueryName { get; } = "around";
        }
    }
}