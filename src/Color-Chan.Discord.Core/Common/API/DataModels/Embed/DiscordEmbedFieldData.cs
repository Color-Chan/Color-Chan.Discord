using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    public record DiscordEmbedFieldData
    {
        /// <summary>
        ///     Name of the field.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <summary>
        ///     Value of the field.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; init; } = null!;

        /// <summary>
        ///     Whether or not this field should display inline.
        /// </summary>
        [JsonPropertyName("inline")]
        public bool? Inline { get; init; }
    }
}