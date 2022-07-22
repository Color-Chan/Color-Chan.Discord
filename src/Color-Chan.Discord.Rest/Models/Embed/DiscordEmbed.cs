using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Embed;

/// <inheritdoc cref="IDiscordEmbed" />
public record DiscordEmbed : IDiscordEmbed
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordEmbed" />
    /// </summary>
    public DiscordEmbed()
    {
    }

    /// <summary>
    ///     Initializes a new <see cref="DiscordEmbed" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordEmbed" />.</param>
    public DiscordEmbed(DiscordEmbedData data)
    {
        Title = data.Title;
        Type = data.Type;
        Description = data.Description;
        Url = data.Url;
        Timestamp = data.Timestamp;
        Color = data.Color;
        Footer = data.Footer is not null ? new DiscordEmbedFooter(data.Footer) : null;
        Image = data.Image is not null ? new DiscordEmbedImage(data.Image) : null;
        Thumbnail = data.Thumbnail is not null ? new DiscordEmbedThumbnail(data.Thumbnail) : null;
        Video = data.Video is not null ? new DiscordEmbedVideo(data.Video) : null;
        Provider = data.Provider is not null ? new DiscordEmbedProvider(data.Provider) : null;
        Author = data.Author is not null ? new DiscordEmbedAuthor(data.Author) : null;
        Fields = data.Fields?.Select(fieldData => new DiscordEmbedField(fieldData));
    }

    /// <inheritdoc />
    public string? Title { get; init; }

    /// <inheritdoc />
    public DiscordEmbedType? Type { get; init; }

    /// <inheritdoc />
    public string? Description { get; init; }

    /// <inheritdoc />
    public string? Url { get; init; }

    /// <inheritdoc />
    public DateTimeOffset? Timestamp { get; init; }

    /// <inheritdoc />
    public Color? Color { get; init; }

    /// <inheritdoc />
    public IDiscordEmbedFooter? Footer { get; init; }

    /// <inheritdoc />
    public IDiscordEmbedImage? Image { get; init; }

    /// <inheritdoc />
    public IDiscordEmbedThumbnail? Thumbnail { get; init; }

    /// <inheritdoc />
    public IDiscordEmbedVideo? Video { get; init; }

    /// <inheritdoc />
    public IDiscordEmbedProvider? Provider { get; init; }

    /// <inheritdoc />
    public IDiscordEmbedAuthor? Author { get; init; }

    /// <inheritdoc />
    public IEnumerable<IDiscordEmbedField>? Fields { get; init; }

    /// <inheritdoc />
    public DiscordEmbedData ToDataModel()
    {
        return new DiscordEmbedData
        {
            Footer = Footer?.ToDataModel(),
            Image = Image?.ToDataModel(),
            Video = Video?.ToDataModel(),
            Provider = Provider?.ToDataModel(),
            Author = Author?.ToDataModel(),
            Fields = Fields?.Select(field => field.ToDataModel()),
            Color = Color,
            Description = Description,
            Thumbnail = Thumbnail?.ToDataModel(),
            Timestamp = Timestamp,
            Title = Title,
            Type = Type,
            Url = Url
        };
    }
}