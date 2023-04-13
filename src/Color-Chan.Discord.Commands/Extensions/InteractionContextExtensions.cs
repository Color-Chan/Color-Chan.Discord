using System;
using System.Linq;
using Color_Chan.Discord.Commands.Models.Contexts;

namespace Color_Chan.Discord.Commands.Extensions;

/// <summary>
///     Contains all the extensions methods for <see cref="IInteractionContext" />.
/// </summary>
public static class InteractionContextExtensions
{
    /// <summary>
    ///    Check if the interaction has an active <see cref="IInteractionContext.Entitlements"/> for the given <paramref name="skuId" />.
    /// </summary>
    /// <param name="context">The <see cref="IInteractionContext"/> that will be used to look for the <paramref name="skuId"/>.</param>
    /// <param name="skuId">The ID of the SKU used in the <see cref="IInteractionContext.Entitlements"/>.</param>
    /// <returns>
    ///    A <see cref="bool" /> indicating if the <paramref name="skuId"/> is active.
    /// </returns>
    public static bool HasActiveEntitlement(this IInteractionContext context, ulong skuId)
    {
        var now = DateTimeOffset.UtcNow;
        return context.Entitlements.Any(
            x => x.SkuId == skuId &&
                x.SubscriptionId is null ||
                (
                    x.StartsAt is not null &&
                    x.StartsAt <= now &&
                    x.EndsAt is not null &&
                    x.EndsAt >= now
                )
        );
    }
}