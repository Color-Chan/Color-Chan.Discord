using System;
using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models;

public record DiscordChannel : IDiscordChannel
{
    public DiscordChannel(DiscordChannelData data)
    {
        Id = data.Id;
        Type = data.Type;
        LastMessageId = data.LastMessageId;
        GuildId = data.GuildId;
        Position = data.Position;
        PermissionOverwrites = data.PermissionOverwrites?.Select(overwriteData => new DiscordOverwrite(overwriteData));
        CategoryId = data.CategoryId;
        Topic = data.Topic;
        LastPinTimestamp = data.LastPinTimestamp;
        Nsfw = data.Nsfw;
        SlowMode = data.SlowMode;
        Bitrate = data.Bitrate;
        UserLimit = data.UserLimit;
        Recipients = data.Recipients?.Select(userData => new DiscordUser(userData));
        Icon = data.Icon;
    }

    /// <inheritdoc />
    public ulong Id { get; init; }

    /// <inheritdoc />
    public DiscordChannelType Type { get; init; }

    /// <inheritdoc />
    public ulong? LastMessageId { get; init; }

    /// <inheritdoc />
    public ulong? GuildId { get; init; }

    /// <inheritdoc />
    public string? Name { get; init; }

    /// <inheritdoc />
    public int? Position { get; init; }

    /// <inheritdoc />
    public IEnumerable<IDiscordOverwrite>? PermissionOverwrites { get; init; }

    /// <inheritdoc />
    public ulong? CategoryId { get; init; }

    /// <inheritdoc />
    public string? Topic { get; init; }

    /// <inheritdoc />
    public DateTimeOffset? LastPinTimestamp { get; init; }

    /// <inheritdoc />
    public bool? Nsfw { get; init; }

    /// <inheritdoc />
    public int? SlowMode { get; init; }

    /// <inheritdoc />
    public int? Bitrate { get; init; }

    /// <inheritdoc />
    public int? UserLimit { get; init; }

    /// <inheritdoc />
    public IEnumerable<IDiscordUser>? Recipients { get; init; }

    /// <inheritdoc />
    public string? Icon { get; init; }
}