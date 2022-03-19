using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Rest.Models.Application
{
    public class DiscordApplication : IDiscordApplication
    {
        public DiscordApplication(DiscordApplicationData data)
        {
            Id = data.Id;
            Name = data.Name;
            Icon = data.Icon;
            Description = data.Description;
            CoverImage = data.CoverImage;
        }

        /// <inheritdoc />
        public ulong Id { get; init; }

        /// <inheritdoc />
        public string Name { get; init; }

        /// <inheritdoc />
        public string? Icon { get; init; }

        /// <inheritdoc />
        public string Description { get; init; }

        /// <inheritdoc />
        public string? CoverImage { get; init; }
    }
}