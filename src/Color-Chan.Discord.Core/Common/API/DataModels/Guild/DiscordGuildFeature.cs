namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <summary>
///     The features of a guild.
/// </summary>
public enum DiscordGuildFeature
{
    /// <summary>
    ///     An unknown feature.
    /// </summary>
    Unknown,

    /// <summary>
    ///     Guild has access to set an animated guild banner image.
    /// </summary>
    AnimatedBanner,

    /// <summary>
    ///     Guild has access to set an animated guild icon.
    /// </summary>
    AnimatedIcon,

    /// <summary>
    ///     Guild is using the old permissions configuration behavior.
    /// </summary>
    ApplicationCommandPermissionsV2,

    /// <summary>
    ///     Guild has set up auto moderation rules.
    /// </summary>
    AutoModeration,

    /// <summary>
    ///     Guild has access to set a guild banner image.
    /// </summary>
    Banner,

    /// <summary>
    ///     Guild has access to use commerce features (i.e. create store channels).
    /// </summary>
    Commerce,

    /// <summary>
    ///     Guild can enable welcome screen, Membership Screening, stage channels and discovery, and receives community
    ///     updates.
    /// </summary>
    Community,

    /// <summary>
    ///     Guild has enabled monetization.
    /// </summary>
    CreatorMonetizableProvisional,

    /// <summary>
    ///     Guild has enabled the role subscription promo page.
    /// </summary>
    CreatorStorePage,

    /// <summary>
    ///     Guild has been set as a support server on the App Directory.
    /// </summary>
    DeveloperSupportServer,

    /// <summary>
    ///     Guild is able to be discovered in the directory.
    /// </summary>
    Discoverable,

    /// <summary>
    ///     Guild is able to be featured in the directory.
    /// </summary>
    Featurable,

    /// <summary>
    ///     Guild has paused invites, preventing new users from joining.
    /// </summary>
    InvitesDisabled,

    /// <summary>
    ///     Guild has access to set an invite splash background.
    /// </summary>
    InviteSplash,

    /// <summary>
    ///     Guild has enabled Membership Screening.
    /// </summary>
    MemberVerificationGateEnabled,

    /// <summary>
    ///     Guild has increased custom soundboard sound slots.
    /// </summary>
    MoreSoundboard,

    /// <summary>
    ///     Guild has increased custom sticker slots.
    /// </summary>
    MoreStickers,

    /// <summary>
    ///     Guild has access to create news channels.
    /// </summary>
    News,

    /// <summary>
    ///     Guild is partnered.
    /// </summary>
    Partnered,

    /// <summary>
    ///     Guild can be previewed before joining via Membership Screening or the directory.
    /// </summary>
    PreviewEnabled,
    
    /// <summary>
    ///     Guild has disabled alerts for join raids in the configured safety alerts channel.
    /// </summary>
    RaidAlertsDisabled,
    
    /// <summary>
    ///     Guild is able to set role icons.
    /// </summary>
    RoleIcons,
   
    /// <summary>
    ///     Guild has role subscriptions that can be purchased.
    /// </summary>
    RoleSubscriptionsAvailableForPurchase,
    
    /// <summary>
    ///     Guild has enabled role subscriptions.
    /// </summary>
    RoleSubscriptionsEnabled,
    
    /// <summary>
    ///     Guild has created soundboard sounds.
    /// </summary>
    Soundboard,

    /// <summary>
    ///     Guild has access to set a vanity URL.
    /// </summary>
    VanityUrl,

    /// <summary>
    ///     Guild is verified.
    /// </summary>
    Verified,

    /// <summary>
    ///     Guild has access to set 384kbps bitrate in voice (previously VIP voice servers).
    /// </summary>
    VipRegions,

    /// <summary>
    ///     Guild has enabled the welcome screen.
    /// </summary>
    WelcomeScreenEnabled,

    /// <summary>
    ///     Guild has enabled ticketed events.
    /// </summary>
    TicketedEventsEnabled
}