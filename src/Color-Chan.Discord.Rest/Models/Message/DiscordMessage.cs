using System;
using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Application;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Message;
using Color_Chan.Discord.Rest.Models.Application;
using Color_Chan.Discord.Rest.Models.Embed;
using Color_Chan.Discord.Rest.Models.Guild;
using Color_Chan.Discord.Rest.Models.Interaction;

namespace Color_Chan.Discord.Rest.Models.Message
{
    public record DiscordMessage : IDiscordMessage
    {
        public DiscordMessage(DiscordMessageData data)
        {
            Id = data.Id;
            ChannelId = data.ChannelId;
            GuildId = data.GuildId;
            Author = new DiscordUser(data.Author);
            Member = data.Member is not null ? new DiscordGuildMember(data.Member) : null;
            Content = data.Content;
            Timestamp = data.Timestamp;
            EditedTimestamp = data.EditedTimestamp;
            IsTts = data.IsTts;
            MentionEveryone = data.MentionEveryone;
            Mentions = data.Mentions.Select(mentionData => new DiscordUser(mentionData));
            MentionsRoles = data.MentionsRoles;
            MentionsChannel = data.MentionsChannel;
            Attachments = data.Attachments.Select(attachmentData => new DiscordAttachment(attachmentData));
            Embeds = data.Embeds.Select(embedData => new DiscordEmbed(embedData));
            Reactions = data.Reactions?.Select(reactionData => new DiscordReaction(reactionData));
            Nonce = data.Nonce;
            IsPinned = data.IsPinned;
            Type = data.Type;
            Activity = data.Activity is not null ? new DiscordMessageActivity(data.Activity) : null;
            Application = data.Application is not null ? new DiscordApplication(data.Application) : null;
            ApplicationId = data.ApplicationId;
            ReferenceMessage = data.ReferenceMessage is not null ? new DiscordMessageReference(data.ReferenceMessage) : null;
            Flags = data.Flags;
            ReferencedMessage = data.ReferencedMessage is not null ? new DiscordMessage(data.ReferencedMessage) : null;
            Interaction = data.Interaction is not null ? new DiscordInteraction(data.Interaction) : null;
            Thread = data.Thread is not null ? new DiscordChannel(data.Thread) : null;
            Components = data.Components?.Select(componentData => new DiscordComponent(componentData));
            StickerItems = data.StickerItems?.Select(componentData => new DiscordMessageStickerItem(componentData));
        }

        /// <inheritdoc />
        public ulong Id { get; init; }

        /// <inheritdoc />
        public ulong? ChannelId { get; set; }

        /// <inheritdoc />
        public ulong? GuildId { get; set; }

        /// <inheritdoc />
        public IDiscordUser Author { get; set; } = null!;

        /// <inheritdoc />
        public IDiscordGuildMember? Member { get; set; }

        /// <inheritdoc />
        public string Content { get; set; } = null!;

        /// <inheritdoc />
        public DateTimeOffset Timestamp { get; init; }

        /// <inheritdoc />
        public DateTimeOffset? EditedTimestamp { get; init; }

        /// <inheritdoc />
        public bool IsTts { get; init; }

        /// <inheritdoc />
        public bool MentionEveryone { get; init; }

        /// <inheritdoc />
        public IEnumerable<IDiscordUser> Mentions { get; set; } = new List<IDiscordUser>();

        /// <inheritdoc />
        public IEnumerable<ulong> MentionsRoles { get; set; } = new List<ulong>();

        /// <inheritdoc />
        public IEnumerable<ulong>? MentionsChannel { get; set; } = new List<ulong>();

        /// <inheritdoc />
        public IEnumerable<IDiscordAttachment> Attachments { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordEmbed> Embeds { get; set; } = new List<IDiscordEmbed>();

        /// <inheritdoc />
        public IEnumerable<IDiscordReaction>? Reactions { get; set; }

        /// <inheritdoc />
        public string? Nonce { get; set; }

        /// <inheritdoc />
        public bool IsPinned { get; set; }

        /// <inheritdoc />
        public ulong WebhookId { get; set; }

        /// <inheritdoc />
        public DiscordMessageType Type { get; set; }

        /// <inheritdoc />
        public IDiscordMessageActivity? Activity { get; set; }

        /// <inheritdoc />
        public IDiscordApplication? Application { get; set; }

        /// <inheritdoc />
        public ulong? ApplicationId { get; set; }

        /// <inheritdoc />
        public IDiscordMessageReference? ReferenceMessage { get; set; }

        /// <inheritdoc />
        public DiscordMessageFlags? Flags { get; set; }

        /// <inheritdoc />
        public IDiscordMessage? ReferencedMessage { get; set; }

        /// <inheritdoc />
        public IDiscordInteraction? Interaction { get; set; }

        /// <inheritdoc />
        public IDiscordChannel? Thread { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordComponent>? Components { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordMessageStickerItem>? StickerItems { get; set; }
    }
}