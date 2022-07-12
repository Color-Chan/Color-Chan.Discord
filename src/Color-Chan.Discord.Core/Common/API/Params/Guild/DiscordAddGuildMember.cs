using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Params.Guild;

/// <summary>
///     Represents a discord Add Guild member API request model.
///     Docs: https://discord.com/developers/docs/resources/guild#membership-screening-object-json-params
/// </summary>
public class DiscordAddGuildMember
{
    /// <summary>
    ///     An oauth2 access token granted with the guilds.join to the bot's application for the user you want to add to the
    ///     guild.
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;

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
}