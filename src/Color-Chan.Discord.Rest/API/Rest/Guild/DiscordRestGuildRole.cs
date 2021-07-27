using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Params.Guild;
using Color_Chan.Discord.Core.Common.API.Rest.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Models.Guild;

namespace Color_Chan.Discord.Rest.API.Rest.Guild
{
    public class DiscordRestGuildRole : DiscordRestBase, IDiscordRestGuildRole
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRestGuildRole" />.
        /// </summary>
        /// <inheritdoc />
        public DiscordRestGuildRole(IDiscordHttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildRole>>> GetGuildRolesAsync(ulong guildId, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordGuildRoleData>>(endpoint, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildRole>> CreateGuildRoleAsync(ulong guildId, DiscordCreateGuildRole role, string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles";
            var result = await HttpClient.PostAsync<DiscordGuildRoleData, DiscordCreateGuildRole>(endpoint, role, auditLogReason, ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildRole>>> ModifyGuildRolePositionsAsync(ulong guildId, IEnumerable<DiscordModifyGuildRolePositions> modifyGuildRoles,
                                                                                                          string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles";
            var result = await HttpClient.PatchAsync<IReadOnlyList<DiscordGuildRoleData>, IEnumerable<DiscordModifyGuildRolePositions>>(endpoint, modifyGuildRoles, auditLogReason, ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildRole>> ModifyGuildRoleAsync(ulong guildId, ulong roleId, DiscordModifyGuildRole role, string? auditLogReason = null,
                                                                                  CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles/{roleId.ToString()}";
            var result = await HttpClient.PostAsync<DiscordGuildRoleData, DiscordModifyGuildRole>(endpoint, role, auditLogReason, ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result> ModifyGuildRoleAsync(ulong guildId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles/{roleId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, null, auditLogReason, ct).ConfigureAwait(false);
        }

        private Result<IDiscordGuildRole> ConvertResult(Result<DiscordGuildRoleData> result)
        {
            if (!result.IsSuccessful || result.Entity is null)
            {
                return Result<IDiscordGuildRole>.FromError(null, result.ErrorResult);
            }

            return Result<IDiscordGuildRole>.FromSuccess(new DiscordGuildRole(result.Entity));
        }
        
        private Result<IReadOnlyList<IDiscordGuildRole>> ConvertResult(Result<IReadOnlyList<DiscordGuildRoleData>> result)
        {
            if (!result.IsSuccessful || result.Entity is null)
            {
                return Result<IReadOnlyList<IDiscordGuildRole>>.FromError(null, result.ErrorResult);
            }

            var roles = new List<IDiscordGuildRole>();
            foreach (var roleData in result.Entity)
            {
                roles.Add(new DiscordGuildRole(roleData));
            }

            return Result<IReadOnlyList<IDiscordGuildRole>>.FromSuccess(roles);
        }
    }
}