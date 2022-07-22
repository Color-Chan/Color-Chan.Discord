using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Application;

/// <inheritdoc cref="IDiscordApplicationCommandOption" />
public record DiscordApplicationCommandOptionData
{
    /// <inheritdoc cref="IDiscordApplicationCommandOption.Type" />
    [JsonPropertyName("type")]
    public DiscordApplicationCommandOptionType Type { get; init; }

    /// <inheritdoc cref="IDiscordApplicationCommandOption.Name" />
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    /// <inheritdoc cref="IDiscordApplicationCommandOption.Description" />
    [JsonPropertyName("description")]
    public string Description { get; init; } = null!;

    /// <inheritdoc cref="IDiscordApplicationCommandOption.IsRequired" />
    [JsonPropertyName("required")]
    public bool? IsRequired { get; init; }

    /// <inheritdoc cref="IDiscordApplicationCommandOption.Choices" />
    [JsonPropertyName("choices")]
    public IEnumerable<DiscordApplicationCommandOptionChoiceData>? Choices { get; set; }

    /// <inheritdoc cref="IDiscordApplicationCommandOption.SubOptions" />
    [JsonPropertyName("options")]
    public IEnumerable<DiscordApplicationCommandOptionData>? SubOptions { get; init; }

    /// <inheritdoc cref="IDiscordApplicationCommandOption.ChanelTypes" />
    [JsonPropertyName("channel_types")]
    public IEnumerable<DiscordChannelType>? ChanelTypes { get; set; }

    /// <inheritdoc cref="IDiscordApplicationCommandOption.MinValue" />
    [JsonPropertyName("min_value")]
    public int? MinValue { get; init; }

    /// <inheritdoc cref="IDiscordApplicationCommandOption.MaxValue" />
    [JsonPropertyName("max_value")]
    public int? MaxValue { get; init; }

    /// <inheritdoc cref="IDiscordApplicationCommandOption.Autocomplete" />
    [JsonPropertyName("autocomplete")]
    public bool? Autocomplete { get; init; }
}