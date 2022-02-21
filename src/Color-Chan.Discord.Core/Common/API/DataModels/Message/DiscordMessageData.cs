using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Message
{    
    /// <inheritdoc cref="IDiscordMessage"/>
    public record DiscordMessageData
    {
        /// <inheritdoc cref="IDiscordMessage.Id"/>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <inheritdoc cref="IDiscordMessage.ChannelId"/>
        [JsonPropertyName("channel_id")]
        public ulong? ChannelId { get; init; }

        /// <inheritdoc cref="IDiscordMessage.GuildId"/>
        [JsonPropertyName("guild_id")]
        public ulong? GuildId { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Author"/>
        [JsonPropertyName("author")]
        public DiscordUserData Author { get; init; } = null!;

        /// <inheritdoc cref="IDiscordMessage.Member"/>
        [JsonPropertyName("member")]
        public DiscordGuildMemberData? Member { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Content"/>
        [JsonPropertyName("content")]
        public string Content { get; init; } = null!;

        /// <inheritdoc cref="IDiscordMessage.Timestamp"/>
        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; init; }

        /// <inheritdoc cref="IDiscordMessage.EditedTimestamp"/>
        [JsonPropertyName("edited_timestamp")]
        public DateTimeOffset? EditedTimestamp { get; init; }

        /// <inheritdoc cref="IDiscordMessage.IsTts"/>
        [JsonPropertyName("tts")]
        public bool IsTts { get; init; }

        /// <inheritdoc cref="IDiscordMessage.MentionEveryone"/>
        [JsonPropertyName("mention_everyone")]
        public bool MentionEveryone { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Mentions"/>
        [JsonPropertyName("mentions")]
        public IEnumerable<DiscordUserData> Mentions { get; init; } = new List<DiscordUserData>();

        /// <inheritdoc cref="IDiscordMessage.MentionsRoles"/>
        [JsonPropertyName("mention_roles")]
        public IEnumerable<ulong> MentionsRoles { get; init; } = new List<ulong>();

        /// <inheritdoc cref="IDiscordMessage.MentionsChannel"/>
        [JsonPropertyName("mention_channels")]
        public IEnumerable<ulong>? MentionsChannel { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Attachments"/>
        [JsonPropertyName("attachments")]
        public IEnumerable<DiscordAttachmentData> Attachments { get; init; } = new List<DiscordAttachmentData>();

        /// <inheritdoc cref="IDiscordMessage.Embeds"/>
        [JsonPropertyName("embeds")]
        public IEnumerable<DiscordEmbedData> Embeds { get; init; } = new List<DiscordEmbedData>();

        /// <inheritdoc cref="IDiscordMessage.Reactions"/>
        [JsonPropertyName("reactions")]
        public IEnumerable<DiscordReactionData>? Reactions { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Nonce"/>
        [JsonPropertyName("nonce")]
        public string? Nonce { get; init; }

        /// <inheritdoc cref="IDiscordMessage.IsPinned"/>
        [JsonPropertyName("pinned")]
        public bool IsPinned { get; init; }

        /// <inheritdoc cref="IDiscordMessage.WebhookId"/>
        [JsonPropertyName("webhook_id")]
        public ulong WebhookId { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Type"/>
        [JsonPropertyName("type")]
        public DiscordMessageType Type { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Activity"/>
        [JsonPropertyName("activity")]
        public DiscordMessageActivityData? Activity { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Application"/>
        [JsonPropertyName("Application")]
        public DiscordApplicationData? Application { get; init; }

        /// <inheritdoc cref="IDiscordMessage.ApplicationId"/>
        [JsonPropertyName("application_id")]
        public ulong? ApplicationId { get; init; }

        /// <inheritdoc cref="IDiscordMessage.ReferenceMessage"/>
        [JsonPropertyName("message_reference")]
        public DiscordMessageReferenceData? ReferenceMessage { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Flags"/>
        [JsonPropertyName("lags")]
        public DiscordMessageFlags? Flags { get; init; }

        /// <inheritdoc cref="IDiscordMessage.ReferencedMessage"/>
        [JsonPropertyName("referenced_message")]
        public DiscordMessageData? ReferencedMessage { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Interaction"/>
        [JsonPropertyName("interaction")]
        public DiscordInteractionData? Interaction { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Thread"/>
        [JsonPropertyName("thread")]
        public DiscordChannelData? Thread { get; init; }

        /// <inheritdoc cref="IDiscordMessage.Components"/>
        [JsonPropertyName("components")]
        public IEnumerable<DiscordComponentData>? Components { get; init; }

        /// <inheritdoc cref="IDiscordMessage.StickerItems"/>
        [JsonPropertyName("sticker_items")]
        public IEnumerable<DiscordMessageStickerItemData>? StickerItems { get; init; }

        /// <summary>
        ///     the stickers sent with the message
        /// </summary>
        [Obsolete("Replaced with sticker_items")]
        [JsonPropertyName("stickers")]
        public IEnumerable<DiscordStickerData>? Stickers { get; init; }
    }
}