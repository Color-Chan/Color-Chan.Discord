using System;
using Color_Chan.Discord.Core.Common.API.DataModels.Entitlement;

namespace Color_Chan.Discord.Core.Common.Models.Entitlement;

/// <summary>
///     Represents a Discord entitlement.
///     Docs: https://discord.com/developers/docs/game-sdk/store#data-models-entitlement-struct
/// </summary>
public interface IDiscordEntitlement
{
    /// <summary>
    ///     The id of the entitlement.
    /// </summary>
    public ulong Id { get; init; }

    /// <summary>
    ///     The list SKU ids to check entitlements for
    /// </summary>
    public ulong SkuId { get; init; }

    /// <summary>
    ///     The Application id that owns the entitlement.
    /// </summary>
    public ulong ApplicationId { get; init; }

    /// <summary>
    ///     The user id to look up entitlements for
    /// </summary>
    public ulong? UserId { get; init; }

    /// <summary>
    ///     The id of the promotion that the entitlement was used with.
    /// </summary>
    public ulong? PromotionId { get; init; }

    /// <summary>
    ///     The time at which the entitlement starts at.
    ///     This is only present if the entitlement is not a test subscription.
    /// </summary>
    public DateTimeOffset? StartsAt { get; set; }

    /// <summary>
    ///     The time at which the entitlement ends at.
    ///     This is only present if the entitlement is not a test subscription.
    /// </summary>
    public DateTimeOffset? EndsAt { get; set; }

    /// <summary>
    ///    The id of the subscription that was used to bought the entitlement.
    ///    This is only present if the entitlement was bought through a subscription.
    /// </summary>
    public ulong? SubscriptionId { get; set; }

    /// <summary>
    ///     Whether the entitlement has been consumed.
    /// </summary>
    public bool Consumed { get; init; }

    /// <summary>
    ///     Whether the entitlement has been deleted.
    /// </summary>
    public bool Deleted { get; init; }

    /// <summary>
    ///     The type of the entitlement.
    /// </summary>
    public DiscordEntitlementType Type { get; init; }
}