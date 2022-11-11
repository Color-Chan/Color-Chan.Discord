namespace Color_Chan.Discord.Core.Common.Models;

/// <summary>
///     Represents a discord Attachment Structure API model.
///     Docs: https://discord.com/developers/docs/resources/channel#attachment-object-attachment-structure
/// </summary>
public interface IDiscordAttachment
{
    /// <summary>
    ///     Attachment id.
    /// </summary>
    ulong Id { get; init; }

    /// <summary>
    ///     name of file attached.
    /// </summary>
    string FileName { get; init; }

    /// <summary>
    ///     Size of file in bytes.
    /// </summary>
    int Size { get; init; }

    /// <summary>
    ///     Source url of file.
    /// </summary>
    string Url { get; init; }

    /// <summary>
    ///     A proxied url of file.
    /// </summary>
    string ProxyUrl { get; init; }

    /// <summary>
    ///     Height of file (if image).
    /// </summary>
    int? Height { get; init; }

    /// <summary>
    ///     Width of file (if image).
    /// </summary>
    int? Width { get; init; }
}