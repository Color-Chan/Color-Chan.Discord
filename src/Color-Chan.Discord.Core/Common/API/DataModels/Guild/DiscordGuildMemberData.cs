using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    /// <summary>
    ///     Represents a discord Guild Member Structure API model.
    ///     Docs: https://discord.com/developers/docs/resources/guild#guild-member-object-guild-member-structure
    /// </summary>
    public record DiscordGuildMemberData
    {
        /// <summary>
        ///     The user this guild member represents.
        /// </summary>
        [JsonPropertyName("user")]
        public DiscordUserData? User { get; init; } = null!;

        /// <summary>
        ///     This users guild nickname.
        /// </summary>
        [JsonPropertyName("nick")]
        public string? NickName { get; init; }

        /// <summary>
        ///     A list of role ids that are assigned the this guild member.
        /// </summary>
        [JsonPropertyName("roles")]
        public IEnumerable<ulong> Roles { get; init; } = new List<ulong>();

        /// <summary>
        ///     When the user joined the guild.
        /// </summary>
        [JsonPropertyName("joined_at")]
        public DateTimeOffset JoinedAt { get; init; }

        /// <summary>
        ///     When the user started boosting the guild.
        /// </summary>
        [JsonPropertyName("premium_since")]
        public DateTimeOffset? PremiumSince { get; init; }

        /// <summary>
        ///     Whether the user is deafened in voice channels.
        /// </summary>
        [JsonPropertyName("deaf")]
        public bool Deaf { get; init; }

        /// <summary>
        ///     Whether the user is muted in voice channels.
        /// </summary>
        [JsonPropertyName("mute")]
        public bool Mute { get; init; }

        /// <summary>
        ///     Whether the user has not yet passed the guild's Membership Screening requirements.
        /// </summary>
        [JsonPropertyName("pending")]
        public bool? Pending { get; init; }

        /// <summary>
        ///     Total permissions of the member in the channel, including overwrites, returned when in the interaction object.
        /// </summary>
        [JsonPropertyName("permissions")]
        public DiscordPermission? Permissions { get; init; }
    }
}