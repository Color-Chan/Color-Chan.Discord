using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Embed
{
    /// <inheritdoc cref="IDiscordEmbedVideo"/>
    public record DiscordEmbedVideo : IDiscordEmbedVideo
    {
        /// <summary>
        ///     Initializes a new <see cref="DiscordEmbedVideo"/>
        /// </summary>
        public DiscordEmbedVideo()
        {
        }

        /// <summary>
        ///     Initializes a new <see cref="DiscordEmbedVideo"/>
        /// </summary>
        /// <param name="data">The data needed to create the <see cref="DiscordEmbedVideo"/>.</param>
        public DiscordEmbedVideo(DiscordEmbedVideoData data)
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
        public DiscordEmbedVideoData ToDataModel()
        {
            return new DiscordEmbedVideoData
            {
                Height = Height,
                Url = Url,
                Width = Width,
                ProxyUrl = ProxyUrl
            };
        }
    }
}