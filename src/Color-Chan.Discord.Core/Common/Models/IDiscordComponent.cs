using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models.Select;

namespace Color_Chan.Discord.Core.Common.Models;

/// <summary>
///     Represents a discord components API model.
///     Docs: https://discord.com/developers/docs/interactions/message-components#component-object
/// </summary>
public interface IDiscordComponent
{
    /// <summary>
    ///     32-bit integer used as an optional identifier for component.
    /// </summary>
    /// <remarks>
    ///     The id field is optional and is used to identify components in the response from an interaction that aren't interactive components.
    /// </remarks>
    /// <remarks>
    ///     The id must be unique within the message and is generated sequentially if left empty. Generation of ids won't use another id that exists in the message if you have one defined for another component.
    /// </remarks>
    int? Id { get; init; }
    
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
    ///     The choices in the select, max 25.
    /// </summary>
    public List<IDiscordSelectOption>? SelectOptions { get; init; }

    /// <summary>
    ///     Custom placeholder text if nothing is selected, max 100 characters.
    /// </summary>
    public string? Placeholder { get; init; }

    /// <summary>
    ///     The minimum number of items that must be chosen; default 1, min 0, max 25.
    /// </summary>
    public int? MinValues { get; init; }

    /// <summary>
    ///     The maximum number of items that can be chosen; default 1, max 25.
    /// </summary>
    public int? MaxValues { get; init; }
    
    /// <summary>
    ///     Identifier for a purchasable SKU, only available when using premium-style buttons.
    /// </summary>
    ulong? SkuId { get; init; }
    
    /// <summary>
    ///     Text that will be displayed similar to a message.
    /// </summary>
    string? Content { get; init; }
    
    /// <summary>
    ///     A thumbnail or a button component, with a future possibility of adding more compatible components.
    /// </summary>
    IDiscordComponent Accessory { get; set; }

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