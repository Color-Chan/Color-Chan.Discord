﻿using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    public record DiscordWebhookData
    {
        /// <summary>
        ///     The id of the webhook.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; set; }

        /// <summary>
        ///     The type of the webhook.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordWebhookType Type { get; set; }

        /// <summary>
        ///     The guild id this webhook is for, if any.
        /// </summary>
        [JsonPropertyName("guild_id")]
        public ulong? GuildId { get; set; }

        /// <summary>
        ///     The channel id this webhook is for, if any.
        /// </summary>
        [JsonPropertyName("channel_id")]
        public ulong? ChannelId { get; set; }

        /// <summary>
        ///     The user this webhook was created by (not returned when getting a webhook with its token).
        /// </summary>
        [JsonPropertyName("user")]
        public DiscordUserData? Creator { get; set; }

        /// <summary>
        ///     The default name of the webhook.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     The default user avatar hash of the webhook.
        /// </summary>
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        /// <summary>
        ///     The secure token of the webhook (returned for Incoming Webhooks).
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>
        ///     The bot/OAuth2 application that created this webhook.
        /// </summary>
        [JsonPropertyName("application_id")]
        public ulong? ApplicationId { get; set; }

        /// <summary>
        ///     The guild of the channel that this webhook is following (returned for Channel Follower Webhooks).
        /// </summary>
        [JsonPropertyName("source_guild")]
        public DiscordGuildData? PartialGuild { get; set; }

        /// <summary>
        ///     The channel that this webhook is following (returned for Channel Follower Webhooks).
        /// </summary>
        [JsonPropertyName("source_guild")]
        public DiscordChannelData? PartialChannel { get; set; }

        /// <summary>
        ///     The url used for executing the webhook (returned by the webhooks OAuth2 flow).
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}