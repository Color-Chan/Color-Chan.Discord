using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Models.Embed
{
    public record DiscordEmbedVideo : IDiscordEmbedVideo
    {
        /// <inheritdoc />
        public string? Url { get; init; }

        /// <inheritdoc />
        public string? ProxyUrl { get; init; }

        /// <inheritdoc />
        public int? Height { get; init; }

        /// <inheritdoc />
        public int? Width { get; init; }

        public DiscordEmbedVideoData ToDataModel()
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