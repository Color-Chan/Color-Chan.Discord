using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Params.User;

/// <summary>
///     Represents a discord Create DM group parameter model.
///     Docs: https://discord.com/developers/docs/resources/user#create-group-dm
/// </summary>
public class DiscordCreateDmGroup
{
    /// <summary>
    ///     access tokens of users that have granted your app the gdm.join scope.
    /// </summary>
    [JsonPropertyName("access_tokens")]
    public IEnumerable<string>? AccessTokens { get; set; }

    /// <summary>
    ///     a dictionary of user ids to their respective nicknames.
    /// </summary>
    [JsonPropertyName("nicks")]
    public Dictionary<ulong, string>? NickNames { get; set; }
}