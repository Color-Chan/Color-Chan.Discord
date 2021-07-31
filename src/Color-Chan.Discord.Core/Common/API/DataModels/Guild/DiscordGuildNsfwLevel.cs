namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    /// <summary>
    ///     Represents a discord Guild NSFW Level API model.
    ///     https://discord.com/developers/docs/resources/guild#guild-object-guild-nsfw-level
    /// </summary>
    public enum DiscordGuildNsfwLevel
    {
        /// <summary>
        ///     Default NSFW level.
        /// </summary>
        Default = 0,

        /// <summary>
        ///     Explicit allowed content.
        /// </summary>
        Explicit = 1,

        /// <summary>
        ///     No explicit allowed content.
        /// </summary>
        Safe = 2,

        /// <summary>
        ///     AgeRestricted guild.
        /// </summary>
        AgeRestricted = 3
    }
}