using System.Drawing;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <inheritdoc cref="IDiscordGuildRole" />
public record DiscordGuildRoleData
{
    /// <inheritdoc cref="IDiscordGuildRole.Id" />
    [JsonPropertyName("id")]
    public ulong Id { get; init; }

    /// <inheritdoc cref="IDiscordGuildRole.Name" />
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    /// <inheritdoc cref="IDiscordGuildRole.Color" />
    [JsonPropertyName("color")]
    public Color Color { get; init; }

    /// <inheritdoc cref="IDiscordGuildRole.IsHoisted" />
    [JsonPropertyName("hoist")]
    public bool IsHoisted { get; init; }

    /// <inheritdoc cref="IDiscordGuildRole.Position" />
    [JsonPropertyName("position")]
    public int Position { get; init; }

    /// <inheritdoc cref="IDiscordGuildRole.Permissions" />
    [JsonPropertyName("permissions")]
    public DiscordPermission Permissions { get; init; }

    /// <inheritdoc cref="IDiscordGuildRole.Managed" />
    [JsonPropertyName("managed")]
    public bool Managed { get; init; }

    /// <inheritdoc cref="IDiscordGuildRole.Mentionable" />
    [JsonPropertyName("mentionable")]
    public bool Mentionable { get; init; }
}