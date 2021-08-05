﻿using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models
{
    public record DiscordAllowedMentions : IDiscordAllowedMentions
    {
        public DiscordAllowedMentions(DiscordAllowedMentionsData data)
        {
            Allowed = data.Allowed;
            AllowedRoles = data.AllowedRoles;
            AllowedUsers = data.AllowedUsers;
            ShouldReplyMentionsAuthor = data.ShouldReplyMentionsAuthor;
        }
        
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
            return new DiscordAllowedMentionsData
            {
                Allowed = Allowed,
                AllowedRoles = AllowedRoles,
                AllowedUsers = AllowedUsers,
                ShouldReplyMentionsAuthor = ShouldReplyMentionsAuthor
            };
        }
    }
}