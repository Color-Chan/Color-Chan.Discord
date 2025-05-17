using System;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Message;

/// <summary>
///     Represents a discord Message Flags API model.
///     Docs: https://discord.com/developers/docs/resources/message#message-object-message-flags
/// </summary>
[Flags]
public enum DiscordMessageFlags
{
    /// <summary>
    ///     Default value for flags, when none are given to a message.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Flag given to messages that have been published to subscribed
    ///     channels (via Channel Following).
    /// </summary>
    CrossPosted = 1 << 0,

    /// <summary>
    ///     Flag given to messages that originated from a message in another
    ///     channel (via Channel Following).
    /// </summary>
    IsCrossPost = 1 << 1,

    /// <summary>
    ///     Flag given to messages that do not display any embeds.
    /// </summary>
    SuppressEmbeds = 1 << 2,

    /// <summary>
    ///     Flag given to messages that the source message for this crossPost
    ///     has been deleted (via Channel Following).
    /// </summary>
    SourceMessageDeleted = 1 << 3,

    /// <summary>
    ///     Flag given to messages that came from the urgent message system.
    /// </summary>
    Urgent = 1 << 4,

    /// <summary>
    ///     This message has an associated thread, with the same id as the message.
    /// </summary>
    HasThread = 1 << 5,

    /// <summary>
    ///     This message is only visible to the user who invoked the Interaction.
    /// </summary>
    Ephemeral = 1 << 6,

    /// <summary>
    ///     This message is an Interaction Response and the bot is "thinking".
    /// </summary>
    Loading = 1 << 7,

    /// <summary>
    ///     This message failed to mention some roles and add their members to the thread.
    /// </summary>
    FailedToMentionSomeRolesInThread = 1 << 8,

    /// <summary>
    ///     This message will not trigger push and desktop notifications.
    /// </summary>
    SuppressNotification = 1 << 12,

    /// <summary>
    ///     This message is a voice message.
    /// </summary>
    IsVoiceMessage = 1 << 13,

    /// <summary>
    ///     This message has a snapshot (via Message Forwarding).
    /// </summary>
    HasSnapshot = 1 << 14,

    /// <summary>
    ///     Allows you to create fully component-driven messages.
    /// </summary>
    IsComponentV2 = 1 << 15,
}