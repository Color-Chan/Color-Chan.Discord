using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction;

/// <inheritdoc cref="IDiscordInteractionOption" />
public record DiscordInteractionOptionData
{
    /// <inheritdoc cref="IDiscordInteractionOption.Name" />
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    /// <inheritdoc cref="IDiscordInteractionOption.Type" />
    [JsonPropertyName("type")]
    public DiscordApplicationCommandOptionType Type { get; init; }

    /// <inheritdoc cref="IDiscordInteractionOption.Value" />
    [JsonPropertyName("value")]
    public JsonElement? JsonValue { get; init; }

    /// <inheritdoc cref="IDiscordInteractionOption.SubOptions" />
    [JsonPropertyName("options")]
    public IEnumerable<DiscordInteractionOptionData>? SubOptions { get; init; }
}