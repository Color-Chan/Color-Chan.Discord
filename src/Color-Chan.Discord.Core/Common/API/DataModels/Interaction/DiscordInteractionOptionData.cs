using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    public record DiscordInteractionOptionData
    {
        /// <summary>
        ///     The name of the parameter.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <summary>
        ///     Value of application command option type.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordApplicationCommandOptionType Type { get; init; }

        /// <summary>
        ///     The value of the pair.
        /// </summary>
        [JsonPropertyName("value")]
        public JsonElement? JsonValue { get; init; }

        /// <summary>
        ///     Present if this option is a group or subcommand.
        /// </summary>
        [JsonPropertyName("options")]
        public IEnumerable<DiscordInteractionOptionData>? SubOptions { get; init; }
    }
}