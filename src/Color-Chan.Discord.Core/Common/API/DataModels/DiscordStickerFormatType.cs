namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <summary>
    ///     Represents a discord Sticker Types API model.
    ///     Docs: https://discord.com/developers/docs/resources/sticker#sticker-object-sticker-format-types
    /// </summary>
    public enum DiscordStickerFormatType
    {
        /// <summary>
        ///     A PNG sticker format.
        /// </summary>
        Png = 1,
        
        /// <summary>
        ///     An  APNG sticker format.
        /// </summary>
        APng = 2,
        
        /// <summary>
        ///     A Lottie sticker format.
        /// </summary>
        Lottie = 3
    }
}