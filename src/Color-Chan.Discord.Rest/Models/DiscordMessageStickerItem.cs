using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models
{
    /// <inheritdoc cref="IDiscordMessageStickerItem"/>
    public class DiscordMessageStickerItem : IDiscordMessageStickerItem
    {
        /// <summary>
        ///     Initializes a new <see cref="DiscordMessageStickerItem"/>
        /// </summary>
        /// <param name="data">The data needed to create the <see cref="DiscordMessageStickerItem"/>.</param>
        public DiscordMessageStickerItem(DiscordMessageStickerItemData data)
        {
            Id = data.Id;
            Name = data.Name;
            FormatType = data.FormatType;
        }

        /// <inheritdoc />
        public ulong Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; } = null!;

        /// <inheritdoc />
        public ulong FormatType { get; set; }
    }
}