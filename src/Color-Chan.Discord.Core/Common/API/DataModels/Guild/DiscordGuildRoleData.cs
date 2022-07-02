using System.Drawing;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <summary>
///     Represents a discord Role Structure API model.
///     https://discord.com/developers/docs/topics/permissions#role-object-role-structure
/// </summary>
public record DiscordGuildRoleData
{
    /// <summary>
    ///     Role id.
    /// </summary>
    [JsonPropertyName("id")]
    public ulong Id { get; init; }

    /// <summary>
    ///     Role name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    /// <summary>
    ///     Integer representation of hexadecimal color code.
    /// </summary>
    [JsonPropertyName("color")]
    public Color Color { get; init; }

    /// <summary>
    ///     If this role is pinned in the user listing.
    /// </summary>
    [JsonPropertyName("hoist")]
    public bool IsHoisted { get; init; }

    /// <summary>
    ///     Position of this role.
    /// </summary>
    [JsonPropertyName("position")]
    public int Position { get; init; }

    /// <summary>
    ///     Permission bit set.
    /// </summary>
    [JsonPropertyName("permissions")]
    public DiscordPermission Permissions { get; init; }

    /// <summary>
    ///     Whether this role is managed by an integration
    /// </summary>
    [JsonPropertyName("managed")]
    public bool Managed { get; init; }

    /// <summary>
    ///     Whether this role is mentionable
    /// </summary>
    [JsonPropertyName("mentionable")]
    public bool Mentionable { get; init; }
}