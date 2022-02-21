using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <inheritdoc cref="IDiscordApplication"/>
    public record DiscordApplicationData
    {
        /// <inheritdoc cref="IDiscordApplication.Id"/>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }
        
        /// <inheritdoc cref="IDiscordApplication.Name"/>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <inheritdoc cref="IDiscordApplication.Icon"/>
        [JsonPropertyName("icon")]
        public string? Icon { get; init; }

        /// <inheritdoc cref="IDiscordApplication.Description"/>
        [JsonPropertyName("description")]
        public string Description { get; init; } = null!;

        /// <inheritdoc cref="IDiscordApplication.CoverImage"/>
        [JsonPropertyName("cover_image")]
        public string? CoverImage { get; init; }
    }
}