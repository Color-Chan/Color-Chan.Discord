using System;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    /// <summary>
    ///     Represents a discord System Channel Flags API model.
    ///     https://discord.com/developers/docs/resources/guild#guild-object-system-channel-flags
    /// </summary>
    [Flags]
    public enum DiscordSystemChannelFlags
    {
        /// <summary>
        ///     No system flags.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Suppress member join notifications.
        /// </summary>
        SuppressJoinNotifications = 1 << 0,

        /// <summary>
        ///     Suppress server boost notifications.
        /// </summary>
        SuppressPremiumSubscriptions = 1 << 1,

        /// <summary>
        ///     Suppress server setup tips.
        /// </summary>
        SuppressGuildReminderNotifications = 1 << 2
    }
}