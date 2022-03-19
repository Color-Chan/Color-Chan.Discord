using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Embed
{
    public record DiscordEmbedFooter : IDiscordEmbedFooter
    {
        public DiscordEmbedFooter()
        {
        }

        public DiscordEmbedFooter(DiscordEmbedFooterData data)
        {
            Text = data.Text;
            IconUrl = data.IconUrl;
            ProxyIconUrl = data.ProxyIconUrl;
        }

        /// <inheritdoc />
        public string Text { get; init; } = null!;

        /// <inheritdoc />
        public string? IconUrl { get; init; }

        /// <inheritdoc />
        public string? ProxyIconUrl { get; init; }

        /// <inheritdoc />
        public DiscordEmbedFooterData ToDataModel()
        {
            return new DiscordEmbedFooterData
            {
                Text = Text,
                IconUrl = IconUrl,
                ProxyIconUrl = ProxyIconUrl
            };
        }
    }
}