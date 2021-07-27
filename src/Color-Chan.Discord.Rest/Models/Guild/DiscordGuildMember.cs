using System;
using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Guild
{
    public record DiscordGuildMember : IDiscordGuildMember
    {
        public DiscordGuildMember(DiscordGuildMemberData data)
        {
            if (data.User is not null) User = new DiscordUser(data.User);
            NickName = data.NickName;
            Roles = data.Roles;
            JoinedAt = data.JoinedAt;
            PremiumSince = data.PremiumSince;
            Deaf = data.Deaf;
            Mute = data.Mute;
            Pending = data.Pending;
            Permissions = data.Permissions;
        }

        /// <inheritdoc />
        public IDiscordUser? User { get; init; }

        /// <inheritdoc />
        public string? NickName { get; init; }

        /// <inheritdoc />
        public IEnumerable<ulong> Roles { get; init; }

        /// <inheritdoc />
        public DateTimeOffset JoinedAt { get; init; }

        /// <inheritdoc />
        public DateTimeOffset? PremiumSince { get; init; }

        /// <inheritdoc />
        public bool Deaf { get; init; }

        /// <inheritdoc />
        public bool Mute { get; init; }

        /// <inheritdoc />
        public bool? Pending { get; init; }

        /// <inheritdoc />
        public DiscordGuildPermission? Permissions { get; init; }
    }
}