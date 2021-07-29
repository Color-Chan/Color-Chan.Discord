using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Application
{
    /// <summary>
    ///     Represents a discord Application Command Option Choice Structure API model.
    ///     https://discord.com/developers/docs/interactions/slash-commands#application-command-object-application-command-option-choice-structure
    /// </summary>
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