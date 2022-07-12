using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Message;

/// <inheritdoc cref="IDiscordMessageActivity" />
public record DiscordMessageActivityData
{
    /// <inheritdoc cref="IDiscordMessageActivity.Type" />
    [JsonPropertyName("type")]
    public DiscordMessageActivityType Type { get; init; }

    /// <inheritdoc cref="IDiscordMessageActivity.PartyId" />
    [JsonPropertyName("party_id")]
    public string? PartyId { get; init; }
}