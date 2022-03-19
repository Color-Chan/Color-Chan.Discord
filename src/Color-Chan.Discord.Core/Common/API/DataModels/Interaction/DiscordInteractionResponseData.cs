using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    public record DiscordInteractionResponseData
    {
        /// <summary>
        ///     The type of interaction.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordInteractionResponseType Type { get; init; }

        /// <summary>
        ///     An optional response message.
        /// </summary>
        [JsonPropertyName("data")]
        public DiscordInteractionCallbackData? Data { get; init; }
    }
}