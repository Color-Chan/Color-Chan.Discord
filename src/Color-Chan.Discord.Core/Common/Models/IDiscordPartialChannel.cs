using System;
using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models
{
    /// <inheritdoc cref="IDiscordPartialChannel"/>
    public interface IDiscordPartialChannel
    {
        // Shared
        /// <inheritdoc cref="IDiscordPartialChannel.Id"/>
        ulong? Id { get; init; }

        /// <inheritdoc cref="IDiscordPartialChannel.Type"/>
        DiscordChannelType? Type { get; init; }

        /// <inheritdoc cref="IDiscordPartialChannel.LastMessageId"/>
        ulong? LastMessageId { get; init; }

        //GuildChannel
        /// <inheritdoc cref="IDiscordPartialChannel.GuildId"/>
        ulong? GuildId { get; init; }

        /// <inheritdoc cref="IDiscordPartialChannel.Name"/>
        string? Name { get; init; }

        /// <inheritdoc cref="IDiscordPartialChannel.Position"/>
        int? Position { get; init; }

        /// <inheritdoc cref="IDiscordPartialChannel.PermissionOverwrites"/>
        IEnumerable<IDiscordOverwrite>? PermissionOverwrites { get; init; }

        /// <inheritdoc cref="IDiscordPartialChannel.CategoryId"/>
        ulong? CategoryId { get; init; }

        //TextChannel
        /// <inheritdoc cref="IDiscordPartialChannel.Topic"/>
        string? Topic { get; init; }

        /// <inheritdoc cref="IDiscordPartialChannel.LastPinTimestamp"/>
        DateTimeOffset? LastPinTimestamp { get; init; }

        /// <inheritdoc cref="IDiscordPartialChannel.Nsfw"/>
        bool? Nsfw { get; init; }

        /// <inheritdoc cref="IDiscordPartialChannel.SlowMode"/>
        int? SlowMode { get; init; }

        //VoiceChannel
        /// <inheritdoc cref="IDiscordPartialChannel.Bitrate"/>
        int? Bitrate { get; init; }

        /// <inheritdoc cref="IDiscordPartialChannel.UserLimit"/>
        int? UserLimit { get; init; }

        //PrivateChannel
        /// <inheritdoc cref="IDiscordPartialChannel.Recipients"/>
        IEnumerable<IDiscordUser>? Recipients { get; init; }

        //GroupChannel
        /// <inheritdoc cref="IDiscordPartialChannel.Icon"/>
        string? Icon { get; init; }
    }
}