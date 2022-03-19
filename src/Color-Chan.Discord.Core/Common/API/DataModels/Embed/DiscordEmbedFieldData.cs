using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    /// <inheritdoc cref="IDiscordEmbedField"/>
    public record DiscordEmbedFieldData
    {
        /// <inheritdoc cref="IDiscordEmbedField.Name"/>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <inheritdoc cref="IDiscordEmbedField.Value"/>
        [JsonPropertyName("value")]
        public string Value { get; init; } = null!;

        /// <inheritdoc cref="IDiscordEmbedField.Inline"/>
        [JsonPropertyName("inline")]
        public bool? Inline { get; init; }
    }
}