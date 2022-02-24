using System;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <inheritdoc cref="IDiscordVoiceState"/>
    public record DiscordVoiceStateData
    {
        /// <inheritdoc cref="IDiscordVoiceState.GuildId"/>
        [JsonPropertyName("unavailable")]
        public ulong? GuildId { get; init; }

        /// <inheritdoc cref="IDiscordVoiceState.ChannelId"/>
        [JsonPropertyName("channel_id")]
        public ulong ChannelId { get; init; }

        /// <inheritdoc cref="IDiscordVoiceState.UserId"/>
        [JsonPropertyName("user_id")]
        public ulong UserId { get; init; }

        /// <inheritdoc cref="IDiscordVoiceState.Member"/>
        [JsonPropertyName("member")]
        public DiscordGuildMemberData? Member { get; init; }

        /// <inheritdoc cref="IDiscordVoiceState.SessionId"/>
        [JsonPropertyName("session_id")]
        public string SessionId { get; init; } = null!;

        /// <inheritdoc cref="IDiscordVoiceState.Deaf"/>
        [JsonPropertyName("deaf")]
        public bool Deaf { get; init; }

        /// <inheritdoc cref="IDiscordVoiceState.Mute"/>
        [JsonPropertyName("mute")]
        public bool Mute { get; init; }

        /// <inheritdoc cref="IDiscordVoiceState.SelfDeaf"/>
        [JsonPropertyName("self_deaf")]
        public bool SelfDeaf { get; init; }

        /// <inheritdoc cref="IDiscordVoiceState.SelfMute"/>
        [JsonPropertyName("self_mute")]
        public bool SelfMute { get; init; }

        /// <inheritdoc cref="IDiscordVoiceState.SelfStream"/>
        [JsonPropertyName("self_stream")]
        public bool? SelfStream { get; init; }

        /// <inheritdoc cref="IDiscordVoiceState.SelfVideo"/>
        [JsonPropertyName("self_video")]
        public bool SelfVideo { get; init; }

        /// <inheritdoc cref="IDiscordVoiceState.Suppress"/>
        [JsonPropertyName("suppress")]
        public bool Suppress { get; init; }

        /// <inheritdoc cref="IDiscordVoiceState.RequestToSpeakTimestamp"/>
        [JsonPropertyName("request_to_speak_timestamp")]
        public DateTimeOffset RequestToSpeakTimestamp { get; init; }
    }
}