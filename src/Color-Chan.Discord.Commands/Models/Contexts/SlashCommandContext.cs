using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Commands.Models.Contexts
{
    /// <inheritdoc cref="ISlashCommandContext" />
    public class SlashCommandContext : InteractionContext, ISlashCommandContext
    {
        /// <inheritdoc />
        public IDiscordGuild? Guild { get; init; }

        /// <inheritdoc />
        public IDiscordChannel? Channel { get; init; }

        /// <inheritdoc />
        public IEnumerable<string> SlashCommandName { get; set; } = null!;
    }
}