﻿using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Commands.Commands
{
    /// <inheritdoc />
    public class SlashCommandContext : ISlashCommandContext
    {
        /// <inheritdoc />
        public IDiscordGuildMember? Member { get; init; }

        /// <inheritdoc />
        public IDiscordUser User { get; init; } = null!;

        /// <inheritdoc />
        public IDiscordMessage? Message { get; init; }

        /// <inheritdoc />
        public IDiscordInteractionCommand CommandRequest { get; init; } = null!;

        /// <inheritdoc />
        public ulong? GuildId { get; set; }

        /// <inheritdoc />
        public ulong ChannelId { get; set; }

        /// <inheritdoc />
        public ulong ApplicationId { get; init; }
        
        /// <inheritdoc />
        public string Token { get; init; } = null!;
        
        /// <inheritdoc />
        public ulong InteractionId { get; init; }

        /// <inheritdoc />
        public string MethodName { get; set; } = null!;
    }
}