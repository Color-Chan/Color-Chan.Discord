using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed;

/// <summary>
///     Represents a discord Embed Footer Structure API model.
///     https://discord.com/developers/docs/resources/channel#embed-object-embed-footer-structure
/// </summary>
public record DiscordEmbedFooterData
{
    /// <summary>
    ///     Footer text.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = null!;

    /// <summary>
    ///     Url of footer icon (only supports http(s) and attachments).
    /// </summary>
    [JsonPropertyName("icon_url")]
    public string? IconUrl { get; init; }

    /// <summary>
    ///     A proxied url of footer icon.
    /// </summary>
    [JsonPropertyName("proxy_icon_url")]
    public string? ProxyIconUrl { get; init; }
}