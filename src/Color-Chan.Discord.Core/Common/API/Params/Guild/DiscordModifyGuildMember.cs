using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Params.Guild;

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
}