namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <summary>
///     Represents a discord Default Message Notification Level API model.
///     Docs: https://discord.com/developers/docs/resources/guild#guild-object-default-message-notification-level
/// </summary>
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