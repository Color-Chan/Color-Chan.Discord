namespace Color_Chan.Discord.Core.Common.Models;

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
    ulong DiscordMessageStickerItemType { get; set; }
}