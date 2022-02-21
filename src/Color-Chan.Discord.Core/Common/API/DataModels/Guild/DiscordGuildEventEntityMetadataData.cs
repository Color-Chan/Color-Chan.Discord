using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <inheritdoc cref="IDiscordGuildEventEntityMetadata"/>
public class DiscordGuildEventEntityMetadataData
{
    /// <inheritdoc cref="IDiscordGuildEventEntityMetadata.Location"/>
    [JsonPropertyName("location")]
    public string? Location { get; set; }
}