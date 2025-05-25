using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <inheritdoc cref="IMediaGalleryItem"/>
public class MediaGalleryItemData
{
    /// <inheritdoc cref="IMediaGalleryItem.Media" />
    [JsonPropertyName("media")]
    public DiscordUnfurledMediaItemData Media { get; set; } = null!;

    /// <inheritdoc cref="IMediaGalleryItem.Description" />
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <inheritdoc cref="IMediaGalleryItem.Spoiler" />
    [JsonPropertyName("spoiler")]
    public bool? Spoiler { get; set; }
}