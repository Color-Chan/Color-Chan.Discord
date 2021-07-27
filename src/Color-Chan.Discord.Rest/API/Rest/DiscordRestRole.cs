using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Params;
using Color_Chan.Discord.Core.Common.API.Params.Guild;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.API.Rest
{
    public class DiscordRestRole : DiscordRestBase, IDiscordRestRole
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRestRole" />.
        /// </summary>
        /// <inheritdoc />
        public DiscordRestRole(IDiscordHttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<DiscordGuildRoleData>>> GetGuildRolesAsync(ulong guildId, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles";
            return await HttpClient.GetAsync<IReadOnlyList<DiscordGuildRoleData>>(endpoint, ct: ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<DiscordGuildRoleData>> CreateGuildRoleAsync(ulong guildId, DiscordCreateGuildRole role, string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles";
            return await HttpClient.PostAsync<DiscordGuildRoleData, DiscordCreateGuildRole>(endpoint, role, auditLogReason, ct).ConfigureAwait(false);
        }
        
        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<DiscordGuildRoleData>>> ModifyGuildRolePositionsAsync(ulong guildId, IEnumerable<DiscordModifyGuildRolePositions> modifyGuildRoles, string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles";
            return await HttpClient.PatchAsync<IReadOnlyList<DiscordGuildRoleData>, IEnumerable<DiscordModifyGuildRolePositions>>(endpoint, modifyGuildRoles, auditLogReason, ct).ConfigureAwait(false);
        }
        
        /// <inheritdoc />
        public virtual async Task<Result<DiscordGuildRoleData>> ModifyGuildRoleAsync(ulong guildId, ulong roleId, DiscordModifyGuildRole role, string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles/{roleId.ToString()}";
            return await HttpClient.PostAsync<DiscordGuildRoleData, DiscordModifyGuildRole>(endpoint, role, auditLogReason, ct).ConfigureAwait(false);
        }
        
        /// <inheritdoc />
        public virtual async Task<Result> ModifyGuildRoleAsync(ulong guildId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles/{roleId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, null, auditLogReason, ct).ConfigureAwait(false);
        }
    }
}