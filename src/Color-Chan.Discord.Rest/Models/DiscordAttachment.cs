using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models
{
    public class DiscordAttachment : IDiscordAttachment
    {
        public DiscordAttachment(DiscordAttachmentData data)
        {
            Id = data.Id;
            FileName = data.FileName;
            Size = data.Size;
            Url = data.Url;
            ProxyUrl = data.ProxyUrl;
            Height = data.Height;
            Width = data.Width;
        }

        /// <inheritdoc />
        public ulong Id { get; init; }

        /// <inheritdoc />
        public string FileName { get; init; }

        /// <inheritdoc />
        public int Size { get; init; }

        /// <inheritdoc />
        public string Url { get; init; } 

        /// <inheritdoc />
        public string ProxyUrl { get; init; }
        
        /// <inheritdoc />
        public int? Height { get; init; }

        /// <inheritdoc />
        public int? Width { get; init; }
    }
}