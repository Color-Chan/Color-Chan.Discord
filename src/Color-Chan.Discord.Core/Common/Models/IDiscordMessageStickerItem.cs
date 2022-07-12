namespace Color_Chan.Discord.Core.Common.Models;

/// <summary>
///     Represents a discord Sticker Item Structure API model.
///     Docs: https://discord.com/developers/docs/resources/sticker#sticker-item-object-sticker-item-structure
/// </summary>
public interface IDiscordMessageStickerItem
{
    /// <summary>
    ///     The id of the sticker.
    /// </summary>
    ulong Id { get; set; }

    /// <summary>
    ///     Name of the sticker.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    ///     Type of sticker format.
    /// </summary>
    ulong FormatType { get; set; }
}