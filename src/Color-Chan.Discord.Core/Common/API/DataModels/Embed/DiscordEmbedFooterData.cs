using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed;

/// <inheritdoc cref="IDiscordEmbedFooter" />
public record DiscordEmbedFooterData
{
    /// <inheritdoc cref="IDiscordEmbedFooter.Text" />
    [JsonPropertyName("text")]
    public string Text { get; init; } = null!;

    /// <inheritdoc cref="IDiscordEmbedFooter.IconUrl" />
    [JsonPropertyName("icon_url")]
    public string? IconUrl { get; init; }

    /// <inheritdoc cref="IDiscordEmbedFooter.ProxyIconUrl" />
    [JsonPropertyName("proxy_icon_url")]
    public string? ProxyIconUrl { get; init; }
}