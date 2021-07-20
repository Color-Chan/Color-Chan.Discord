using System.Collections.Generic;

namespace Color_Chan.Discord.Core.Common.Models.Interaction
{
    public interface IDiscordInteractionCommand
    {
        /// <summary>
        ///     The ID of the invoked command.
        /// </summary>
        ulong Id { get; init; }

        /// <summary>
        ///     The name of the invoked command.
        /// </summary>
        string Name { get; init; }

        /// <summary>
        ///     Converted users + roles + channels.
        /// </summary>
        IDiscordInteractionCommandResolved? Resolved { get; init; }

        /// <summary>
        ///     The params + values from the user.
        /// </summary>
        IEnumerable<IDiscordInteractionCommandOption>? Options { get; set; }

        /// <summary>
        ///     For components, the custom_id of the component.
        /// </summary>
        string? CustomId { get; init; }

        /// <summary>
        ///     For components, the type of the component.
        /// </summary>
        string? ComponentType { get; init; }
    }
}