﻿using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Embed
{
    public record DiscordEmbedThumbnail : IDiscordEmbedThumbnail
    {
        public DiscordEmbedThumbnail()
        {
            
        }
        public DiscordEmbedThumbnail(DiscordEmbedThumbnailData data)
        {
            Url = data.Url;
            ProxyUrl = data.ProxyUrl;
            Height = data.Height;
            Width = data.Width;
        }

        /// <inheritdoc />
        public string? Url { get; init; }

        /// <inheritdoc />
        public string? ProxyUrl { get; init; }

        /// <inheritdoc />
        public int? Height { get; init; }

        /// <inheritdoc />
        public int? Width { get; init; }

        /// <inheritdoc />
        public DiscordEmbedThumbnailData ToDataModel()
        {
            return new()
            {
                Height = Height,
                Url = Url,
                Width = Width,
                ProxyUrl = ProxyUrl
            };
        }
    }
}