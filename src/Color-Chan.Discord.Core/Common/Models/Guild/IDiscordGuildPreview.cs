using System.Collections.Generic;

namespace Color_Chan.Discord.Core.Common.Models.Guild
{
    /// <summary>
    ///     Represents a discord Guild Preview Structure API model.
    ///     Docs: https://discord.com/developers/docs/resources/guild#guild-preview-object-guild-preview-structure
    /// </summary>
    public interface IDiscordGuildPreview
    {
        /// <summary>
        ///     Guild id.
        /// </summary>
        ulong Id { get; init; }

        /// <summary>
        ///     Guild name (2-100 characters, excluding trailing and leading whitespace).
        /// </summary>
        string Name { get; init; }

        /// <summary>
        ///     Icon hash.
        /// </summary>
        string? Icon { get; init; }

        /// <summary>
        ///     Splash hash.
        /// </summary>
        string? Splash { get; init; }

        /// <summary>
        ///     Discovery splash hash; only present for guilds with the "DISCOVERABLE" feature.
        /// </summary>
        string? DiscoverySplash { get; init; }

        /// <summary>
        ///     Roles in the guild.
        /// </summary>
        IEnumerable<IDiscordGuildRole> Roles { get; set; }

        /// <summary>
        ///     Custom guild emojis.
        /// </summary>
        IEnumerable<IDiscordEmoji> Emojis { get; set; }

        /// <summary>
        ///     Approximate number of members in this guild, returned from the GET /guilds/{id} endpoint when with_counts is true.
        /// </summary>
        int? ApproximateMemberCount { get; set; }

        /// <summary>
        ///     Approximate number of non-offline members in this guild, returned from the GET /guilds/{id} endpoint when
        ///     with_counts is true.
        /// </summary>
        int? ApproximatePresenceCount { get; set; }

        /// <summary>
        ///     The description for the guild, if the guild is discoverable.
        /// </summary>
        string? Description { get; init; }
    }
}