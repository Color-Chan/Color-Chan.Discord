using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Embed
{
    /// <inheritdoc cref="IDiscordEmbedProvider"/>
    public record DiscordEmbedProvider : IDiscordEmbedProvider
    {
        /// <summary>
        ///     Initializes a new <see cref="DiscordEmbedProvider"/>
        /// </summary>
        public DiscordEmbedProvider()
        {
        }

        /// <summary>
        ///     Initializes a new <see cref="DiscordEmbedProvider"/>
        /// </summary>
        /// <param name="data">The data needed to create the <see cref="DiscordEmbedProvider"/>.</param>
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
            return new DiscordEmbedProviderData
            {
                Name = Name,
                Url = Url
            };
        }
    }
}