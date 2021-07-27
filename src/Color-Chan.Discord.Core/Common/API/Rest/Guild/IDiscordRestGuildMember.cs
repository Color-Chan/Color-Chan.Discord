using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.Params.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Core.Common.API.Rest.Guild
{
    public interface IDiscordRestGuildMember
    {
        /// <summary>
        ///     Get a guild member.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="userId">The user id of the member.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordGuildMember" /> with the request results.
        /// </returns>
        Task<Result<IDiscordGuildMember>> GetGuildMemberAsync(ulong guildId, ulong userId, CancellationToken ct = default);

        /// <summary>
        ///     Get a list of guild members.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="limit">max number of members to return (1-1000). Default is 1.</param>
        /// <param name="afterId">the highest user id in the previous page. Default is 0.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of <see cref="IDiscordGuildMember" />
        ///     with the request results.
        /// </returns>
        Task<Result<IReadOnlyList<IDiscordGuildMember>>> ListGuildMembersAsync(ulong guildId, int limit = 1, ulong afterId = 0, CancellationToken ct = default);

        /// <summary>
        ///     Get a list of guild member whose username or nickname starts with a provided string.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="query">Query string to match username(s) and nickname(s) against.</param>
        /// <param name="limit">max number of members to return (1-1000). Default is 1.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of <see cref="IDiscordGuildMember" />
        ///     with the request results.
        /// </returns>
        Task<Result<IReadOnlyList<IDiscordGuildMember>>> SearchGuildMembersAsync(ulong guildId, string query, int limit = 1, CancellationToken ct = default);

        /// <summary>
        ///     Adds a user to the guild, provided you have a valid oauth2 access token for the user with the guilds.join scope.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="userId">The user id of the new member.</param>
        /// <param name="addGuildMember">The <see cref="DiscordAddGuildMember"/> containing the body.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordGuildMember" />.
        ///     <see cref="IDiscordGuildMember" /> will be null if the user was already in the server.
        /// </returns>
        Task<Result<IDiscordGuildMember?>> AddGuildMemberAsync(ulong guildId, ulong userId, DiscordAddGuildMember addGuildMember, CancellationToken ct = default);

        /// <summary>
        ///     Modify attributes of a guild member.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="userId">The user id of the guild member.</param>
        /// <param name="modifyGuildMember">The <see cref="DiscordModifyGuildMember"/> data that will be used to modify the user.</param>
        /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordGuildMember" /> with the request results.
        /// </returns>
        Task<Result<IDiscordGuildMember>> ModifyGuildMemberAsync(ulong guildId, ulong userId, DiscordModifyGuildMember modifyGuildMember,
                                                                 string? auditLogReason = null, CancellationToken ct = default);

        /// <summary>
        ///     Modify the current users nickname.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="modifyGuildMember">The <see cref="DiscordModifyCurrentUserNick"/> data that will be used to modify the user.</param>
        /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result" /> with the request results.
        /// </returns>
        Task<Result> ModifyCurrentUserNickAsync(ulong guildId, DiscordModifyCurrentUserNick modifyGuildMember,
                                                string? auditLogReason = null, CancellationToken ct = default);

        /// <summary>
        ///     Add a role to a guild member.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="roleId">The role id.</param>
        /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result" /> with the request results.
        /// </returns>
        Task<Result> AddGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default);

        /// <summary>
        ///     Removes a role from a guild member.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="roleId">The role id.</param>
        /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result" /> with the request results.
        /// </returns>
        Task<Result> RemoveGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default);

        /// <summary>
        ///     Kick a member from a guild.
        /// </summary>
        /// <param name="guildId">The guild id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result" /> with the request results.
        /// </returns>
        Task<Result> RemoveGuildMemberAsync(ulong guildId, ulong userId, string? auditLogReason = null, CancellationToken ct = default);
    }
}