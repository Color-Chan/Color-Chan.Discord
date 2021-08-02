using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models
{
    public interface IDiscordEmoji
    {
        /// <summary>
        ///     Emoji id.
        /// </summary>
        ulong Id { get; init; }

        /// <summary>
        ///     Emoji name
        /// </summary>
        string Name { get; init; }

        /// <summary>
        ///     Roles allowed to use this emoji
        /// </summary>
        IEnumerable<ulong>? RoleIds { get; init; }

        /// <summary>
        ///     User that created this emoji
        /// </summary>
        IDiscordUser? User { get; init; }

        /// <summary>
        ///     Whether this emoji must be wrapped in colons.
        /// </summary>
        bool? RequireColons { get; init; }

        /// <summary>
        ///     Whether this emoji is managed.
        /// </summary>
        bool? IsManaged { get; init; }

        /// <summary>
        ///     Whether this emoji is animated.
        /// </summary>
        bool? IsAnimated { get; init; }

        /// <summary>
        ///     Whether this emoji can be used, may be false due to loss of Server Boosts.
        /// </summary>
        bool? IsAvailable { get; init; }

        /// <summary>
        ///     Converts the model back to a discord data model so that it can be send to discord.
        /// </summary>
        /// <returns>
        ///     The converted <see cref="DiscordEmojiData" />.
        /// </returns>
        DiscordEmojiData ToDataModel();
    }
}