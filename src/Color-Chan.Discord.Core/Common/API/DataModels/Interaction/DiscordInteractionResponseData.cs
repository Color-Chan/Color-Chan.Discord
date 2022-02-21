using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    /// <summary>
    ///     Represents a discord Interaction Response Structure API model.
    ///     Docs: https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-interaction-response-structure
    /// </summary>
    public record DiscordInteractionResponseData
    {
        /// <summary>
        ///     The type of interaction.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordInteractionCallbackType Type { get; init; }

        /// <summary>
        ///     An optional response message.
        /// </summary>
        [JsonPropertyName("data")]
        public DiscordInteractionCallbackData? Data { get; init; }
    }
}