using System;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models;

/// <inheritdoc />
public class MediaGalleryItem : IMediaGalleryItem
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MediaGalleryItem"/> class.
    /// </summary>
    public MediaGalleryItem()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="MediaGalleryItem"/> class with the specified data model.
    /// </summary>
    /// <param name="data">The data model to initialize the item with.</param>
    /// <exception cref="ArgumentNullException">Thrown when the media in the data model is null.</exception>
    public MediaGalleryItem(MediaGalleryItemData data)
    {
        Media = data.Media is not null ? new DiscordUnfurledMediaItem { Url = data.Media.Url } : throw new ArgumentNullException(nameof(data.Media), "Media cannot be null.");
        Description = data.Description;
        Spoiler = data.Spoiler;
    }

    /// <inheritdoc />
    public IDiscordUnfurledMediaItem Media { get; init; } = null!;

    /// <inheritdoc />
    public string? Description { get; init; }

    /// <inheritdoc />
    public bool? Spoiler { get; init; }

    /// <inheritdoc />
    public MediaGalleryItemData ToDataModel()
    {
        return new MediaGalleryItemData
        {
            Media = Media.ToDataModel(),
            Description = Description,
            Spoiler = Spoiler
        };
    }
}