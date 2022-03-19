using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Application
{
    /// <inheritdoc cref="IDiscordApplicationCommandOptionChoice"/>
    public record DiscordApplicationCommandOptionChoiceData
    {
        /// <inheritdoc cref="IDiscordApplicationCommandOptionChoice.Name"/>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <inheritdoc cref="IDiscordApplicationCommandOptionChoice.RawValue"/>
        [JsonPropertyName("value")]
        public object Value { get; init; } = null!;
    }
}