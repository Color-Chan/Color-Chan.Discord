using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <inheritdoc cref="IDiscordVoiceRegion"/>
public class DiscordVoiceRegionData
{
    /// <inheritdoc cref="IDiscordVoiceRegion.Id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    /// <inheritdoc cref="IDiscordVoiceRegion.Name"/>
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    /// <inheritdoc cref="IDiscordVoiceRegion.Optimal"/>
    [JsonPropertyName("optimal")]
    public bool Optimal { get; set; }
    
    /// <inheritdoc cref="IDiscordVoiceRegion.Depricated"/>
    [JsonPropertyName("deprecated")]
    public bool Deprecated { get; set; }
    
    /// <inheritdoc cref="IDiscordVoiceRegion.Custom"/>
    [JsonPropertyName("custom")]
    public bool Custom { get; set; }
}