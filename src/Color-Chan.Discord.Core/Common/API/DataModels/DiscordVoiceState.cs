using System;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    public record DiscordVoiceState
    {
        /// <summary>
        ///     The guild id this voice state is for.
        /// </summary>
        [JsonPropertyName("unavailable")]
        public ulong? GuildId { get; init; }

        /// <summary>
        ///     The channel id this user is connected to.
        /// </summary>
        [JsonPropertyName("channel_id")]
        public ulong ChannelId { get; init; }

        /// <summary>
        ///     The user id this voice state is for.
        /// </summary>
        [JsonPropertyName("user_id")]
        public ulong UserId { get; init; }

        /// <summary>
        ///     The guild member this voice state is for.
        /// </summary>
        [JsonPropertyName("member")]
        public DiscordGuildMemberData? Member { get; init; }

        /// <summary>
        ///     The session id for this voice state.
        /// </summary>
        [JsonPropertyName("session_id")]
        public string SessionId { get; init; } = null!;

        /// <summary>
        ///     Whether this user is deafened by the server.
        /// </summary>
        [JsonPropertyName("deaf")]
        public bool Deaf { get; init; }

        /// <summary>
        ///     Whether this user is muted by the server.
        /// </summary>
        [JsonPropertyName("mute")]
        public bool Mute { get; init; }

        /// <summary>
        ///     Whether this user is locally deafened.
        /// </summary>
        [JsonPropertyName("self_deaf")]
        public bool SelfDeaf { get; init; }

        /// <summary>
        ///     Whether this user is locally muted.
        /// </summary>
        [JsonPropertyName("self_mute")]
        public bool SelfMute { get; init; }

        /// <summary>
        ///     Whether this user is streaming using "Go Live".
        /// </summary>
        [JsonPropertyName("self_stream")]
        public bool? SelfStream { get; init; }

        /// <summary>
        ///     Whether this user's camera is enabled.
        /// </summary>
        [JsonPropertyName("self_video")]
        public bool SelfVideo { get; init; }

        /// <summary>
        ///     Whether this user is muted by the current user.
        /// </summary>
        [JsonPropertyName("suppress")]
        public bool Suppress { get; init; }

        /// <summary>
        ///     The time at which the user requested to speak.
        /// </summary>
        [JsonPropertyName("request_to_speak_timestamp")]
        public DateTimeOffset RequestToSpeakTimestamp { get; init; }
    }
}