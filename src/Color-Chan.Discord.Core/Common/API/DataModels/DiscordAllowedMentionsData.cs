using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <summary>
///     Represents a discord Allowed Mentions Structure API model.
///     https://discord.com/developers/docs/resources/channel#allowed-mentions-object-allowed-mentions-structure
/// </summary>
public record DiscordAllowedMentionsData
{
    /// <summary>
    ///     an array of allowed mention types to parse from the content.
    /// </summary>
    [JsonPropertyName("parse")]
    public IEnumerable<DiscordAllowedMentionsType> Allowed { get; init; } = new List<DiscordAllowedMentionsType>();

    /// <summary>
    ///     Array of role_ids to mention (Max size of 100).
    /// </summary>
    [JsonPropertyName("roles")]
    public IEnumerable<ulong> AllowedRoles { get; init; } = new List<ulong>();

    /// <summary>
    ///     Array of role_ids to mention (Max size of 100).
    /// </summary>
    [JsonPropertyName("users")]
    public IEnumerable<ulong> AllowedUsers { get; init; } = new List<ulong>();

    /// <summary>
    ///     For replies, whether to mention the author of the message being replied to (default false).
    /// </summary>
    [JsonPropertyName("replied_user")]
    public bool ShouldReplyMentionsAuthor { get; init; }
}