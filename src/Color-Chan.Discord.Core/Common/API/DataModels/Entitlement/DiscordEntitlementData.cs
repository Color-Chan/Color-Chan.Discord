using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Entitlement;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Entitlement;

/// <inheritdoc cref="IDiscordEntitlement" />
public record DiscordEntitlementData
{
    /// <inheritdoc cref="IDiscordEntitlement.Id" />
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    /// <inheritdoc cref="IDiscordEntitlement.SkuId" />
    [JsonPropertyName("sku_id")]
    public ulong SkuId { get; set; }
    
    /// <inheritdoc cref="IDiscordEntitlement.ApplicationId" />
    [JsonPropertyName("application_id")]
    public ulong ApplicationId { get; set; }

    /// <inheritdoc cref="IDiscordEntitlement.UserId" />
    [JsonPropertyName("user_id")]
    public ulong? UserId { get; set; }

    /// <inheritdoc cref="IDiscordEntitlement.PromotionId" />
    [JsonPropertyName("promotion_id")]
    public ulong? PromotionId { get; set; }

    /// <inheritdoc cref="IDiscordEntitlement.Consumed" />
    [JsonPropertyName("consumed")]
    public bool Consumed { get; set; }

    /// <inheritdoc cref="IDiscordEntitlement.Deleted" />
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }

    /// <inheritdoc cref="IDiscordEntitlement.Type" />
    [JsonPropertyName("type")]
    public DiscordEntitlementType Type { get; set; }
}