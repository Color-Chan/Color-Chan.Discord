using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models
{
    public class DiscordMessageStickerItem : IDiscordMessageStickerItem
    {
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