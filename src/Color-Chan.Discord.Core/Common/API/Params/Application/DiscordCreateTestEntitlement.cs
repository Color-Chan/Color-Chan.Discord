using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Params.Application;

/// <summary>
///     Represents a discord Create Test Entitlement API request model.
/// </summary>
/// <param name="SkuId">The sku to grant entitlement to, as Discord for this value.</param>
/// <param name="OwnerId">The guild_id or user_id to grant entitlement to.</param>
/// <param name="OwnerType">1 for a server subscription, 2 for a user subscription.</param>
public record DiscordCreateTestEntitlement(
    [property: JsonPropertyName("sku_id")]
    ulong SkuId,
    [property: JsonPropertyName("owner_id")]
    ulong OwnerId,
    [property: JsonPropertyName("owner_type")]
    int OwnerType
);