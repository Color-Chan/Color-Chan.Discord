using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    public record DiscordInteractionCommandOptionData
    {
        /// <summary>
        ///     The name of the parameter.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        ///     Value of application command option type.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordApplicationCommandOptionType Type { get; set; }

        /// <summary>
        ///     The value of the pair.
        /// </summary>
        [JsonPropertyName("value")]
        public JsonElement? JsonValue { get; set; }

        /// <summary>
        ///     Present if this option is a group or subcommand.
        /// </summary>
        [JsonPropertyName("options")]
        public IEnumerable<DiscordInteractionData>? SubOptions { get; set; }
    }
}