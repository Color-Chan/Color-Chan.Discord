using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Core
{
    /// <summary>
    ///     The context for a interaction command.
    /// </summary>
    public interface ISlashCommandContext
    {
        /// <summary>
        ///     The guild user that has made the interaction request.
        /// </summary>
        /// <remarks>
        ///     Only available when the interaction was send in a Guild.
        /// </remarks>
        IDiscordGuildMember? Member { get; init; }

        /// <summary>
        ///     The user that has made the interaction request.
        /// </summary>
        IDiscordUser User { get; init; }

        /// <summary>
        ///     The interaction command that the user has requested.
        /// </summary>
        IDiscordInteractionCommand Command { get; init; }

        /// <summary>
        ///     The message of the interaction request.
        /// </summary>
        public IDiscordMessage Message { get; init; }
    }
}