using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Entitlement;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Message;
using Color_Chan.Discord.Rest.Models.Entitlement;
using Color_Chan.Discord.Rest.Models.Guild;
using Color_Chan.Discord.Rest.Models.Message;

namespace Color_Chan.Discord.Rest.Models.Interaction;

/// <inheritdoc cref="IDiscordInteraction" />
public record DiscordInteraction : IDiscordInteraction
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordInteraction" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordInteraction" />.</param>
    public DiscordInteraction(DiscordInteractionData data)
    {
        Id = data.Id;
        ApplicationId = data.ApplicationId;
        RequestType = data.RequestType;
        if (data.Data is not null) Data = new DiscordInteractionRequest(data.Data);
        GuildId = data.GuildId;
        ChannelId = data.ChannelId;
        if (data.GuildMember is not null) GuildMember = new DiscordGuildMember(data.GuildMember);
        if (data.User is not null) User = new DiscordUser(data.User);
        Token = data.Token;
        Versions = data.Versions;
        if (data.Message is not null) Message = new DiscordMessage(data.Message);
        if (data.Permissions is not null) Permissions = data.Permissions;
        EntitlementSkuIds = data.EntitlementSkuIds;
        Entitlements = data.Entitlements.Select(x => new DiscordEntitlement(x));
    }

    /// <inheritdoc />
    public ulong Id { get; init; }

    /// <inheritdoc />
    public ulong ApplicationId { get; init; }

    /// <inheritdoc />
    public DiscordInteractionRequestType RequestType { get; init; }

    /// <inheritdoc />
    public IDiscordInteractionRequest? Data { get; init; }

    /// <inheritdoc />
    public ulong? GuildId { get; init; }

    /// <inheritdoc />
    public ulong? ChannelId { get; init; }

    /// <inheritdoc />
    public IDiscordGuildMember? GuildMember { get; init; }

    /// <inheritdoc />
    public IDiscordUser? User { get; init; }

    /// <inheritdoc />
    public string Token { get; init; }

    /// <inheritdoc />
    public int Versions { get; init; }

    /// <inheritdoc />
    public IDiscordMessage? Message { get; init; }

    /// <inheritdoc />
    public DiscordPermission? Permissions { get; init; }

    /// <inheritdoc />
    public IEnumerable<ulong> EntitlementSkuIds { get; init; }

    /// <inheritdoc />
    public IEnumerable<IDiscordEntitlement> Entitlements { get; init; }

    /// <inheritdoc />
    public bool IsPingInteraction()
    {
        return RequestType == DiscordInteractionRequestType.Ping;
    }
}