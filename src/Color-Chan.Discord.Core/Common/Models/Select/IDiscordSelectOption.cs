namespace Color_Chan.Discord.Core.Common.Models.Select
{
    public interface IDiscordSelectOption
    {
        /// <summary>
        ///     The user-facing name of the option, max 100 characters.
        /// </summary>
        string Label { get; init; }

        /// <summary>
        ///     The dev-define value of the option, max 100 characters.
        /// </summary>
        string Value { get; init; }

        /// <summary>
        ///     The user-facing name of the option, max 100 characters.
        /// </summary>
        string? Description { get; init; }

        /// <summary>
        ///     The emoji used. Containing the id, name, and animated.
        /// </summary>
        IDiscordEmoji? Emoji { get; init; }

        /// <summary>
        ///     Will render this option as selected by default.
        /// </summary>
        bool? Default { get; init; }
    }
}