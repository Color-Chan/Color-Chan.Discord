using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Params;
using Color_Chan.Discord.Core.Common.API.Params.Guild;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Core.Common.API.Rest
{
    public interface IDiscordRestRole
    {
        /// <summary>
        ///     Get a list of <see cref="DiscordGuildRoleData"/>s.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of <see cref="DiscordGuildRoleData" />
        ///     with the request results.
        /// </returns>
        Task<Result<IReadOnlyList<DiscordGuildRoleData>>> GetGuildRolesAsync(ulong guildId, CancellationToken ct = default);

        /// <summary>
        ///     Creates a new discord guild role.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="role">The <see cref="DiscordCreateGuildRole"/> containing the new role data.</param>
        /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="DiscordGuildRoleData" /> with the request results.
        /// </returns>
        Task<Result<DiscordGuildRoleData>> CreateGuildRoleAsync(ulong guildId, DiscordCreateGuildRole role, string? auditLogReason = null, CancellationToken ct = default);

        /// <summary>
        ///     Changes the positions of guild roles.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="modifyGuildRoles">The <see cref="DiscordModifyGuildRolePositions"/> containing the new positions.</param>
        /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of <see cref="DiscordGuildRoleData" />
        ///     with the request results.
        /// </returns>
        Task<Result<IReadOnlyList<DiscordGuildRoleData>>> ModifyGuildRolePositionsAsync(ulong guildId, IEnumerable<DiscordModifyGuildRolePositions> modifyGuildRoles, string? auditLogReason = null, CancellationToken ct = default);

        /// <summary>
        ///     Modify a guild role.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="roleId">The role id.</param>
        /// <param name="role">The <see cref="DiscordModifyGuildRole"/> containing the new role data.</param>
        /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="DiscordGuildRoleData" /> with the request results.
        /// </returns>
        Task<Result<DiscordGuildRoleData>> ModifyGuildRoleAsync(ulong guildId, ulong roleId, DiscordModifyGuildRole role, string? auditLogReason = null, CancellationToken ct = default);

        /// <summary>
        ///     Deletes a discord guild role.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="roleId">The role id.</param>
        /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result" /> with the request results.
        /// </returns>
        Task<Result> ModifyGuildRoleAsync(ulong guildId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default);
    }
}