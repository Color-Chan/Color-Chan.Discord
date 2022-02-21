using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Rest.Models.Application
{
    /// <inheritdoc />
    public class DiscordPartialApplication : IDiscordPartialApplication
    {
        /// <summary>
        ///     Initializes a new <see cref="DiscordPartialApplication"/>
        /// </summary>
        /// <param name="data">The data needed to create the <see cref="DiscordPartialApplication"/>.</param>
        public DiscordPartialApplication(DiscordPartialApplicationData data)
        {
            Id = data.Id;
            Name = data.Name;
            Icon = data.Icon;
            Description = data.Description;
            CoverImage = data.CoverImage;
        }

        /// <inheritdoc />
        public ulong? Id { get; init; }

        /// <inheritdoc />
        public string? Name { get; init; }

        /// <inheritdoc />
        public string? Icon { get; init; }

        /// <inheritdoc />
        public string? Description { get; init; }

        /// <inheritdoc />
        public string? CoverImage { get; init; }
    }
}