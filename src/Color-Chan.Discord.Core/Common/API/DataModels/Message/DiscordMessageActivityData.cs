using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Message
{
    /// <summary>
    ///     Represents a discord Message Activity Structure API model.
    ///     Docs: https://discord.com/developers/docs/resources/channel#message-object-message-activity-structure
    /// </summary>
    public record DiscordMessageActivityData
    {
        /// <summary>
        ///     Type of message activity.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordMessageActivityType Type { get; init; }

        /// <summary>
        ///     Party_id from a Rich Presence event.
        /// </summary>
        [JsonPropertyName("party_id")]
        public string? PartyId { get; init; }
    }
}