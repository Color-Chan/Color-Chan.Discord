using System;
using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Rest.Models.Message
{
    public record DiscordMessage : IDiscordMessage
    {
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
        public ulong? ApplicationId { get; set; }

        /// <inheritdoc />
        public DiscordMessageFlags? Flags { get; set; }

        /// <inheritdoc />
        public IDiscordInteraction? Interaction { get; set; }

        /// <inheritdoc />
        public IDiscordChannel? Thread { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordComponent>? Components { get; set; }
    }
}