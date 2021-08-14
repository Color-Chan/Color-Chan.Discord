using System.Collections.Generic;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Commands.Models.Contexts
{
    /// <summary>
    ///     The context for a slash command.
    /// </summary>
    public interface ISlashCommandContext : IInteractionContext
    {
        /// <summary>
        ///     The guild the slash command was used in.
        /// </summary>
        /// <remarks>
        ///     Always null when <see cref="SlashCommandConfiguration.EnableAutoGetGuild" /> is disabled.
        /// </remarks>
        IDiscordGuild? Guild { get; init; }

        /// <summary>
        ///     The channel the slash command was used in.
        /// </summary>
        /// <remarks>
        ///     Always null when <see cref="SlashCommandConfiguration.EnableAutoGetGuild" /> is disabled.
        /// </remarks>
        IDiscordChannel? Channel { get; init; }

        /// <summary>
        ///     The method name of the slash command.
        /// </summary>
        /// <remarks>
        ///     Used to create a unique string for rate limiting.
        /// </remarks>
        string? MethodName { get; set; }

        /// <summary>
        ///     The full slash command name.
        /// </summary>
        IEnumerable<string> SlashCommandName { get; set; }
    }
}