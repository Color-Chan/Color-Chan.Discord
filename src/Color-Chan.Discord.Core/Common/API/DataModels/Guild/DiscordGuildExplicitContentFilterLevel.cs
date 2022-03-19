namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    /// <summary>
    ///     Represents a discord Explicit Content Filter Level API model.
    ///     https://discord.com/developers/docs/resources/guild#guild-object-explicit-content-filter-level
    /// </summary>
    public enum DiscordGuildExplicitContentFilterLevel
    {
        /// <summary>
        ///     Media content will not be scanned.
        /// </summary>
        Disabled = 0,

        /// <summary>
        ///     Media content sent by members without roles will be scanned.
        /// </summary>
        MembersWithoutRoles = 1,

        /// <summary>
        ///     Media content sent by all members will be scanned.
        /// </summary>
        AllMembers = 2
    }
}