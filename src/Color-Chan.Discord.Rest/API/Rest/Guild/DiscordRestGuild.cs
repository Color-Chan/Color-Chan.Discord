using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Params.Guild;
using Color_Chan.Discord.Core.Common.API.Rest.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.API.Rest.Guild
{
    public class DiscordRestGuild : DiscordRestBase
    {
        private readonly IDiscordRestGuildRole _restGuildRole;

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRestGuild" />.
        /// </summary>
        /// <inheritdoc />
        public DiscordRestGuild(IDiscordHttpClient httpClient, IDiscordRestGuildRole restGuildRole) : base(httpClient)
        {
            _restGuildRole = restGuildRole;
        }
        
        /// <inheritdoc cref="IDiscordRestGuildRole"/>
        public virtual Task<Result<IReadOnlyList<IDiscordGuildRole>>> GetGuildRolesAsync(ulong guildId, CancellationToken ct = default)
        {
            return _restGuildRole.GetGuildRolesAsync(guildId, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildRole"/>
        public virtual Task<Result<IDiscordGuildRole>> CreateGuildRoleAsync(ulong guildId, DiscordCreateGuildRole role, string? auditLogReason = null, CancellationToken ct = default)
        {
            return _restGuildRole.CreateGuildRoleAsync(guildId, role, auditLogReason, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildRole"/>
        public virtual Task<Result<IReadOnlyList<IDiscordGuildRole>>> ModifyGuildRolePositionsAsync(ulong guildId, IEnumerable<DiscordModifyGuildRolePositions> modifyGuildRoles,
                                                                                                             string? auditLogReason = null, CancellationToken ct = default)
        {
            return _restGuildRole.ModifyGuildRolePositionsAsync(guildId, modifyGuildRoles, auditLogReason, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildRole"/>
        public virtual Task<Result<IDiscordGuildRole>> ModifyGuildRoleAsync(ulong guildId, ulong roleId, DiscordModifyGuildRole role, string? auditLogReason = null,
                                                                                     CancellationToken ct = default)
        {
            return _restGuildRole.ModifyGuildRoleAsync(guildId, roleId, role, auditLogReason, ct);
        }

        /// <inheritdoc cref="IDiscordRestGuildRole"/>
        public virtual Task<Result> ModifyGuildRoleAsync(ulong guildId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default)
        {
            return _restGuildRole.ModifyGuildRoleAsync(guildId, roleId, auditLogReason, ct);
        }
    }
}