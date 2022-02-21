using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Embed
{
    /// <inheritdoc cref="IDiscordEmbedAuthor"/>
    public record DiscordEmbedAuthor : IDiscordEmbedAuthor
    {
        /// <summary>
        ///     Initializes a new <see cref="DiscordEmbedAuthor"/>
        /// </summary>
        public DiscordEmbedAuthor()
        {
        }

        /// <summary>
        ///     Initializes a new <see cref="DiscordEmbedAuthor"/>
        /// </summary>
        /// <param name="data">The data needed to create the <see cref="DiscordEmbedAuthor"/>.</param>
        public DiscordEmbedAuthor(DiscordEmbedAuthorData data)
        {
            Name = data.Name;
            Url = data.Url;
            IconUrl = data.IconUrl;
            ProxyIconUrl = data.ProxyIconUrl;
        }

        /// <inheritdoc />
        public string? Name { get; init; }

        /// <inheritdoc />
        public string? Url { get; init; }

        /// <inheritdoc />
        public string? IconUrl { get; init; }

        /// <inheritdoc />
        public string? ProxyIconUrl { get; init; }

        /// <inheritdoc />
        public DiscordEmbedAuthorData ToDataModel()
        {
            return new DiscordEmbedAuthorData
            {
                Name = Name,
                Url = Url,
                IconUrl = IconUrl,
                ProxyIconUrl = ProxyIconUrl
            };
        }
    }
}