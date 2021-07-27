using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Models.Embed
{
    public record DiscordEmbedProvider : IDiscordEmbedProvider
    {
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