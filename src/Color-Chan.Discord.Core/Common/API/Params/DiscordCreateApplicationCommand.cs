using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Core.Common.API.Params
{
    public record DiscordCreateApplicationCommand
    {
        /// <summary>
        ///     1-32 lowercase character name matching ^[\w-]{1,32}$.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <summary>
        ///     1-100 character description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; init; } = null!;

        /// <summary>
        ///     the parameters for the command.
        /// </summary>
        [JsonPropertyName("options")]
        public IEnumerable<DiscordApplicationCommandOptionData>? Options { get; init; }

        /// <summary>
        ///     Whether the command is enabled by default when the app is added to a guild.
        /// </summary>
        [JsonPropertyName("default_permission")]
        public bool? DefaultPermission { get; init; }
    }
}