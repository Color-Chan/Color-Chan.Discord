namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <summary>
///     Represents a discord Sticker Types API model.
///     Docs: https://discord.com/developers/docs/resources/sticker#sticker-object-sticker-types
/// </summary>
public enum DiscordStickerType
{
    /// <summary>
    ///     An official sticker in a pack, part of Nitro or in a removed purchasable pack
    /// </summary>
    Standard = 1,

    /// <summary>
    ///     A sticker uploaded to a Boosted guild for the guild's members.
    /// </summary>
    Guild = 2
}