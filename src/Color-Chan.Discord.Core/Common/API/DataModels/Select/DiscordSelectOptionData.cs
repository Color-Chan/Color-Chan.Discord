using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Select;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Select;

/// <inheritdoc cref="IDiscordSelectOption" />
public record DiscordSelectOptionData
{
    /// <inheritdoc cref="IDiscordSelectOption.Label" />
    [JsonPropertyName("label")]
    public string Label { get; init; } = null!;

    /// <inheritdoc cref="IDiscordSelectOption.Value" />
    [JsonPropertyName("value")]
    public string Value { get; init; } = null!;

    /// <inheritdoc cref="IDiscordSelectOption.Description" />
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <inheritdoc cref="IDiscordSelectOption.Emoji" />
    [JsonPropertyName("emoji")]
    public DiscordEmojiData? Emoji { get; init; }

    /// <inheritdoc cref="IDiscordSelectOption.Default" />
    [JsonPropertyName("default")]
    public bool? Default { get; init; }
}