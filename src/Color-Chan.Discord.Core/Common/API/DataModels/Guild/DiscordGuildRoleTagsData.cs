using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.Converters;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <inheritdoc cref="IDiscordGuildRoleTags" />
[JsonConverter(typeof(DiscordGuildRoleTagsDataConverter))]
public class DiscordGuildRoleTagsData
{
    /// <inheritdoc cref="IDiscordGuildRoleTags.BotId"/>
    [JsonPropertyName("bot_id")]
    public ulong? BotId { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildRoleTags.IntegrationId"/>
    [JsonPropertyName("integration_id")]
    public ulong? IntegrationId { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildRoleTags.PremiumSubscriber"/>
    [JsonPropertyName("premium_subscriber")]
    public bool PremiumSubscriber { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildRoleTags.SubscriptionListingId"/>
    [JsonPropertyName("subscription_listing_id")]
    public ulong? SubscriptionListingId { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildRoleTags.AvailableForPurchase"/>
    [JsonPropertyName("available_for_purchase")]
    public bool AvailableForPurchase { get; set; }
    
    /// <inheritdoc cref="IDiscordGuildRoleTags.GuildConnections"/>
    [JsonPropertyName("guild_connections")]
    public bool GuildConnections { get; set; }
}