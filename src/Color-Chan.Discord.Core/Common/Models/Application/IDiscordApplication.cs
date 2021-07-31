namespace Color_Chan.Discord.Core.Common.Models.Application
{
    public interface IDiscordApplication
    {
        /// <summary>
        ///     The id of the app.
        /// </summary>
        ulong Id { get; init; }

        /// <summary>
        ///     The name of the app.
        /// </summary>
        string Name { get; init; }

        /// <summary>
        ///     The icon hash of the app.
        /// </summary>
        string? Icon { get; init; }

        /// <summary>
        ///     The description of the app.
        /// </summary>
        string Description { get; init; }

        /// <summary>
        ///     Gets the ID of the embed's image asset.
        /// </summary>
        string? CoverImage { get; init; }
    }
}