using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models;

/// <summary>
///     Represents a discord Media Gallery Item Data API model.
///     Docs: https://discord.com/developers/docs/components/reference#media-gallery-media-gallery-item-structure
/// </summary>
public interface IMediaGalleryItem
{
    /// <summary>
    ///     A url or attachment.
    /// </summary>
    IDiscordUnfurledMediaItem Media { get; init; }
    
    /// <summary>
    ///     Alt text for the media.
    /// </summary>
    string? Description { get; init; }
    
    /// <summary>
    ///     Whether the media should be a spoiler (or blurred out). Defaults to false.
    /// </summary>
    bool? Spoiler { get; init; }
    
    /// <summary>
    ///     Converts the model back to a discord data model so that it can be send to discord.
    /// </summary>
    /// <returns>
    ///     The converted <see cref="MediaGalleryItemData" />.
    /// </returns>
    MediaGalleryItemData ToDataModel();
}