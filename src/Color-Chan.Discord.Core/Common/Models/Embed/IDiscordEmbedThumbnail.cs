using Color_Chan.Discord.Core.Common.API.DataModels.Embed;

namespace Color_Chan.Discord.Core.Common.Models.Embed;

public interface IDiscordEmbedThumbnail
{
    /// <summary>
    ///     Source url of thumbnail (only supports http(s) and attachments).
    /// </summary>
    string? Url { get; init; }

    /// <summary>
    ///     A proxied url of the image.
    /// </summary>
    string? ProxyUrl { get; init; }

    /// <summary>
    ///     Height of image.
    /// </summary>
    int? Height { get; init; }

    /// <summary>
    ///     Width of image.
    /// </summary>
    int? Width { get; init; }

    /// <summary>
    ///     Converts the model back to a discord data model so that it can be send to discord.
    /// </summary>
    /// <returns>
    ///     The converted <see cref="DiscordEmbedThumbnailData" />.
    /// </returns>
    DiscordEmbedThumbnailData ToDataModel();
}