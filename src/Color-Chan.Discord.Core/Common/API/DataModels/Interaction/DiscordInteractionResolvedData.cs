using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    /// <inheritdoc cref="IDiscordInteractionResolved"/>
    public record DiscordInteractionResolvedData
    {
        /// <inheritdoc cref="IDiscordInteractionResolved.Users"/>
        [JsonPropertyName("users")]
        public IReadOnlyDictionary<ulong, DiscordUserData>? Users { get; init; }

        /// <inheritdoc cref="IDiscordInteractionResolved.Members"/>
        [JsonPropertyName("members")]
        public IReadOnlyDictionary<ulong, DiscordGuildMemberData>? Members { get; init; }

        /// <inheritdoc cref="IDiscordInteractionResolved.Roles"/>
        [JsonPropertyName("roles")]
        public IReadOnlyDictionary<ulong, DiscordGuildRoleData>? Roles { get; init; }

        /// <inheritdoc cref="IDiscordInteractionResolved.Channels"/>
        [JsonPropertyName("channels")]
        public IReadOnlyDictionary<ulong, DiscordChannelData>? Channels { get; init; }

        /// <inheritdoc cref="IDiscordInteractionResolved.Messages"/>
        [JsonPropertyName("messages")]
        public IReadOnlyDictionary<ulong, DiscordMessageData>? Messages { get; init; }
    }
}