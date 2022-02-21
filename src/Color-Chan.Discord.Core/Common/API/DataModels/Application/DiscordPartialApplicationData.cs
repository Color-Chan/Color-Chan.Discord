using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Application;

/// <inheritdoc cref="IDiscordPartialApplication"/>
public class DiscordPartialApplicationData
{
    /// <inheritdoc cref="IDiscordPartialApplication.Id"/>
    [JsonPropertyName("id")]
    public ulong? Id { get; init; }
        
    /// <inheritdoc cref="IDiscordPartialApplication.Name"/>
    [JsonPropertyName("name")]
    public string? Name { get; init; } = null!;

    /// <inheritdoc cref="IDiscordPartialApplication.Icon"/>
    [JsonPropertyName("icon")]
    public string? Icon { get; init; }

    /// <inheritdoc cref="IDiscordPartialApplication.Description"/>
    [JsonPropertyName("description")]
    public string? Description { get; init; } = null!;

    /// <inheritdoc cref="IDiscordPartialApplication.CoverImage"/>
    [JsonPropertyName("cover_image")]
    public string? CoverImage { get; init; }
}