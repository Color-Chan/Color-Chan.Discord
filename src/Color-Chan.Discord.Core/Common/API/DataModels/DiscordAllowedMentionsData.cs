using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <inheritdoc cref="IDiscordAllowedMentions"/>
    public record DiscordAllowedMentionsData
    {
        /// <inheritdoc cref="IDiscordAllowedMentions.Allowed"/>
        [JsonPropertyName("parse")]
        public IEnumerable<DiscordAllowedMentionsType> Allowed { get; init; } = new List<DiscordAllowedMentionsType>();

        /// <inheritdoc cref="IDiscordAllowedMentions.AllowedRoles"/>
        [JsonPropertyName("roles")]
        public IEnumerable<ulong> AllowedRoles { get; init; } = new List<ulong>();

        /// <inheritdoc cref="IDiscordAllowedMentions.AllowedUsers"/>
        [JsonPropertyName("users")]
        public IEnumerable<ulong> AllowedUsers { get; init; } = new List<ulong>();

        /// <inheritdoc cref="IDiscordAllowedMentions.ShouldReplyMentionsAuthor"/>
        [JsonPropertyName("replied_user")]
        public bool ShouldReplyMentionsAuthor { get; init; }
    }
}