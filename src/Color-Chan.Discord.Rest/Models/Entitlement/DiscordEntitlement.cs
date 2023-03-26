using System;
using Color_Chan.Discord.Core.Common.API.DataModels.Entitlement;
using Color_Chan.Discord.Core.Common.Models.Entitlement;

namespace Color_Chan.Discord.Rest.Models.Entitlement;

/// <inheritdoc />
public class DiscordEntitlement : IDiscordEntitlement
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordEntitlement" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordEntitlement" />.</param>
    public DiscordEntitlement(DiscordEntitlementData data)
    {
        Id = data.Id;
        SkuId = data.SkuId;
        ApplicationId = data.ApplicationId;
        UserId = data.UserId;
        PromotionId = data.PromotionId;
        Consumed = data.Consumed;
        Deleted = data.Deleted;
        Type = data.Type;
        StartAt = data.StartAt;
        EndAt = data.EndAt;
        SubscriptionId = data.SubscriptionId;
    }

    /// <inheritdoc />
    public ulong Id { get; init; }

    /// <inheritdoc />
    public ulong SkuId { get; init; }

    /// <inheritdoc />
    public ulong ApplicationId { get; init; }

    /// <inheritdoc />
    public ulong? UserId { get; init; }

    /// <inheritdoc />
    public ulong? PromotionId { get; init; }

    /// <inheritdoc />
    public DateTimeOffset? StartAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? EndAt { get; set; }

    /// <inheritdoc />
    public ulong? SubscriptionId { get; set; }

    /// <inheritdoc />
    public bool Consumed { get; init; }

    /// <inheritdoc />
    public bool Deleted { get; init; }

    /// <inheritdoc />
    public DiscordEntitlementType Type { get; init; }
}