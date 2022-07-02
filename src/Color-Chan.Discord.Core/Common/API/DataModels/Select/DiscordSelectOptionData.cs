using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Select;

public record DiscordSelectOptionData
{
    /// <summary>
    ///     The user-facing name of the option, max 100 characters.
    /// </summary>
    [JsonPropertyName("label")]
    public string Label { get; init; } = null!;

    /// <summary>
    ///     The dev-define value of the option, max 100 characters.
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; init; } = null!;

    /// <summary>
    ///     The user-facing name of the option, max 100 characters.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    ///     The emoji used. Containing the id, name, and animated.
    /// </summary>
    [JsonPropertyName("emoji")]
    public DiscordEmojiData? Emoji { get; init; }

    /// <summary>
    ///     Will render this option as selected by default.
    /// </summary>
    [JsonPropertyName("default")]
    public bool? Default { get; init; }
}