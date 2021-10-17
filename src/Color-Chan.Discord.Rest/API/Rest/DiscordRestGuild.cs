using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Params.Guild;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.API.Rest
{
    /// <inheritdoc cref="IDiscordRestGuild" />
    public class DiscordRestGuild : DiscordRestBase, IDiscordRestGuild
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRestGuild" />.
        /// </summary>
        /// <inheritdoc />
        public DiscordRestGuild(IDiscordHttpClient httpClient) : base(httpClient)
        {
        }

        // All api calls for guilds.

        #region Guild

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuild>> CreateGuildAsync(DiscordCreateGuild createGuild, CancellationToken ct = default)
        {
            const string endpoint = "guilds";
            var result = await HttpClient.PostAsync<DiscordGuildData, DiscordCreateGuild>(endpoint, createGuild, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuild>> GetGuildAsync(ulong guildId, bool withCounts = false, CancellationToken ct = default)
        {
            var queries = new List<KeyValuePair<string, string>>
            {
                new(Constants.Headers.WithCountsQueryName, withCounts.ToString())
            };

            var endpoint = $"guilds/{guildId.ToString()}";
            var result = await HttpClient.GetAsync<DiscordGuildData>(endpoint, queries, ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildPreview>> GetGuildPreviewAsync(ulong guildId, CancellationToken ct = default)
        {
            var endpoint = $"guilds/{guildId.ToString()}";
            var result = await HttpClient.GetAsync<DiscordGuildPreviewData>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuild>> ModifyGuildAsync(ulong guildId, DiscordModifyGuild modifyGuild, string? auditLogReason, CancellationToken ct = default)
        {
            var endpoint = $"guilds/{guildId.ToString()}";
            var result = await HttpClient.PatchAsync<DiscordGuildData, DiscordModifyGuild>(endpoint, modifyGuild, auditLogReason, ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result> DeleteGuildAsync(ulong guildId, CancellationToken ct = default)
        {
            var endpoint = $"guilds/{guildId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, ct: ct).ConfigureAwait(false);
        }

        #endregion

        // All api calls for guild channels.

        #region Guild channels

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordChannel>>> GetGuildChannelsAsync(ulong guildId, CancellationToken ct = default)
        {
            var endpoint = $"guilds/{guildId.ToString()}/channels";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordChannelData>>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        // Todo: Create guild channel: https://discord.com/developers/docs/resources/guild#create-guild-channel

        // Todo: Modify Guild Channel Positions: https://discord.com/developers/docs/resources/guild#modify-guild-channel-positions

        #endregion

        // All api calls for guild roles.

        #region Guild role

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildRole>>> GetGuildRolesAsync(ulong guildId, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordGuildRoleData>>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildRole>> CreateGuildRoleAsync(ulong guildId, DiscordCreateGuildRole role, string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles";
            var result = await HttpClient.PostAsync<DiscordGuildRoleData, DiscordCreateGuildRole>(endpoint, role, auditLogReason, ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildRole>>> ModifyGuildRolePositionsAsync(ulong guildId, IEnumerable<DiscordModifyGuildRolePositions> modifyGuildRoles,
                                                                                                          string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles";
            var result = await HttpClient.PatchAsync<IReadOnlyList<DiscordGuildRoleData>, IEnumerable<DiscordModifyGuildRolePositions>>(endpoint, modifyGuildRoles, auditLogReason, ct)
                                         .ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildRole>> ModifyGuildRoleAsync(ulong guildId, ulong roleId, DiscordModifyGuildRole role, string? auditLogReason = null,
                                                                                  CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles/{roleId.ToString()}";
            var result = await HttpClient.PatchAsync<DiscordGuildRoleData, DiscordModifyGuildRole>(endpoint, role, auditLogReason, ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result> DeleteGuildRoleAsync(ulong guildId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/roles/{roleId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, null, auditLogReason, ct).ConfigureAwait(false);
        }

        #endregion

        // All api calls for guild members.

        #region Guild member

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildMember>> GetGuildMemberAsync(ulong guildId, ulong userId, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/members/{userId.ToString()}";
            var result = await HttpClient.GetAsync<DiscordGuildMemberData>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildMember>>> ListGuildMembersAsync(ulong guildId, int limit = 1, ulong afterId = 0, CancellationToken ct = default)
        {
            var queries = new List<KeyValuePair<string, string>>
            {
                new(Constants.Headers.LimitQueryName, limit.ToString()),
                new(Constants.Headers.AfterQueryName, afterId.ToString())
            };

            string endpoint = $"guilds/{guildId.ToString()}/members";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordGuildMemberData>>(endpoint, queries, ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildMember>>> SearchGuildMembersAsync(ulong guildId, string query, int limit = 1, CancellationToken ct = default)
        {
            var queries = new List<KeyValuePair<string, string>>
            {
                new(Constants.Headers.LimitQueryName, limit.ToString()),
                new(Constants.Headers.SearchQueryName, query)
            };

            string endpoint = $"guilds/{guildId.ToString()}/members/search";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordGuildMemberData>>(endpoint, queries, ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildMember?>> AddGuildMemberAsync(ulong guildId, ulong userId, DiscordAddGuildMember addGuildMember, CancellationToken ct = default)
        {
            // Todo: can return null!
            string endpoint = $"guilds/{guildId.ToString()}/members/{userId.ToString()}";
            var result = await HttpClient.PutAsync<DiscordGuildMemberData, DiscordAddGuildMember>(endpoint, addGuildMember, ct: ct).ConfigureAwait(false);

            if (result.IsSuccessful && result.Entity is null) return Result<IDiscordGuildMember?>.FromSuccess(null);

            return ApiResultConverters.ConvertResult(result)!;
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildMember>> ModifyGuildMemberAsync(ulong guildId, ulong userId, DiscordModifyGuildMember modifyGuildMember,
                                                                                      string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/members/{userId.ToString()}";
            var result = await HttpClient.PatchAsync<DiscordGuildMemberData, DiscordModifyGuildMember>(endpoint, modifyGuildMember, auditLogReason, ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result> ModifyCurrentUserNickAsync(ulong guildId, DiscordModifyCurrentUserNick modifyGuildMember,
                                                                     string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/members/@me/nick";
            return await HttpClient.PatchAsync(endpoint, modifyGuildMember, auditLogReason, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result> AddGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/members/{userId.ToString()}/roles/{roleId.ToString()}";
            return await HttpClient.PutAsync(endpoint, auditLogReason, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result> RemoveGuildMemberRoleAsync(ulong guildId, ulong userId, ulong roleId, string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/members/{userId.ToString()}/roles/{roleId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, null, auditLogReason, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result> RemoveGuildMemberAsync(ulong guildId, ulong userId, string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/members/{userId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, null, auditLogReason, ct).ConfigureAwait(false);
        }

        #endregion
    }
}