using System;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.Models;

/// <summary>
///     Represents a discord Voice State Structure API model.
///     Docs: https://discord.com/developers/docs/resources/voice#voice-state-object-voice-state-structure
/// </summary>
public interface IDiscordVoiceState
{
    /// <summary>
    ///     The guild id this voice state is for.
    /// </summary>
    public ulong? GuildId { get; init; }

    /// <summary>
    ///     The channel id this user is connected to.
    /// </summary>
    public ulong ChannelId { get; init; }

    /// <summary>
    ///     The user id this voice state is for.
    /// </summary>
    public ulong UserId { get; init; }

    /// <summary>
    ///     The guild member this voice state is for.
    /// </summary>
    public IDiscordGuildMember? Member { get; init; }

    /// <summary>
    ///     The session id for this voice state.
    /// </summary>
    public string SessionId { get; init; }

    /// <summary>
    ///     Whether this user is deafened by the server.
    /// </summary>
    public bool Deaf { get; init; }

    /// <summary>
    ///     Whether this user is muted by the server.
    /// </summary>
    public bool Mute { get; init; }

    /// <summary>
    ///     Whether this user is locally deafened.
    /// </summary>
    public bool SelfDeaf { get; init; }

    /// <summary>
    ///     Whether this user is locally muted.
    /// </summary>
    public bool SelfMute { get; init; }

    /// <summary>
    ///     Whether this user is streaming using "Go Live".
    /// </summary>
    public bool? SelfStream { get; init; }

    /// <summary>
    ///     Whether this user's camera is enabled.
    /// </summary>
    public bool SelfVideo { get; init; }

    /// <summary>
    ///     Whether this user is muted by the current user.
    /// </summary>
    public bool Suppress { get; init; }

    /// <summary>
    ///     The time at which the user requested to speak.
    /// </summary>
    public DateTimeOffset RequestToSpeakTimestamp { get; init; }
}