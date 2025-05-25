using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <inheritdoc cref="IDiscordUnfurledMediaItem"/>
public class DiscordUnfurledMediaItemData
{
    /// <inheritdoc cref="IDiscordUnfurledMediaItem.Url" />
    [JsonPropertyName("url")]
    public required string Url { get; init; }
}