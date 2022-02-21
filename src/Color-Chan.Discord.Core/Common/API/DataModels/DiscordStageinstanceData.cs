using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <inheritdoc cref="IDiscordStageInstance"/>
    public record DiscordStageinstanceData
    {
        /// <inheritdoc cref="IDiscordStageInstance.Id"/>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <inheritdoc cref="IDiscordStageInstance.GuildId"/>
        [JsonPropertyName("guild_id")]
        public ulong GuildId { get; init; }

        /// <inheritdoc cref="IDiscordStageInstance.ChannelId"/>
        [JsonPropertyName("channel_id")]
        public ulong ChannelId { get; init; }

        /// <inheritdoc cref="IDiscordStageInstance.Topic"/>
        [JsonPropertyName("topic")]
        public string Topic { get; init; } = null!;

        /// <inheritdoc cref="IDiscordStageInstance.PrivacyLevel"/>
        [JsonPropertyName("privacy_level")]
        public DiscordStagePrivacyLevel PrivacyLevel { get; init; }

        /// <inheritdoc cref="IDiscordStageInstance.DiscoverableDisabled"/>
        [JsonPropertyName("discoverable_disabled")]
        public bool DiscoverableDisabled { get; init; }
    }
}