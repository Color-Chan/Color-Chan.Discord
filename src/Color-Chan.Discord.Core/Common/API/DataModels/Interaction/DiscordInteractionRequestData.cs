using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Select;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    /// <summary>
    ///     More info on
    ///     https://discord.com/developers/docs/interactions/slash-commands#interaction-object-application-command-interaction-data-structure.
    /// </summary>
    public record DiscordInteractionRequestData
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
        ///     The type of the invoked command.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordApplicationCommandTypes Type { get; init; }

        /// <summary>
        ///     Converted users + roles + channels.
        /// </summary>
        [JsonPropertyName("resolved")]
        public DiscordInteractionResolvedData? Resolved { get; init; }

        /// <summary>
        ///     The params + values from the user.
        /// </summary>
        [JsonPropertyName("options")]
        public IEnumerable<DiscordInteractionOptionData>? Options { get; init; }

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

        /// <summary>
        ///     The values the user selected.
        /// </summary>
        [JsonPropertyName("values")]
        public DiscordSelectOptionData? Values { get; init; }

        /// <summary>
        ///     Id the of user or message targeted by a user or message command.
        /// </summary>
        [JsonPropertyName("target_id")]
        public ulong? TargetId { get; init; }
    }
}