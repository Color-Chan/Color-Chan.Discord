using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Params.Guild;
using Color_Chan.Discord.Core.Common.API.Rest.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models.Guild;

namespace Color_Chan.Discord.Rest.API.Rest.Guild
{
    public class DiscordRestGuildMember : DiscordRestBase, IDiscordRestGuildMember
    {
        private const string LimitQueryName = "limit";
        private const string AfterQueryName = "after";
        private const string SearchQueryName = "query";

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRestGuildMember" />.
        /// </summary>
        /// <inheritdoc />
        public DiscordRestGuildMember(IDiscordHttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildMember>> GetGuildMemberAsync(ulong guildId, ulong userId, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/members/{userId.ToString()}";
            var result = await HttpClient.GetAsync<DiscordGuildMemberData>(endpoint, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildMember>>> ListGuildMembersAsync(ulong guildId, int limit = 1, ulong afterId = 0, CancellationToken ct = default)
        {
            var queries = new List<KeyValuePair<string, string>>
            {
                new(LimitQueryName, limit.ToString()),
                new(AfterQueryName, afterId.ToString())
            };

            string endpoint = $"guilds/{guildId.ToString()}/members";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordGuildMemberData>>(endpoint, queries, ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildMember>>> SearchGuildMembersAsync(ulong guildId, string query, int limit = 1, CancellationToken ct = default)
        {
            var queries = new List<KeyValuePair<string, string>>
            {
                new(LimitQueryName, limit.ToString()),
                new(SearchQueryName, query)
            };

            string endpoint = $"guilds/{guildId.ToString()}/members/search";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordGuildMemberData>>(endpoint, queries, ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildMember?>> AddGuildMemberAsync(ulong guildId, ulong userId, DiscordAddGuildMember addGuildMember, CancellationToken ct = default)
        {
            // Todo: can return null!
            string endpoint = $"guilds/{guildId.ToString()}/members/{userId.ToString()}";
            var result = await HttpClient.PutAsync<DiscordGuildMemberData, DiscordAddGuildMember>(endpoint, addGuildMember, ct: ct).ConfigureAwait(false);

            if (result.IsSuccessful && result.Entity is null) return Result<IDiscordGuildMember?>.FromSuccess(null);

            return ConvertResult(result!)!;
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildMember>> ModifyGuildMemberAsync(ulong guildId, ulong userId, DiscordModifyGuildMember modifyGuildMember,
                                                                                      string? auditLogReason = null, CancellationToken ct = default)
        {
            string endpoint = $"guilds/{guildId.ToString()}/members/{userId.ToString()}";
            var result = await HttpClient.PatchAsync<DiscordGuildMemberData, DiscordModifyGuildMember>(endpoint, modifyGuildMember, auditLogReason, ct).ConfigureAwait(false);
            return ConvertResult(result);
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

        private Result<IDiscordGuildMember> ConvertResult(Result<DiscordGuildMemberData> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordGuildMember>.FromError(null, result.ErrorResult);

            return Result<IDiscordGuildMember>.FromSuccess(new DiscordGuildMember(result.Entity));
        }

        private Result<IReadOnlyList<IDiscordGuildMember>> ConvertResult(Result<IReadOnlyList<DiscordGuildMemberData>> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordGuildMember>>.FromError(null, result.ErrorResult);

            var list = new List<IDiscordGuildMember>();
            foreach (var data in result.Entity) list.Add(new DiscordGuildMember(data));

            return Result<IReadOnlyList<IDiscordGuildMember>>.FromSuccess(list);
        }
    }
}