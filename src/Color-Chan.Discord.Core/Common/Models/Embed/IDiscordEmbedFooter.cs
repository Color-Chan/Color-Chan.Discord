using Color_Chan.Discord.Core.Common.API.DataModels.Embed;

namespace Color_Chan.Discord.Core.Common.Models.Embed
{
    /// <summary>
    ///     Represents a discord Embed Footer Structure API model.
    ///     Docs: https://discord.com/developers/docs/resources/channel#embed-object-embed-footer-structure
    /// </summary>
    public interface IDiscordEmbedFooter
    {
        /// <summary>
        ///     Footer text.
        /// </summary>
        string Text { get; init; }

        /// <summary>
        ///     Url of footer icon (only supports http(s) and attachments).
        /// </summary>
        string? IconUrl { get; init; }

        /// <summary>
        ///     A proxied url of footer icon.
        /// </summary>
        string? ProxyIconUrl { get; init; }

        /// <summary>
        ///     Converts the model back to a discord data model so that it can be send to discord.
        /// </summary>
        /// <returns>
        ///     The converted <see cref="DiscordEmbedFooterData" />.
        /// </returns>
        DiscordEmbedFooterData ToDataModel();
    }
}