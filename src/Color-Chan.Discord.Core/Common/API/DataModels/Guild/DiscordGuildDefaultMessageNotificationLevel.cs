namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    public enum DiscordGuildDefaultMessageNotificationLevel : byte
    {
        /// <summary>
        ///     Members will receive notifications for all messages by default.
        /// </summary>
        AllMessages = 0,

        /// <summary>
        ///     Members will receive notifications only for messages that @mention them by default.
        /// </summary>
        OnlyMentions = 1
    }
}