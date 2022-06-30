using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.Params.Guild;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Core.Common.API.Rest;

// Todo: Implement missing API methods
/// <summary>
///     Contains all the API calls mentioned in the guild object documentation.
///     https://discord.com/developers/docs/resources/guild
/// </summary>
public interface IDiscordRestGuild
{
    /// <summary>
    ///     Create a new guild.
    /// </summary>
    /// <param name="createGuild">The <see cref="DiscordCreateGuild" /> params that will be used to create the guild.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IDiscordGuild" /> with the request results.
    /// </returns>
    Task<Result<IDiscordGuild>> CreateGuildAsync(DiscordCreateGuild createGuild, CancellationToken ct = default);

    /// <summary>
    ///     Get a guild.
    /// </summary>
    /// <param name="guildId">The ID fo the guild.</param>
    /// <param name="withCounts">
    ///     Whether or not the <see cref="IDiscordGuild" /> will contain approximate member and presence
    ///     counts for the guild.
    /// </param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IDiscordGuild" /> with the request results.
    /// </returns>
    Task<Result<IDiscordGuild>> GetGuildAsync(ulong guildId, bool withCounts = false, CancellationToken ct = default);

    /// <summary>
    ///     Get a preview of a guild.
    /// </summary>
    /// <remarks>
    ///     If the user is not in the guild, then the guild must be lurkable (it must be Discoverable or have a live public
    ///     stage).
    /// </remarks>
    /// <param name="guildId">the guild id.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IDiscordGuildPreview" /> with the request results.
    /// </returns>
    Task<Result<IDiscordGuildPreview>> GetGuildPreviewAsync(ulong guildId, CancellationToken ct = default);

    /// <summary>
    ///     Modifies a guild.
    /// </summary>
    /// <param name="guildId">The guild id.</param>
    /// <param name="modifyGuild">
    ///     The <see cref="DiscordModifyGuild" /> containing the data that will be used to modify the
    ///     guild.
    /// </param>
    /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IDiscordGuild" /> with the request results.
    /// </returns>
    Task<Result<IDiscordGuild>> ModifyGuildAsync(ulong guildId, DiscordModifyGuild modifyGuild, string? auditLogReason, CancellationToken ct = default);

    /// <summary>
    ///     Deletes a guild.
    /// </summary>
    /// <param name="guildId">The guild id.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result" /> with the request results.
    /// </returns>
    Task<Result> DeleteGuildAsync(ulong guildId, CancellationToken ct = default);

    /// <summary>
    ///     Get the channels of a guild.
    /// </summary>
    /// <param name="guildId">The guild id.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IDiscordChannel" /> with the request results.
    /// </returns>
    Task<Result<IReadOnlyList<IDiscordChannel>>> GetGuildChannelsAsync(ulong guildId, CancellationToken ct = default);

    /// <summary>
    ///     Get a list of <see cref="IDiscordGuildRole" />s.
    /// </summary>
    /// <param name="guildId">The guild id.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of <see cref="IDiscordGuildRole" />
    ///     with the request results.
    /// </returns>
    Task<Result<IReadOnlyList<IDiscordGuildRole>>> GetGuildRolesAsync(ulong guildId, CancellationToken ct = default);

    /// <summary>
    ///     Creates a new discord guild role.
    /// </summary>
    /// <param name="guildId">The guild id.</param>
    /// <param name="role">The <see cref="DiscordCreateGuildRole" /> containing the new role data.</param>
    /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IDiscordGuildRole" /> with the request results.
    /// </returns>
    Task<Result<IDiscordGuildRole>> CreateGuildRoleAsync(ulong guildId, DiscordCreateGuildRole role, string? auditLogReason = null, CancellationToken ct = default);

    /// <summary>
    ///     Changes the positions of guild roles.
    /// </summary>
    /// <param name="guildId">The guild id.</param>
    /// <param name="modifyGuildRoles">The <see cref="DiscordModifyGuildRolePositions" /> containing the new positions.</param>
    /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of <see cref="IDiscordGuildRole" />
    ///     with the request results.
    /// </returns>
    Task<Result<IReadOnlyList<IDiscordGuildRole>>> ModifyGuildRolePositionsAsync(ulong guildId, IEnumerable<DiscordModifyGuildRolePositions> modifyGuildRoles, string? auditLogReason = null,
                                                                                 CancellationToken ct = default);

    /// <summary>
    ///     Modify a guild role.
    /// </summary>
    /// <param name="guildId">The guild id.</param>
    /// <param name="roleId">The role id.</param>
    /// <param name="role">The <see cref="DiscordModifyGuildRole" /> containing the new role data.</param>
    /// <param name="auditLogReason">The reason for this action. This will be shown in the audit logs.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IDiscordGuildRole" /> with the request results.
    /// </returns>
    Task<Result<IDiscordGuildRole>> ModifyGuildRoleAsync(ulong guildId, ulong roleId, DiscordModifyGuildRole role, string? auditLogReason = null, CancellationToken ct = default);

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
    Task<Result> DeleteGuildRoleAsync(ulong guildId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default);

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
    /// <param name="addGuildMember">The <see cref="DiscordAddGuildMember" /> containing the body.</param>
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
    /// <param name="modifyGuildMember">The <see cref="DiscordModifyGuildMember" /> data that will be used to modify the user.</param>
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
    /// <param name="modifyGuildMember">
    ///     The <see cref="DiscordModifyCurrentUserNick" /> data that will be used to modify the
    ///     user.
    /// </param>
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