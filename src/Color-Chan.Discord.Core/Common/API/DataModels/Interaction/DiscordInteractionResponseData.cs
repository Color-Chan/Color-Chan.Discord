using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction;

/// <inheritdoc cref="IDiscordInteractionResponse" />
public record DiscordInteractionResponseData
{
    /// <inheritdoc cref="IDiscordInteractionResponse.Type" />
    [JsonPropertyName("type")]
    public DiscordInteractionCallbackType Type { get; init; }

    /// <inheritdoc cref="IDiscordInteractionResponse.Data" />
    [JsonPropertyName("data")]
    public DiscordInteractionCallbackData? Data { get; init; }
}