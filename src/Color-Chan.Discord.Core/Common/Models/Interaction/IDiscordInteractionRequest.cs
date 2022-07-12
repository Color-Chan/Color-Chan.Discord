using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Select;

namespace Color_Chan.Discord.Core.Common.Models.Interaction;

/// <summary>
///     Represents a discord Interaction Request Structure API model.
///     Docs:
///     https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-data-structure
/// </summary>
public interface IDiscordInteractionRequest
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
    ///     The type of the invoked command.
    /// </summary>
    DiscordApplicationCommandTypes Type { get; set; }

    /// <summary>
    ///     Converted users + roles + channels.
    /// </summary>
    IDiscordInteractionResolved? Resolved { get; init; }

    /// <summary>
    ///     The params + values from the user.
    /// </summary>
    IEnumerable<IDiscordInteractionOption>? Options { get; set; }

    /// <summary>
    ///     For components, the custom_id of the component.
    /// </summary>
    string? CustomId { get; init; }

    /// <summary>
    ///     For components, the type of the component.
    /// </summary>
    DiscordComponentType? ComponentType { get; init; }

    /// <summary>
    ///     The values the user selected.
    /// </summary>
    public IDiscordSelectOption? Values { get; init; }

    /// <summary>
    ///     Id the of user or message targeted by a user or message command.
    /// </summary>
    public ulong? TargetId { get; init; }
}