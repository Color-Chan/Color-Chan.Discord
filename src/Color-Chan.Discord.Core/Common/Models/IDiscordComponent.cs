using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models
{
    public interface IDiscordComponent
    {
        /// <summary>
        ///     The component type.
        /// </summary>
        DiscordComponentType Type { get; init; }

        /// <summary>
        ///     The style the button.
        /// </summary>
        DiscordButtonStyle? ButtonStyle { get; init; }

        /// <summary>
        ///     Text that appears on the button, max 80 characters.
        /// </summary>
        string? Label { get; init; }

        /// <summary>
        ///     Partial emoji data. Name, id, and animated.
        /// </summary>
        IDiscordEmoji? Emoji { get; init; }

        /// <summary>
        ///     A developer-defined identifier for the button, max 100 characters.
        /// </summary>
        string? CustomId { get; init; }

        /// <summary>
        ///     Url for link-style buttons.
        /// </summary>
        string? Url { get; init; }

        /// <summary>
        ///     Whether the button is disabled, default false
        /// </summary>
        bool? Disabled { get; init; }

        /// <summary>
        ///     A list of child components.
        /// </summary>
        IEnumerable<IDiscordComponent>? ChildComponents { get; init; }

        /// <summary>
        ///     Converts the model back to a discord data model so that it can be send to discord.
        /// </summary>
        /// <returns>
        ///     The converted <see cref="DiscordComponentData" />.
        /// </returns>
        DiscordComponentData ToDataModel();
    }
}