using System;
using System.Linq;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Core.Extensions;

/// <summary>
///     Contains all the extensions methods for <see cref="IDiscordInteraction" />.
/// </summary>
public static class DiscordInteractionExtensions
{
    /// <summary>
    ///    Check if the interaction has an active <see cref="IDiscordInteraction.Entitlements"/> for the given <paramref name="skuId" />.
    /// </summary>
    /// <param name="interaction">The <see cref="IDiscordInteraction"/> that will be used to look for the <paramref name="skuId"/>.</param>
    /// <param name="skuId">The ID of the SKU used in the <see cref="IDiscordInteraction.Entitlements"/>.</param>
    /// <returns>
    ///    A <see cref="bool" /> indicating if the <paramref name="skuId"/> is active.
    /// </returns>
    public static bool HasActiveEntitlement(this IDiscordInteraction interaction, ulong skuId)
    {
        var now = DateTimeOffset.UtcNow;
        return interaction.Entitlements.Any(x => x.SkuId == skuId && x.StartAt <= now && x.EndAt >= now);
    }
}