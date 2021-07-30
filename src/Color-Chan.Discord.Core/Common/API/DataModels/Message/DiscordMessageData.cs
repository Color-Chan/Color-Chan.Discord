﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Message
{
    public record DiscordMessageData
    {
        /// <summary>
        ///     Id of the message.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; set; }

        /// <summary>
        ///     Id of the channel the message was sent in.
        /// </summary>
        [JsonPropertyName("channel_id")]
        public ulong? ChannelId { get; set; }

        /// <summary>
        ///     Id of the guild the message was sent in.
        /// </summary>
        [JsonPropertyName("guild_id")]
        public ulong? GuildId { get; set; }

        /// <summary>
        ///     The author of this message.
        /// </summary>
        /// <remarks>
        ///     Only available when the message is created by a user or a bot.
        ///     Not when the message is create by a webhook.
        /// </remarks>
        [JsonPropertyName("author")]
        public DiscordUserData Author { get; set; } = null!;

        /// <summary>
        ///     Member properties for this message's author.
        /// </summary>
        /// <remarks>
        ///     The member object exists in MESSAGE_CREATE and MESSAGE_UPDATE events from text-based guild channels,
        ///     provided that the author of the message is not a webhook.
        ///     This allows bots to obtain real-time member data without requiring bots to store member state in memory.
        /// </remarks>
        [JsonPropertyName("member")]
        public DiscordGuildMemberData? Member { get; set; }

        /// <summary>
        ///     Contents of the message.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = null!;

        /// <summary>
        ///     When this message was sent.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; init; }

        /// <summary>
        ///     When this message was edited (or null if never).
        /// </summary>
        [JsonPropertyName("edited_timestamp")]
        public DateTimeOffset? EditedTimestamp { get; init; }

        /// <summary>
        ///     Whether this was a TTS message.
        /// </summary>
        [JsonPropertyName("tts")]
        public bool IsTts { get; init; }

        /// <summary>
        ///     Whether this message mentions everyone.
        /// </summary>
        [JsonPropertyName("mention_everyone")]
        public bool MentionEveryone { get; init; }

        /// <summary>
        ///     Users specifically mentioned in the message.
        /// </summary>
        [JsonPropertyName("mentions")]
        public IEnumerable<DiscordUserData> Mentions { get; set; } = new List<DiscordUserData>();

        /// <summary>
        ///     Roles specifically mentioned in this message.
        /// </summary>
        [JsonPropertyName("mention_roles")]
        public IEnumerable<ulong> MentionsRoles { get; set; } = new List<ulong>();

        /// <summary>
        ///     Channels specifically mentioned in this message.
        /// </summary>
        [JsonPropertyName("mention_channels")]
        public IEnumerable<ulong>? MentionsChannel { get; set; }

        /// <summary>
        ///     Any attached files.
        /// </summary>
        [JsonPropertyName("attachments")]
        public IEnumerable<DiscordAttachmentData> Attachments { get; set; } = new List<DiscordAttachmentData>();

        /// <summary>
        ///     Any embedded content.
        /// </summary>
        [JsonPropertyName("embeds")]
        public IEnumerable<DiscordEmbedData> Embeds { get; set; } = new List<DiscordEmbedData>();

        /// <summary>
        ///     Reactions to the message.
        /// </summary>
        [JsonPropertyName("embeds")]
        public IEnumerable<DiscordReactionData>? Reactions { get; set; }

        /// <summary>
        ///     Used for validating a message was sent.
        /// </summary>
        [JsonPropertyName("nonce")]
        public string? Nonce { get; set; }

        /// <summary>
        ///     Whether this message is pinned.
        /// </summary>
        [JsonPropertyName("pinned")]
        public bool IsPinned { get; set; }

        /// <summary>
        ///     If the message is generated by a webhook, this is the webhook's id.
        /// </summary>
        [JsonPropertyName("webhook_id")]
        public ulong WebhookId { get; set; }

        /// <summary>
        ///     Type of message.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordMessageType Type { get; set; }

        /// <summary>
        ///     The activity data, Sent with Rich Presence-related chat embeds.
        /// </summary>
        [JsonPropertyName("activity")]
        public DiscordMessageActivityData? Activity { get; set; }

        /// <summary>
        ///     The application data, sent with Rich Presence-related chat embeds.
        /// </summary>
        [JsonPropertyName("Application")]
        public DiscordApplicationData? Application { get; set; }

        /// <summary>
        ///     If the message is a response to an Interaction, this is the id of the interaction's application.
        /// </summary>
        [JsonPropertyName("application_id")]
        public ulong? ApplicationId { get; set; }

        /// <summary>
        ///     Data showing the source of a cross post, channel follow add, pin, or reply message.
        /// </summary>
        [JsonPropertyName("message_reference")]
        public DiscordMessageReferenceData? ReferenceMessage { get; set; }

        /// <summary>
        ///     Message flags combined as a bitfield.
        /// </summary>
        [JsonPropertyName("lags")]
        public DiscordMessageFlags? Flags { get; set; }

        /// <summary>
        ///     The message associated with <see cref="ReferenceMessage" />.
        /// </summary>
        /// <remarks>
        ///     This field is only returned for messages with a type of 19 (REPLY) or 21 (THREAD_STARTER_MESSAGE).
        ///     If the message is a reply but the referenced_message field is not present,
        ///     the backend did not attempt to fetch the message that was being replied to, so its state is unknown.
        ///     If the field exists but is null, the referenced message was deleted.
        /// </remarks>
        [JsonPropertyName("referenced_message")]
        public DiscordMessageData? ReferencedMessage { get; set; }

        /// <summary>
        ///     Sent if the message is a response to an Interaction.
        /// </summary>
        [JsonPropertyName("interaction")]
        public DiscordInteractionData? Interaction { get; set; }

        /// <summary>
        ///     The thread that was started from this message, includes thread member object.
        /// </summary>
        [JsonPropertyName("thread")]
        public DiscordChannelData? Thread { get; set; }

        /// <summary>
        ///     Sent if the message contains components like buttons, action rows, or other interactive components.
        /// </summary>
        [JsonPropertyName("components")]
        public IEnumerable<DiscordComponentData>? Components { get; set; }

        /// <summary>
        ///     Sent if the message contains stickers.
        /// </summary>
        /// <remarks>
        ///     Bots cannot send stickers.
        /// </remarks>
        [JsonPropertyName("sticker_items")]
        public IEnumerable<DiscordMessageStickerItemData>? StickerItems { get; set; }

        /// <summary>
        ///     the stickers sent with the message
        /// </summary>
        [Obsolete("Replaced with sticker_items")]
        [JsonPropertyName("stickers")]

        public IEnumerable<DiscordStickerData>? Stickers { get; set; }
    }
}