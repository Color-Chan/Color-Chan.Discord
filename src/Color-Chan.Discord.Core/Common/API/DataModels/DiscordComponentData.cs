using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Select;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

public record DiscordComponentData
{
    /// <summary>
    ///     The component type.
    /// </summary>
    [JsonPropertyName("type")]
    public DiscordComponentType Type { get; init; }

    /// <summary>
    ///     A developer-defined identifier for the button, max 100 characters.
    /// </summary>
    [JsonPropertyName("custom_id")]
    public string? CustomId { get; init; }

    /// <summary>
    ///     Whether the button is disabled, default false
    /// </summary>
    [JsonPropertyName("disabled")]
    public bool? Disabled { get; init; }

    /// <summary>
    ///     The style the button.
    /// </summary>
    [JsonPropertyName("style")]
    public DiscordButtonStyle? ButtonStyle { get; init; }

    /// <summary>
    ///     Text that appears on the button, max 80 characters.
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    ///     Partial emoji data. Name, id, and animated.
    /// </summary>
    [JsonPropertyName("emoji")]
    public DiscordEmojiData? Emoji { get; init; }

    /// <summary>
    ///     Url for link-style buttons.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    ///     The choices in the select, max 25.
    /// </summary>
    [JsonPropertyName("options")]
    public IEnumerable<DiscordSelectOptionData>? SelectOptions { get; init; }

    /// <summary>
    ///     Custom placeholder text if nothing is selected, max 100 characters.
    /// </summary>
    [JsonPropertyName("placeholder")]
    public string? Placeholder { get; init; }

    /// <summary>
    ///     The minimum number of items that must be chosen; default 1, min 0, max 25.
    /// </summary>
    [JsonPropertyName("min_values")]
    public int? MinValues { get; init; }

    /// <summary>
    ///     The maximum number of items that can be chosen; default 1, max 25.
    /// </summary>
    [JsonPropertyName("max_values")]
    public int? MaxValues { get; init; }

    /// <summary>
    ///     A list of child components.
    /// </summary>
    [JsonPropertyName("components")]
    public IEnumerable<DiscordComponentData>? ChildComponents { get; init; }
}