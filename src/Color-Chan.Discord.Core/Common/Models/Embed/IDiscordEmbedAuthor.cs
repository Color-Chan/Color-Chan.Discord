using Color_Chan.Discord.Core.Common.API.DataModels.Embed;

namespace Color_Chan.Discord.Core.Common.Models.Embed;

/// <summary>
///     Represents a discord Embed Author  Structure API model.
///     Docs: https://discord.com/developers/docs/resources/channel#embed-object-embed-author-structure
/// </summary>
public interface IDiscordEmbedAuthor
{
    /// <summary>
    ///     Name of author.
    /// </summary>
    string? Name { get; init; }

    /// <summary>
    ///     Url of author.
    /// </summary>
    string? Url { get; init; }

    /// <summary>
    ///     Url of author icon (only supports http(s) and attachments).
    /// </summary>
    string? IconUrl { get; init; }

    /// <summary>
    ///     A proxied url of author icon.
    /// </summary>
    string? ProxyIconUrl { get; init; }

    /// <summary>
    ///     Converts the model back to a discord data model so that it can be send to discord.
    /// </summary>
    /// <returns>
    ///     The converted <see cref="DiscordEmbedAuthorData" />.
    /// </returns>
    DiscordEmbedAuthorData ToDataModel();
}