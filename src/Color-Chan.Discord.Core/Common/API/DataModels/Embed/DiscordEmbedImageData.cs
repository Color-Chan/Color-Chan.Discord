using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed;

/// <summary>
///     Represents a discord Embed Image Structure API model.
///     https://discord.com/developers/docs/resources/channel#embed-object-embed-image-structure
/// </summary>
public record DiscordEmbedImageData
{
    /// <summary>
    ///     Source url of image (only supports http(s) and attachments).
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    ///     A proxied url of the image.
    /// </summary>
    [JsonPropertyName("proxy_url")]
    public string? ProxyUrl { get; init; }

    /// <summary>
    ///     Height of image.
    /// </summary>
    [JsonPropertyName("height")]
    public int? Height { get; init; }

    /// <summary>
    ///     Width of image.
    /// </summary>
    [JsonPropertyName("width")]
    public int? Width { get; init; }
}