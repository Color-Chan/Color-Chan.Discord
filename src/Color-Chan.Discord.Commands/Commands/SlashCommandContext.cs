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
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandContext" />.
        /// </summary>
        /// <param name="user">The <see cref="IDiscordUser"/> that activated the slash command.</param>
        /// <param name="message">The <see cref="IDiscordMessage"/> containing the message of the slash command request.</param>
        /// <param name="command">The <see cref="IDiscordInteractionCommand"/> containing the data for the slash command request.</param>
        public SlashCommandContext(IDiscordUser user, IDiscordMessage message, IDiscordInteractionCommand command)
        {
            User = user;
            Message = message;
            Command = command;
        }

        /// <inheritdoc />
        public IDiscordGuildMember? Member { get; init; }

        /// <inheritdoc />
        public IDiscordUser User { get; init; }

        /// <inheritdoc />
        public IDiscordMessage Message { get; init; }

        /// <inheritdoc />
        public IDiscordInteractionCommand Command { get; init; }

        /// <inheritdoc />
        public ulong? GuildId { get; set; }

        /// <inheritdoc />
        public ulong ChannelId { get; set; }

        /// <inheritdoc />
        public ulong ApplicationId { get; init; }
    }
}