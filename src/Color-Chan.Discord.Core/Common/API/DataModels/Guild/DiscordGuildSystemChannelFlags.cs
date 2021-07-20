using System;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    [Flags]
    public enum DiscordGuildSystemChannelFlags
    {
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