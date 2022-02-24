using System;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Rest.Models.Guild;

namespace Color_Chan.Discord.Rest.Models;

/// <inheritdoc cref="IDiscordVoiceState"/>
public class DiscordVoiceState : IDiscordVoiceState
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordVoiceState"/>
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordVoiceState"/>.</param>
    public DiscordVoiceState(DiscordVoiceStateData data)
    {
        GuildId = data.GuildId;
        ChannelId = data.ChannelId;
        UserId = data.UserId;
        Member = data.Member is null ? null : new DiscordGuildMember(data.Member);
        SessionId = data.SessionId;
        Deaf = data.Deaf;
        Mute = data.Mute;
        SelfDeaf = data.SelfDeaf;
        SelfMute = data.SelfMute;
        SelfStream = data.SelfStream;
        SelfVideo = data.SelfVideo;
        Suppress = data.Suppress;
        RequestToSpeakTimestamp = data.RequestToSpeakTimestamp;
    }
    
    /// <inheritdoc />
    public ulong? GuildId { get; init; }
    
    /// <inheritdoc />
    public ulong ChannelId { get; init; }
    
    /// <inheritdoc />
    public ulong UserId { get; init; }
    
    /// <inheritdoc />
    public IDiscordGuildMember? Member { get; init; }
    
    /// <inheritdoc />
    public string SessionId { get; init; }
    
    /// <inheritdoc />
    public bool Deaf { get; init; }
    
    /// <inheritdoc />
    public bool Mute { get; init; }
    
    /// <inheritdoc />
    public bool SelfDeaf { get; init; }
    
    /// <inheritdoc />
    public bool SelfMute { get; init; }
    
    /// <inheritdoc />
    public bool? SelfStream { get; init; }
    
    /// <inheritdoc />
    public bool SelfVideo { get; init; }
    
    /// <inheritdoc />
    public bool Suppress { get; init; }
    
    /// <inheritdoc />
    public DateTimeOffset RequestToSpeakTimestamp { get; init; }
}