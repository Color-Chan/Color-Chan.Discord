using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models
{
    public record DiscordAllowedMentions : IDiscordAllowedMentions
    {
        /// <inheritdoc />
        public IEnumerable<DiscordAllowedMentionsType> Allowed { get; init; } = new List<DiscordAllowedMentionsType>();

        /// <inheritdoc />
        public IEnumerable<ulong> AllowedRoles { get; init; } = new List<ulong>();

        /// <inheritdoc />
        public IEnumerable<ulong> AllowedUsers { get; init; } = new List<ulong>();

        /// <inheritdoc />
        public bool ShouldReplyMentionsAuthor { get; init; }

        /// <inheritdoc />
        public DiscordAllowedMentionsData ToDataModel()
        {
            return new()
            {
                Allowed = Allowed,
                AllowedRoles = AllowedRoles,
                AllowedUsers = AllowedUsers,
                ShouldReplyMentionsAuthor = ShouldReplyMentionsAuthor
            };
        }
    }
}