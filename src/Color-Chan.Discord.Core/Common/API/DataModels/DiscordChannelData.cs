using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <summary>
    ///     Represents a guild or DM channel within Discord.
    /// </summary>
    public record DiscordChannelData
    {
        // Shared
        /// <summary>
        ///     The id of this channel.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <summary>
        ///     The type of channel.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordChannelType Type { get; init; }

        /// <summary>
        ///     The id of the last message sent in this channel (may not point to an existing or valid message).
        /// </summary>
        [JsonPropertyName("last_message_id")]
        public ulong? LastMessageId { get; init; }

        //GuildChannel
        /// <summary>
        ///     The id of the guild (may be missing for some channel objects received over gateway guild dispatches).
        /// </summary>
        [JsonPropertyName("guild_id")]
        public ulong? GuildId { get; init; }

        /// <summary>
        ///     The name of the channel (1-100 characters).
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        ///     Sorting position of the channel.
        /// </summary>
        [JsonPropertyName("position")]
        public int? Position { get; init; }

        /// <summary>
        ///     Explicit permission overwrites for members and roles.
        /// </summary>
        [JsonPropertyName("permission_overwrites")]

        public IEnumerable<DiscordOverwriteData>? PermissionOverwrites { get; init; }

        /// <summary>
        ///     For guild channels: id of the parent category for a channel
        ///     (each parent category can contain up to 50 channels),
        ///     for threads: id of the text channel this thread was created
        /// </summary>
        [JsonPropertyName("parent_id")]
        public ulong? CategoryId { get; init; }

        //TextChannel
        /// <summary>
        ///     The channel topic (0-1024 characters).
        /// </summary>
        [JsonPropertyName("topic")]
        public string? Topic { get; init; }

        /// <summary>
        ///     When the last pinned message was pinned.
        ///     This may be null in events such as GUILD_CREATE when a message is not pinned.
        /// </summary>
        [JsonPropertyName("last_pin_timestamp")]

        public DateTimeOffset? LastPinTimestamp { get; init; }

        /// <summary>
        ///     Whether the channel is nsfw
        /// </summary>
        [JsonPropertyName("nsfw")]
        public bool? Nsfw { get; init; }

        /// <summary>
        ///     Amount of seconds a user has to wait before sending another message (0-21600);
        ///     bots, as well as users with the permission manage_messages or manage_channel, are unaffected
        /// </summary>
        [JsonPropertyName("rate_limit_per_user")]

        public int? SlowMode { get; init; }

        //VoiceChannel
        /// <summary>
        ///     The bitrate (in bits) of the voice channel.
        /// </summary>
        [JsonPropertyName("bitrate")]
        public int? Bitrate { get; init; }

        /// <summary>
        ///     The user limit of the voice channel.
        /// </summary>
        [JsonPropertyName("user_limit")]
        public int? UserLimit { get; init; }

        //PrivateChannel
        /// <summary>
        ///     The recipients of the DM.
        /// </summary>
        [JsonPropertyName("recipients")]
        public IEnumerable<DiscordUserData>? Recipients { get; init; }

        //GroupChannel
        /// <summary>
        ///     Icon hash.
        /// </summary>
        [JsonPropertyName("icon")]
        public string? Icon { get; init; }
    }
}