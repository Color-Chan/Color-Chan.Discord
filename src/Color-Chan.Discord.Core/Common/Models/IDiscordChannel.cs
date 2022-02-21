using System;
using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models
{
    /// <summary>
    ///     Represents a discord Channel Structure API model.
    ///     Docs: https://discord.com/developers/docs/resources/channel#channel-object-channel-structure
    /// </summary>
    public interface IDiscordChannel
    {
        // Shared
        /// <summary>
        ///     The id of this channel.
        /// </summary>
        ulong Id { get; init; }

        /// <summary>
        ///     The type of channel.
        /// </summary>
        DiscordChannelType Type { get; init; }

        /// <summary>
        ///     The id of the last message sent in this channel (may not point to an existing or valid message).
        /// </summary>
        ulong? LastMessageId { get; init; }

        //GuildChannel
        /// <summary>
        ///     The id of the guild (may be missing for some channel objects received over gateway guild dispatches).
        /// </summary>
        ulong? GuildId { get; init; }

        /// <summary>
        ///     The name of the channel (1-100 characters).
        /// </summary>
        string? Name { get; init; }

        /// <summary>
        ///     Sorting position of the channel.
        /// </summary>
        int? Position { get; init; }

        /// <summary>
        ///     Explicit permission overwrites for members and roles.
        /// </summary>
        IEnumerable<IDiscordOverwrite>? PermissionOverwrites { get; init; }

        /// <summary>
        ///     For guild channels: id of the parent category for a channel
        ///     (each parent category can contain up to 50 channels),
        ///     for threads: id of the text channel this thread was created
        /// </summary>
        ulong? CategoryId { get; init; }

        //TextChannel
        /// <summary>
        ///     The channel topic (0-1024 characters).
        /// </summary>
        string? Topic { get; init; }

        /// <summary>
        ///     When the last pinned message was pinned.
        ///     This may be null in events such as GUILD_CREATE when a message is not pinned.
        /// </summary>
        DateTimeOffset? LastPinTimestamp { get; init; }

        /// <summary>
        ///     Whether the channel is nsfw
        /// </summary>
        bool? Nsfw { get; init; }

        /// <summary>
        ///     Amount of seconds a user has to wait before sending another message (0-21600);
        ///     bots, as well as users with the permission manage_messages or manage_channel, are unaffected
        /// </summary>
        int? SlowMode { get; init; }

        //VoiceChannel
        /// <summary>
        ///     The bitrate (in bits) of the voice channel.
        /// </summary>
        int? Bitrate { get; init; }

        /// <summary>
        ///     The user limit of the voice channel.
        /// </summary>
        int? UserLimit { get; init; }

        //PrivateChannel
        /// <summary>
        ///     The recipients of the DM.
        /// </summary>
        IEnumerable<IDiscordUser>? Recipients { get; init; }

        //GroupChannel
        /// <summary>
        ///     Icon hash.
        /// </summary>
        string? Icon { get; init; }
    }
}