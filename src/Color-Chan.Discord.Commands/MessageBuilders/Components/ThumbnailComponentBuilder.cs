using System;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Rest.Models;

namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     The <see cref="IComponentBuilder"/> for <see cref="DiscordComponentType.Thumbnail"/>s.
/// </summary>
public class ThumbnailComponentBuilder : BaseComponentBuilder<ThumbnailComponentBuilder>, IComponentBuilder
{
    private const int MaxDescriptionLength = 1024;
    private string? _url;
    private string? _description;
    private bool? _spoiler;

    /// <summary>
    ///     Sets the description for the thumbnail component.
    /// </summary>
    /// <param name="description">The description to be set for the thumbnail.</param>
    /// <returns>
    ///     The updated <see cref="ThumbnailComponentBuilder" />.
    /// </returns>
    public ThumbnailComponentBuilder WithDescription(string description)
    {
        if (description.Length > MaxDescriptionLength)
        {
            throw new ArgumentException($"Description cannot exceed {MaxDescriptionLength} characters.", nameof(description));
        }

        _description = description;
        return this;
    }

    /// <summary>
    ///   Sets the URL for the thumbnail component.
    /// </summary>
    /// <param name="url">The URL to be set for the thumbnail.</param>
    /// <returns>
    ///     The updated <see cref="ThumbnailComponentBuilder" />.
    /// </returns>
    public ThumbnailComponentBuilder WithUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
        }

        _url = url;
        return this;
    }

    /// <summary>
    ///     Sets whether the thumbnail should be marked as a spoiler.
    /// </summary>
    /// <param name="spoiler">True if the thumbnail is a spoiler, false otherwise.</param>
    /// <returns>
    ///     The updated <see cref="ThumbnailComponentBuilder" />.
    /// </returns>
    public ThumbnailComponentBuilder WithSpoiler(bool spoiler)
    {
        _spoiler = spoiler;
        return this;
    }

    /// <inheritdoc />
    public IDiscordComponent Build()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(_url);

        return new DiscordComponent
        {
            Id = Id,
            Type = DiscordComponentType.Thumbnail,
            Media = new DiscordUnfurledMediaItem
            {
                Url = _url
            },
            Description = _description,
            Spoiler = _spoiler
        };
    }
}