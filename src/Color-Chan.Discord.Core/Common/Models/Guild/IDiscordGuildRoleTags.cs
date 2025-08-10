using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.Models.Guild;

/// <summary>
///     Represents a discord Role Tags API model.
///     Docs: https://discord.com/developers/docs/topics/permissions#role-object-role-tags-structure
/// </summary>
public interface IDiscordGuildRoleTags
{
    /// <summary>
    ///     The id of the bot this role belongs to
    /// </summary>
    public ulong? BotId { get; set; }
    
    /// <summary>
    ///     The id of the integration this role belongs to
    /// </summary>
    public ulong? IntegrationId { get; set; }
    
    /// <summary>
    ///     Whether this is the guild's Booster role
    /// </summary>
    public bool PremiumSubscriber { get; set; }
    
    /// <summary>
    ///     The id of this role's subscription sku and listing
    /// </summary>
    public ulong? SubscriptionListingId { get; set; }
    
    /// <summary>
    ///     Whether this role is available for purchase
    /// </summary>
    public bool AvailableForPurchase { get; set; }
    
    /// <summary>
    ///     Whether this role is a guild's linked role
    /// </summary>
    public bool GuildConnections { get; set; }
    
    /// <summary>
    ///     Converts the model back to a discord data model so that it can be send to discord.
    /// </summary>
    /// <returns>
    ///     The converted <see cref="DiscordGuildRoleTagsData" />.
    /// </returns>
    DiscordGuildRoleTagsData ToDataModel();
}