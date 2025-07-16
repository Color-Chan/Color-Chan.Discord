using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Guild;

/// <inheritdoc cref="IDiscordGuildRoleTags" />
public class DiscordGuildRoleTags : IDiscordGuildRoleTags
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordGuildRoleTags" />.
    /// </summary>
    /// <param name="dataTags">The data needed to create the <see cref="DiscordGuildRoleTags" />.</param>
    public DiscordGuildRoleTags(DiscordGuildRoleTagsData dataTags)
    {
        BotId = dataTags.BotId;
        IntegrationId = dataTags.IntegrationId;
        PremiumSubscriber = dataTags.PremiumSubscriber;
        SubscriptionListingId = dataTags.SubscriptionListingId;
        AvailableForPurchase = dataTags.AvailableForPurchase;
        GuildConnections = dataTags.GuildConnections;
    }

    /// <inheritdoc />
    public ulong? BotId { get; set; }

    /// <inheritdoc />
    public ulong? IntegrationId { get; set; }

    /// <inheritdoc />
    public bool PremiumSubscriber { get; set; }

    /// <inheritdoc />
    public ulong? SubscriptionListingId { get; set; }

    /// <inheritdoc />
    public bool AvailableForPurchase { get; set; }

    /// <inheritdoc />
    public bool GuildConnections { get; set; }

    /// <inheritdoc />
    public DiscordGuildRoleTagsData ToDataModel()
    {
        return new DiscordGuildRoleTagsData
        {
            BotId = BotId,
            IntegrationId = IntegrationId,
            PremiumSubscriber = PremiumSubscriber,
            SubscriptionListingId = SubscriptionListingId,
            AvailableForPurchase = AvailableForPurchase,
            GuildConnections = GuildConnections
        };
    }
}