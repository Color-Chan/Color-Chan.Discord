using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Embed
{
    public record DiscordEmbedAuthor : IDiscordEmbedAuthor
    {
        public DiscordEmbedAuthor()
        {
        }

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
            return new()
            {
                Name = Name,
                Url = Url,
                IconUrl = IconUrl,
                ProxyIconUrl = ProxyIconUrl
            };
        }
    }
}