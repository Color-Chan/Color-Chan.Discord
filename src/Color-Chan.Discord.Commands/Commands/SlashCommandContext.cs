using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Commands.Commands
{
    public class SlashCommandContext : ISlashCommandContext
    {
        public SlashCommandContext(IDiscordGuildMember? member, IDiscordUser user, IDiscordMessage message, IDiscordInteractionCommand command)
        {
            Member = member;
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
    }
}