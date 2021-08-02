namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
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
        ///     Guild has access to set an animated guild icon.
        /// </summary>
        AnimatedIcon,
        
        /// <summary>
        ///     Guild has access to set a guild banner image.
        /// </summary>
        Banner,
        
        /// <summary>
        ///     Guild has access to use commerce features (i.e. create store channels).
        /// </summary>
        Commerce,
        
        /// <summary>
        ///     Guild can enable welcome screen, Membership Screening, stage channels and discovery, and receives community updates.
        /// </summary>
        Community,
        
        /// <summary>
        ///     Guild is able to be discovered in the directory.
        /// </summary>
        Discoverable,
        
        /// <summary>
        ///     Guild is able to be featured in the directory.
        /// </summary>
        Featurable,
        
        /// <summary>
        ///     Guild has access to set an invite splash background.
        /// </summary>
        InviteSplash,
        
        /// <summary>
        ///     Guild has enabled Membership Screening.
        /// </summary>
        MemberVerificationGateEnabled,
        
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
        ///     Guild has access to set a vanity URL.
        /// </summary>
        VanityUrl,
        
        /// <summary>
        ///     Gild is verified.
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
        TicketedEventsEnabled,
        
        /// <summary>
        ///     Guild has enabled monetization.
        /// </summary>
        MonetizationEnabled,
        
        /// <summary>
        ///     Guild has increased custom sticker slots.
        /// </summary>
        MoreStickers,
        
        /// <summary>
        ///     Guild has access to the three day archive time for threads.
        /// </summary>
        ThreeDayThreadArchive,
        
        /// <summary>
        ///     Guild has access to the seven day archive time for threads.
        /// </summary>
        SevenDayThreadArchive,
        
        /// <summary>
        ///     Guild has access to create private threads.
        /// </summary>
        PrivateThreads
    }
}