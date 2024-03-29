using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Params.User;

/// <summary>
///     Represents a Modify Current User parameter model.
///     Docs: https://discord.com/developers/docs/resources/user#connection-object-json-params
/// </summary>
public class DiscordModifyCurrentUser
{
    // Todo: Add support to upload avatar.

    /// <summary>
    ///     User's username, if changed may cause the user's discriminator to be randomized.
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }
}