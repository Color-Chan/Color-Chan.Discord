namespace Color_Chan.Discord.Core.Common.Models.Application
{   
    /// <inheritdoc cref="IDiscordApplication"/>
    public interface IDiscordPartialApplication
    {
        /// <inheritdoc cref="IDiscordApplication.Id"/>
        ulong? Id { get; init; }

        /// <inheritdoc cref="IDiscordApplication.Name"/>
        string? Name { get; init; }

        /// <inheritdoc cref="IDiscordApplication.Icon"/>
        string? Icon { get; init; }

        /// <inheritdoc cref="IDiscordApplication.Description"/>
        string? Description { get; init; }

        /// <inheritdoc cref="IDiscordApplication.CoverImage"/>
        string? CoverImage { get; init; }
    }
}