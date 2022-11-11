using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed;

/// <inheritdoc cref="IDiscordEmbedVideo" />
public record DiscordEmbedVideoData
{
    /// <inheritdoc cref="IDiscordEmbedVideo.Url" />
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <inheritdoc cref="IDiscordEmbedVideo.ProxyUrl" />
    [JsonPropertyName("proxy_url")]
    public string? ProxyUrl { get; init; }

    /// <inheritdoc cref="IDiscordEmbedVideo.Height" />
    [JsonPropertyName("height")]
    public int? Height { get; init; }

    /// <inheritdoc cref="IDiscordEmbedVideo.Width" />
    [JsonPropertyName("width")]
    public int? Width { get; init; }
}