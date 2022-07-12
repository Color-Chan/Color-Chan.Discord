﻿using System;
using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Application;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Core.Common.Models.Message;

/// <summary>
///     Represents a discord Message Structure API model.
///     Docs: https://discord.com/developers/docs/resources/channel#message-object-message-structure
/// </summary>
public interface IDiscordMessage
{
    /// <summary>
    ///     The Discord provided snowflake id.
    /// </summary>
    ulong Id { get; init; }

    /// <summary>
    ///     Id of the channel the message was sent in.
    /// </summary>
    ulong? ChannelId { get; set; }

    /// <summary>
    ///     Id of the guild the message was sent in.
    /// </summary>
    ulong? GuildId { get; set; }

    /// <summary>
    ///     The author of this message.
    /// </summary>
    /// <remarks>
    ///     Only available when the message is created by a user or a bot.
    ///     Not when the message is create by a webhook.
    /// </remarks>
    IDiscordUser Author { get; set; }

    /// <summary>
    ///     Member properties for this message's author.
    /// </summary>
    /// <remarks>
    ///     The member object exists in MESSAGE_CREATE and MESSAGE_UPDATE events from text-based guild channels,
    ///     provided that the author of the message is not a webhook.
    ///     This allows bots to obtain real-time member data without requiring bots to store member state in memory.
    /// </remarks>
    IDiscordGuildMember? Member { get; set; }

    /// <summary>
    ///     Contents of the message.
    /// </summary>
    string Content { get; set; }

    /// <summary>
    ///     When this message was sent.
    /// </summary>
    DateTimeOffset Timestamp { get; init; }

    /// <summary>
    ///     When this message was edited (or null if never).
    /// </summary>
    DateTimeOffset? EditedTimestamp { get; init; }

    /// <summary>
    ///     Whether this was a TTS message.
    /// </summary>
    bool IsTts { get; init; }

    /// <summary>
    ///     Whether this message mentions everyone.
    /// </summary>
    bool MentionEveryone { get; init; }

    /// <summary>
    ///     Users specifically mentioned in the message.
    /// </summary>
    IEnumerable<IDiscordUser> Mentions { get; set; }

    /// <summary>
    ///     Roles specifically mentioned in this message.
    /// </summary>
    IEnumerable<ulong> MentionsRoles { get; set; }

    /// <summary>
    ///     Channels specifically mentioned in this message.
    /// </summary>
    IEnumerable<ulong>? MentionsChannel { get; set; }

    /// <summary>
    ///     Any attached files.
    /// </summary>
    IEnumerable<IDiscordAttachment> Attachments { get; set; }

    /// <summary>
    ///     Any embedded content.
    /// </summary>
    IEnumerable<IDiscordEmbed> Embeds { get; set; }

    /// <summary>
    ///     Reactions to the message.
    /// </summary>
    IEnumerable<IDiscordReaction>? Reactions { get; set; }

    /// <summary>
    ///     Used for validating a message was sent.
    /// </summary>
    string? Nonce { get; set; }

    /// <summary>
    ///     Whether this message is pinned.
    /// </summary>
    bool IsPinned { get; set; }

    /// <summary>
    ///     If the message is generated by a webhook, this is the webhook's id.
    /// </summary>
    ulong WebhookId { get; set; }

    /// <summary>
    ///     Type of message.
    /// </summary>
    DiscordMessageType Type { get; set; }

    /// <summary>
    ///     The activity data, Sent with Rich Presence-related chat embeds.
    /// </summary>
    IDiscordMessageActivity? Activity { get; set; }

    /// <summary>
    ///     The application data, sent with Rich Presence-related chat embeds.
    /// </summary>
    IDiscordApplication? Application { get; set; }

    /// <summary>
    ///     If the message is a response to an Interaction, this is the id of the interaction's application.
    /// </summary>
    ulong? ApplicationId { get; set; }

    /// <summary>
    ///     Data showing the source of a cross post, channel follow add, pin, or reply message.
    /// </summary>
    IDiscordMessageReference? ReferenceMessage { get; set; }

    /// <summary>
    ///     Message flags combined as a bitfield.
    /// </summary>
    DiscordMessageFlags? Flags { get; set; }

    /// <summary>
    ///     The message associated with <see cref="ReferenceMessage" />.
    /// </summary>
    /// <remarks>
    ///     This field is only returned for messages with a type of 19 (REPLY) or 21 (THREAD_STARTER_MESSAGE).
    ///     If the message is a reply but the referenced_message field is not present,
    ///     the backend did not attempt to fetch the message that was being replied to, so its state is unknown.
    ///     If the field exists but is null, the referenced message was deleted.
    /// </remarks>
    IDiscordMessage? ReferencedMessage { get; set; }

    /// <summary>
    ///     Sent if the message is a response to an Interaction.
    /// </summary>
    IDiscordInteraction? Interaction { get; set; }

    /// <summary>
    ///     The thread that was started from this message, includes thread member object.
    /// </summary>
    IDiscordChannel? Thread { get; set; }

    /// <summary>
    ///     Sent if the message contains components like buttons, action rows, or other interactive components.
    /// </summary>
    IEnumerable<IDiscordComponent>? Components { get; set; }

    /// <summary>
    ///     Sent if the message contains stickers.
    /// </summary>
    /// <remarks>
    ///     Bots cannot send stickers.
    /// </remarks>
    public IEnumerable<IDiscordMessageStickerItem>? StickerItems { get; set; }
}