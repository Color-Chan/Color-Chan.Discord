using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Application
{
    public record DiscordApplicationCommandOptionData
    {
        /// <summary>
        ///     value of application command option type.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordApplicationCommandOptionType Type { get; set; }

        /// <summary>
        ///     1-32 lowercase character name matching ^[\w-]{1,32}$.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        ///     1-100 character description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        /// <summary>
        ///     If the parameter is required or optional--default false.
        /// </summary>
        [JsonPropertyName("required")]
        public bool? IsRequired { get; set; }

        /// <summary>
        ///     Choices for string and int types for the user to pick from.
        /// </summary>
        [JsonPropertyName("choices")]
        public IEnumerable<DiscordApplicationCommandOptionChoiceData>? Choice { get; set; }

        /// <summary>
        ///     If the option is a subcommand or subcommand group type, this nested options will be the parameters.
        /// </summary>
        [JsonPropertyName("options")]
        public IEnumerable<DiscordApplicationCommandOptionData>? SubOptions { get; set; }
    }
}