using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Core.Common.Models.Interaction
{
    public interface IDiscordInteractionCommandResolved
    {
        /// <summary>
        ///     The ids and User objects.
        /// </summary>
        IReadOnlyDictionary<ulong, IDiscordUser>? Users { get; init; }

        /// <summary>
        ///     The ids and partial Member objects.
        /// </summary>
        /// <remarks>
        ///     Partial Member objects are missing user, deaf and mute fields.
        /// </remarks>
        IReadOnlyDictionary<ulong, IDiscordGuildMember>? Members { get; init; }

        /// <summary>
        ///     The ids and Role objects.
        /// </summary>
        IReadOnlyDictionary<ulong, IDiscordGuildRole>? Roles { get; init; }

        /// <summary>
        ///     The ids and partial Channel objects.
        /// </summary>
        /// <remarks>
        ///     Partial Channel objects only have id, name, type and permissions fields.
        /// </remarks>
        IReadOnlyDictionary<ulong, IDiscordChannel>? Channels { get; init; }
        
        /// <summary>
        ///     the ids and partial Message objects.
        /// </summary>
        IReadOnlyDictionary<ulong, IDiscordMessage>? Messages { get; init; }
    }
}