using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.Params.Guild;
using Color_Chan.Discord.Core.Common.API.Rest.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.API.Rest.Guild
{
    public class DiscordRestGuild : DiscordRestBase
    {
        private readonly IDiscordRestGuildMember _restGuildMember;
        private readonly IDiscordRestGuildRole _restGuildRole;

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRestGuild" />.
        /// </summary>
        /// <inheritdoc />
        public DiscordRestGuild(IDiscordHttpClient httpClient, IDiscordRestGuildRole restGuildRole, IDiscordRestGuildMember restGuildMember) : base(httpClient)
        {
            _restGuildRole = restGuildRole;
            _restGuildMember = restGuildMember;
        }

        /// <inheritdoc cref="IDiscordRestGuildRole" />
        public virtual Task<Result<IReadOnlyList<IDiscordGuildRole>>> GetGuildRolesAsync(ulong guildId, CancellationToken ct = default)
        {
            return _restGuildRole.GetGuildRolesAsync(guildId, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildRole" />
        public virtual Task<Result<IDiscordGuildRole>> CreateGuildRoleAsync(ulong guildId, DiscordCreateGuildRole role, string? auditLogReason = null, CancellationToken ct = default)
        {
            return _restGuildRole.CreateGuildRoleAsync(guildId, role, auditLogReason, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildRole" />
        public virtual Task<Result<IReadOnlyList<IDiscordGuildRole>>> ModifyGuildRolePositionsAsync(ulong guildId, IEnumerable<DiscordModifyGuildRolePositions> modifyGuildRoles,
                                                                                                    string? auditLogReason = null, CancellationToken ct = default)
        {
            return _restGuildRole.ModifyGuildRolePositionsAsync(guildId, modifyGuildRoles, auditLogReason, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildRole" />
        public virtual Task<Result<IDiscordGuildRole>> ModifyGuildRoleAsync(ulong guildId, ulong roleId, DiscordModifyGuildRole role, string? auditLogReason = null,
                                                                            CancellationToken ct = default)
        {
            return _restGuildRole.ModifyGuildRoleAsync(guildId, roleId, role, auditLogReason, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildRole" />
        public virtual Task<Result> ModifyGuildRoleAsync(ulong guildId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default)
        {
            return _restGuildRole.ModifyGuildRoleAsync(guildId, roleId, auditLogReason, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildMember" />
        public virtual Task<Result<IDiscordGuildMember>> GetGuildMemberAsync(ulong guildId, ulong userId, CancellationToken ct = default)
        {
            return _restGuildMember.GetGuildMemberAsync(guildId, userId, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildMember" />
        public virtual Task<Result<IReadOnlyList<IDiscordGuildMember>>> ListGuildMembersAsync(ulong guildId, int limit = 1, ulong afterId = 0, CancellationToken ct = default)
        {
            return _restGuildMember.ListGuildMembersAsync(guildId, limit, afterId, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildMember" />
        public virtual Task<Result<IReadOnlyList<IDiscordGuildMember>>> SearchGuildMembersAsync(ulong guildId, string query, int limit = 1, CancellationToken ct = default)
        {
            return _restGuildMember.SearchGuildMembersAsync(guildId, query, limit, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildMember" />
        public virtual Task<Result<IDiscordGuildMember?>> AddGuildMemberAsync(ulong guildId, ulong userId, DiscordAddGuildMember addGuildMember, CancellationToken ct = default)
        {
            return _restGuildMember.AddGuildMemberAsync(guildId, userId, addGuildMember, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildMember" />
        public virtual Task<Result<IDiscordGuildMember>> ModifyGuildMemberAsync(ulong guildId, ulong userId, DiscordModifyGuildMember modifyGuildMember,
                                                                                string? auditLogReason = null, CancellationToken ct = default)
        {
            return _restGuildMember.ModifyGuildMemberAsync(guildId, userId, modifyGuildMember, auditLogReason, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildMember" />
        public virtual Task<Result> ModifyCurrentUserNickAsync(ulong guildId, DiscordModifyCurrentUserNick modifyGuildMember,
                                                               string? auditLogReason = null, CancellationToken ct = default)
        {
            return _restGuildMember.ModifyCurrentUserNickAsync(guildId, modifyGuildMember, auditLogReason, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildMember" />
        public virtual Task<Result> AddGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default)
        {
            return _restGuildMember.AddGuildMemberRoleAsync(guildId, userId, roleId, auditLogReason, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildMember" />
        public virtual Task<Result> RemoveGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default)
        {
            return _restGuildMember.RemoveGuildMemberRoleAsync(guildId, userId, roleId, auditLogReason, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildMember" />
        public virtual Task<Result> RemoveGuildMemberAsync(ulong guildId, ulong userId, string? auditLogReason = null, CancellationToken ct = default)
        {
            return _restGuildMember.RemoveGuildMemberAsync(guildId, userId, auditLogReason, ct);
        }
    }
}