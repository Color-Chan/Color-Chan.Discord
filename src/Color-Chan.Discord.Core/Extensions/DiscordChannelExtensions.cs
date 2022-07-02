using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Extensions;

/// <summary>
///     Contains all the extensions methods for <see cref="IDiscordChannel" />.
/// </summary>
public static class DiscordChannelExtensions
{
    /// <summary>
    ///     Get a string mentioning a specific <paramref name="channel" />.
    /// </summary>
    /// <param name="channel">The <see cref="IDiscordChannel" /> that will be mentioned.</param>
    /// <returns>
    ///     A <see cref="string" /> containing the mentioned <paramref name="channel" />.
    /// </returns>
    public static string Mention(this IDiscordChannel channel)
    {
        return $"<#{channel.Id.ToString()}>";
    }
}