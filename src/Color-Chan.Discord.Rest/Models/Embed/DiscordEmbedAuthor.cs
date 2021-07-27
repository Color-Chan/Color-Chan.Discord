using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Models.Embed
{
    public record DiscordEmbedAuthor : IDiscordEmbedAuthor
    {
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