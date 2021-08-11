using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Extensions
{
    /// <summary>
    ///     Contains all the extensions methods for <see cref="IDiscordGuildRole" />.
    /// </summary>
    public static class DiscordGuildRoleExtensions
    {
        /// <summary>
        ///     Get a string mentioning a specific <paramref name="role"/>.
        /// </summary>
        /// <param name="role">The <see cref="IDiscordGuildRole"/> that will be mentioned.</param>
        /// <returns>
        ///     A <see cref="string"/> containing the mentioned <paramref name="role"/>.
        /// </returns>
        public static string Mention(IDiscordGuildRole role)
        {
            return $"<@&{role.Id.ToString()}>";
        }
    }
}