using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <summary>
    ///     A user's presence is their current state on a guild. This event is sent when a user's presence or info, such as
    ///     name or avatar, is updated..
    /// </summary>
    /// <remarks>
    ///     If you are using Gateway Intents, you must specify the GUILD_PRESENCES intent in order to receive Presence Update
    ///     events.
    /// </remarks>
    public record DiscordGuildPresenceData
    {
        /// <summary>
        ///     The user presence is being updated for.
        /// </summary>
        [JsonPropertyName("user")]
        public DiscordUserData User { get; set; } = null!;

        /// <summary>
        ///     id of the guild.
        /// </summary>
        [JsonPropertyName("guild_id")]
        public ulong GuildId { get; set; }

        /// <summary>
        ///     Either "idle", "dnd", "online", or "offline".
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = null!;
    }
}