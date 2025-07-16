using System;
using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Rest.Models;

namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     The <see cref="IComponentBuilder"/> for <see cref="DiscordComponentType.MediaGallery"/>s.
/// </summary>
public class MediaGalleryComponentBuilder : BaseComponentBuilder<MediaGalleryComponentBuilder>, IComponentBuilder
{
    private const int MaxDescriptionLength = 1024;
    private List<IMediaGalleryItem> Items { get; } = [];

    /// <summary>
    ///     Adds a media item to the gallery with the specified URL, description, and spoiler status.
    /// </summary>
    /// <param name="url">The URL of the media item.</param>
    /// <param name="description">The description of the media item.</param>
    /// <param name="spoiler">Whether the media item is a spoiler.</param>
    /// <returns>
    ///     The updated <see cref="MediaGalleryComponentBuilder" /> with the new media item added to the gallery.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the URL is null or whitespace.</exception>
    /// <exception cref="ArgumentException">Thrown when the description exceeds the maximum length.</exception>
    public MediaGalleryComponentBuilder WithMedia(string url, string? description = null, bool? spoiler = null)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
        }

        if (description is not null && description.Length > MaxDescriptionLength)
        {
            throw new ArgumentException($"Description cannot exceed {MaxDescriptionLength} characters.", nameof(description));
        }

        var item = new MediaGalleryItem
        {
            Media = new DiscordUnfurledMediaItem { Url = url },
            Description = description,
            Spoiler = spoiler
        };

        Items.Add(item);
        return this;
    }

    /// <inheritdoc />
    public IDiscordComponent Build()
    {
        return new DiscordComponent
        {
            Id = Id,
            Type = DiscordComponentType.MediaGallery,
            Items = Items
        };
    }
}