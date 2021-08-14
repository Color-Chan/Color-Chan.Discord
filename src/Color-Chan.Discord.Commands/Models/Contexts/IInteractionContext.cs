using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Commands.Models.Contexts
{
    /// <summary>
    ///     The context for an interaction command.
    /// </summary>
    public interface IInteractionContext
    {
        /// <summary>
        ///     The guild user that has made the interaction request.
        /// </summary>
        /// <remarks>
        ///     Null when the interaction was used in DMs.
        /// </remarks>
        IDiscordGuildMember? Member { get; init; }

        /// <summary>
        ///     The user that has made the interaction request.
        /// </summary>
        IDiscordUser User { get; init; }

        /// <summary>
        ///     The interaction command that the user has requested. Including the request data.
        /// </summary>
        IDiscordInteractionCommand CommandRequest { get; init; }

        /// <summary>
        ///     The message of the interaction request.
        /// </summary>
        IDiscordMessage? Message { get; init; }

        /// <summary>
        ///     The Guild id of the current guild.
        /// </summary>
        /// <remarks>
        ///     Null when the interaction was used in DMs.
        /// </remarks>
        ulong? GuildId { get; set; }

        /// <summary>
        ///     The Channel id of the current Channel.
        /// </summary>
        /// <remarks>
        ///     This will be the user the id when the interaction was used in DMs.
        /// </remarks>
        ulong ChannelId { get; set; }

        /// <summary>
        ///     Id of the application this interaction is for.
        /// </summary>
        ulong ApplicationId { get; init; }

        /// <summary>
        ///     The token of the interaction.
        /// </summary>
        string Token { get; init; }

        /// <summary>
        ///     The interaction id.
        /// </summary>
        ulong InteractionId { get; init; }
    }
}