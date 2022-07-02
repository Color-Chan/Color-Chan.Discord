using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models;

public interface IDiscordAllowedMentions
{
    /// <summary>
    ///     an array of allowed mention types to parse from the content.
    /// </summary>
    public IEnumerable<DiscordAllowedMentionsType> Allowed { get; init; }

    /// <summary>
    ///     Array of role_ids to mention (Max size of 100).
    /// </summary>
    public IEnumerable<ulong> AllowedRoles { get; init; }

    /// <summary>
    ///     Array of role_ids to mention (Max size of 100).
    /// </summary>
    public IEnumerable<ulong> AllowedUsers { get; init; }

    /// <summary>
    ///     For replies, whether to mention the author of the message being replied to (default false).
    /// </summary>
    public bool ShouldReplyMentionsAuthor { get; init; }

    /// <summary>
    ///     Converts the model back to a discord data model so that it can be send to discord.
    /// </summary>
    /// <returns>
    ///     The converted <see cref="DiscordAllowedMentionsData" />.
    /// </returns>
    DiscordAllowedMentionsData ToDataModel();
}