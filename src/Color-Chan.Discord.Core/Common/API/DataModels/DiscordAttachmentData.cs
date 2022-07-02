using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

public record DiscordAttachmentData
{
    /// <summary>
    ///     Attachment id.
    /// </summary>
    [JsonPropertyName("id")]
    public ulong Id { get; init; }

    /// <summary>
    ///     name of file attached.
    /// </summary>
    [JsonPropertyName("filename")]
    public string FileName { get; init; } = null!;

    /// <summary>
    ///     Size of file in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; init; }

    /// <summary>
    ///     Source url of file.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = null!;

    /// <summary>
    ///     A proxied url of file.
    /// </summary>
    [JsonPropertyName("proxy_url")]
    public string ProxyUrl { get; init; } = null!;

    /// <summary>
    ///     Height of file (if image).
    /// </summary>
    [JsonPropertyName("height")]
    public int? Height { get; init; }

    /// <summary>
    ///     Width of file (if image).
    /// </summary>
    [JsonPropertyName("width")]
    public int? Width { get; init; }
}