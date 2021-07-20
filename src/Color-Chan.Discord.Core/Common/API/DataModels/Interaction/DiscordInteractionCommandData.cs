using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    /// <summary>
    ///     More info on
    ///     https://discord.com/developers/docs/interactions/slash-commands#interaction-object-application-command-interaction-data-structure.
    /// </summary>
    public record DiscordInteractionCommandData
    {
        /// <summary>
        ///     The ID of the invoked command.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <summary>
        ///     The name of the invoked command.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <summary>
        ///     Converted users + roles + channels.
        /// </summary>
        [JsonPropertyName("resolved")]
        public DiscordInteractionCommandResolvedData? Resolved { get; init; }

        /// <summary>
        ///     The params + values from the user.
        /// </summary>
        [JsonPropertyName("options")]
        public IEnumerable<DiscordInteractionCommandOptionData>? Options { get; set; }

        /// <summary>
        ///     For components, the custom_id of the component.
        /// </summary>
        [JsonPropertyName("custom_id")]
        public string? CustomId { get; init; }

        /// <summary>
        ///     For components, the type of the component.
        /// </summary>
        [JsonPropertyName("component_type")]
        public string? ComponentType { get; init; }
    }
}