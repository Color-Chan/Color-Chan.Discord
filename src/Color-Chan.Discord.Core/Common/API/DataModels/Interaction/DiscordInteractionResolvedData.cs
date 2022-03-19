using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    public record DiscordInteractionResolvedData
    {
        /// <summary>
        ///     The ids and User objects.
        /// </summary>
        [JsonPropertyName("users")]
        public IReadOnlyDictionary<ulong, DiscordUserData>? Users { get; init; }

        /// <summary>
        ///     The ids and partial Member objects.
        /// </summary>
        /// <remarks>
        ///     Partial Member objects are missing user, deaf and mute fields.
        /// </remarks>
        [JsonPropertyName("members")]
        public IReadOnlyDictionary<ulong, DiscordGuildMemberData>? Members { get; init; }

        /// <summary>
        ///     The ids and Role objects.
        /// </summary>
        [JsonPropertyName("roles")]
        public IReadOnlyDictionary<ulong, DiscordGuildRoleData>? Roles { get; init; }

        /// <summary>
        ///     The ids and partial Channel objects.
        /// </summary>
        /// <remarks>
        ///     Partial Channel objects only have id, name, type and permissions fields.
        /// </remarks>
        [JsonPropertyName("channels")]
        public IReadOnlyDictionary<ulong, DiscordChannelData>? Channels { get; init; }

        /// <summary>
        ///     The ids and partial Message objects.
        /// </summary>
        [JsonPropertyName("messages")]
        public IReadOnlyDictionary<ulong, DiscordMessageData>? Messages { get; init; }
    }
}