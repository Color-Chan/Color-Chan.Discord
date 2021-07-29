using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <summary>
    ///     Represents a Stage Instance Structure API model.
    ///     https://discord.com/developers/docs/resources/stage-instance#stage-instance-object-stage-instance-structure
    /// </summary>
    public record DiscordStageinstanceData
    {
        /// <summary>
        ///     The id of this Stage instance.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <summary>
        ///     The guild id of the associated Stage channel.
        /// </summary>
        [JsonPropertyName("guild_id")]
        public ulong GuildId { get; init; }

        /// <summary>
        ///     The id of the associated Stage channel.
        /// </summary>
        [JsonPropertyName("channel_id")]
        public ulong ChannelId { get; init; }

        /// <summary>
        ///     The topic of the Stage instance (1-120 characters).
        /// </summary>
        [JsonPropertyName("topic")]
        public string Topic { get; init; } = null!;

        /// <summary>
        ///     The privacy level of the Stage instance.
        /// </summary>
        [JsonPropertyName("privacy_level")]
        public DiscordStagePrivacyLevel PrivacyLevel { get; init; }

        /// <summary>
        ///     Whether or not Stage discovery is disabled.
        /// </summary>
        [JsonPropertyName("discoverable_disabled")]
        public bool DiscoverableDisabled { get; init; }
    }
}