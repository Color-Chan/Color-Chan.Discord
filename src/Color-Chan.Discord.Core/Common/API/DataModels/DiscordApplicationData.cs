using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    public record DiscordApplicationData
    {
        /// <summary>
        ///     The id of the app.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <summary>
        ///     The name of the app.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <summary>
        ///     The icon hash of the app.
        /// </summary>
        [JsonPropertyName("icon")]
        public string? Icon { get; init; }

        /// <summary>
        ///     The description of the app.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; init; } = null!;

        /// <summary>
        ///     Gets the ID of the embed's image asset.
        /// </summary>
        [JsonPropertyName("cover_image")]
        public string? CoverImage { get; init; }
    }
}