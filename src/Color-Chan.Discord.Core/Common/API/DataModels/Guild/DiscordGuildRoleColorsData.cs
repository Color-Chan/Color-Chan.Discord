using System.Drawing;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <inheritdoc cref="IDiscordGuildRoleColors" />
public record DiscordGuildRoleColorsData
{
    /// <inheritdoc cref="IDiscordGuildRoleColors" />
    [JsonPropertyName("primary_color")]
    public Color Primary { get; init; }

    /// <inheritdoc cref="IDiscordGuildRoleColors" />
    [JsonPropertyName("secondary_color")]
    public Color? Secondary { get; init; }

    /// <inheritdoc cref="IDiscordGuildRoleColors" />
    [JsonPropertyName("tertiary_color")]
    public Color? Tertiary { get; init; }
}