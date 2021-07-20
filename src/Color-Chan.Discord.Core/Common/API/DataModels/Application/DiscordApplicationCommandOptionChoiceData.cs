using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Application
{
    public record DiscordApplicationCommandOptionChoiceData
    {
        /// <summary>
        ///     1-100 character choice name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        ///     Value of the choice, up to 100 characters if string.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; } = null!;
    }
}