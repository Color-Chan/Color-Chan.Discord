using Color_Chan.Discord.Core.Common.API.DataModels.Embed;

namespace Color_Chan.Discord.Core.Common.Models.Embed
{
    public interface IDiscordEmbedVideo
    {
        /// <summary>
        ///     Source url of video.
        /// </summary>
        string? Url { get; init; }

        /// <summary>
        ///     A proxied url of the video.
        /// </summary>
        string? ProxyUrl { get; init; }

        /// <summary>
        ///     Height of video.
        /// </summary>
        int? Height { get; init; }

        /// <summary>
        ///     Width of video.
        /// </summary>
        int? Width { get; init; }

        /// <summary>
        ///     Converts the model back to a discord data model so that it can be send to discord.
        /// </summary>
        /// <returns>
        ///     The converted <see cref="DiscordEmbedVideoData" />.
        /// </returns>
        DiscordEmbedVideoData ToDataModel();
    }
}