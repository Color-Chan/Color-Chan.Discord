using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Extensions
{
    /// <summary>
    ///     Contains all the extensions methods for <see cref="IDiscordUser" />.
    /// </summary>
    public static class DiscordUserExtensions
    {
        /// <summary>
        ///     Get a string mentioning a specific <paramref name="user" />.
        /// </summary>
        /// <param name="user">The <see cref="IDiscordUser" /> that will be mentioned.</param>
        /// <param name="useNickname">Whether or not the mention with the nickname of the user.</param>
        /// <returns>
        ///     A <see cref="string" /> containing the mentioned <paramref name="user" />.
        /// </returns>
        public static string Mention(this IDiscordUser user, bool useNickname = true)
        {
            return !useNickname ? $"<@{user.Id.ToString()}>" : $"<@!{user.Id.ToString()}>";
        }
    }
}