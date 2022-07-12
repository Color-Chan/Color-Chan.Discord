using Color_Chan.Discord.Core.Common.API.DataModels.Select;

namespace Color_Chan.Discord.Core.Common.Models.Select;

/// <summary>
///     Represents a discord Select Option Structure API model.
///     Docs:
///     https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-option-structure
/// </summary>
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

    /// <summary>
    ///     Converts the model back to a discord data model so that it can be send to discord.
    /// </summary>
    /// <returns>
    ///     The converted <see cref="DiscordSelectOptionData" />.
    /// </returns>
    DiscordSelectOptionData ToDataModel();
}