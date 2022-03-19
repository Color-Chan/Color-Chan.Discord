namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    /// <summary>
    ///     Represents a discord Premium Tier API model.
    ///     Docs: https://discord.com/developers/docs/resources/guild#guild-object-premium-tier
    /// </summary>
    public enum DiscordGuildPremiumTier
    {
        /// <summary>
        ///     Guild has not unlocked any Server Boost perks.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Guild has unlocked Server Boost level 1 perks.
        /// </summary>
        Tier1 = 1,

        /// <summary>
        ///     Guild has unlocked Server Boost level 2 perks.
        /// </summary>
        Tier2 = 2,

        /// <summary>
        ///     Guild has unlocked Server Boost level 3 perks.
        /// </summary>
        Tier3 = 3
    }
}