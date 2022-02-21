using System;
using Color_Chan.Discord.Core.Common.API.DataModels.Invite;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Application;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Invites;
using Color_Chan.Discord.Rest.Models.Application;
using Color_Chan.Discord.Rest.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Invite;

/// <inheritdoc />
public class DiscordInvite : IDiscordInvite
{       
    /// <summary>
    ///     Initializes a new <see cref="DiscordInvite"/>
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordInvite"/>.</param>
    public DiscordInvite(DiscordInviteData data)
    {
        Code = data.Code;
        PartialGuild = data.PartialGuild is not null ? new DiscordPartialGuild(data.PartialGuild) : null;
        PartialChannel = data.PartialChannel is not null ? new DiscordPartialChannel(data.PartialChannel) : null;
        Inviter = data.Inviter is not null ? new DiscordUser(data.Inviter) : null;
        TargetType = data.TargetType;
        TargetPartialApplication = data.TargetPartialApplication is not null ? new DiscordPartialApplication(data.TargetPartialApplication) : null;
        ApproximatePresenceCount = data.ApproximatePresenceCount;
        ApproximateMemberCount = data.ApproximateMemberCount;
        ExpiresAt = data.ExpiresAt;
        GuildScheduledEvent = data.GuildScheduledEvent is not null ? new DiscordGuildScheduledEvent(data.GuildScheduledEvent) : null;
    }
    
    /// <inheritdoc />
    public string Code { get; set; }
    
    /// <inheritdoc />
    public IDiscordPartialGuild? PartialGuild { get; set; }
    
    /// <inheritdoc />
    public IDiscordPartialChannel? PartialChannel { get; set; }
    
    /// <inheritdoc />
    public IDiscordUser? Inviter { get; set; }
    
    /// <inheritdoc />
    public DiscordInviteTargetType? TargetType { get; set; }
    
    /// <inheritdoc />
    public IDiscordUser? TargetUser { get; set; }
    
    /// <inheritdoc />
    public IDiscordPartialApplication? TargetPartialApplication { get; set; }
    
    /// <inheritdoc />
    public int? ApproximatePresenceCount { get; set; }
    
    /// <inheritdoc />
    public int? ApproximateMemberCount { get; set; }
    
    /// <inheritdoc />
    public DateTimeOffset? ExpiresAt { get; set; }
    
    /// <inheritdoc />
    public IDiscordGuildScheduledEvent? GuildScheduledEvent { get; set; }
}