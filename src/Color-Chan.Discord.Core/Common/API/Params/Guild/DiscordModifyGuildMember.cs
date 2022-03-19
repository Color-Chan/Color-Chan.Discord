using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Params.Guild
{
    /// <summary>
    ///     Represents a discord Modify Guild Member API request model.
    ///     Docs: https://discord.com/developers/docs/resources/guild#modify-guild-member
    /// </summary>
    public class DiscordModifyGuildMember
    {
        /// <summary>
        ///     Value to set users nickname to.
        /// </summary>
        /// <remarks>
        ///     Requires MANAGE_NICKNAMES permission
        /// </remarks>
        [JsonPropertyName("nick")]
        public string Nick { get; set; } = null!;

        /// <summary>
        ///     List of role ids the member is assigned
        /// </summary>
        /// <remarks>
        ///     Requires MANAGE_ROLES permission
        /// </remarks>
        [JsonPropertyName("roles")]
        public IEnumerable<ulong> RoleIds { get; set; } = null!;

        /// <summary>
        ///     Whether the user is muted in voice channels.
        /// </summary>
        /// <remarks>
        ///     Requires MUTE_MEMBERS permission
        /// </remarks>
        [JsonPropertyName("mute")]
        public bool IsMuted { get; set; }

        /// <summary>
        ///     Whether the user is deafened in voice channels.
        /// </summary>
        /// <remarks>
        ///     Requires DEAFEN_MEMBERS permission
        /// </remarks>
        [JsonPropertyName("deaf")]
        public bool Deaf { get; set; }

        /// <summary>
        ///     Id of channel to move user to (if they are connected to voice).
        ///     If the channel_id is set to null, this will force the target user to be disconnected from voice.
        /// </summary>
        /// <remarks>
        ///     Requires MOVE_MEMBERS permission
        /// </remarks>
        [JsonPropertyName("channel_id")]
        public ulong? ChannelId { get; set; }
        
        /// <summary>
        ///     When the user's timeout will expire and the user will be able to communicate in the guild again (up to 28 days in the future),
        ///     set to null to remove timeout.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Will throw a 403 error if the user has the ADMINISTRATOR permission or is the owner of the guild.
        ///     </para>
        ///     <para>
        ///         Requires the MODERATE_MEMBERS permission.
        ///     </para>
        /// </remarks>
        [JsonPropertyName("communication_disabled_until")]
        public DateTimeOffset? CommunicationDisabledUntil { get; set; }
    }
}