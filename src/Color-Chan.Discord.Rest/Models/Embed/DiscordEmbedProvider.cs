using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Embed
{
    public record DiscordEmbedProvider : IDiscordEmbedProvider
    {
        public DiscordEmbedProvider()
        {
        }

        public DiscordEmbedProvider(DiscordEmbedProviderData data)
        {
            Name = data.Name;
            Url = data.Url;
        }

        /// <inheritdoc />
        public string? Name { get; init; }

        /// <inheritdoc />
        public string? Url { get; init; }

        /// <inheritdoc />
        public DiscordEmbedProviderData ToDataModel()
        {
            return new()
            {
                Name = Name,
                Url = Url
            };
        }
    }
}