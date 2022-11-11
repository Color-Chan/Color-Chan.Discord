using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <inheritdoc cref="IDiscordGuildWelcomeScreen" />
public record DiscordGuildWelcomeScreenData
{
    /// <inheritdoc cref="IDiscordGuildWelcomeScreen.Description" />
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <inheritdoc cref="IDiscordGuildWelcomeScreen.WelcomeChannels" />
    [JsonPropertyName("welcome_channels")]
    public IEnumerable<DiscordGuildWelcomeChannelData> WelcomeChannels { get; init; } = new List<DiscordGuildWelcomeChannelData>();
}