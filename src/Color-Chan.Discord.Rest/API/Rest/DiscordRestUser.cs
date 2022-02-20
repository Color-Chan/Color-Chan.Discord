using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Params.User;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.API.Rest
{
    public class DiscordRestUser : DiscordRestBase, IDiscordRestUser
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRestGuild" />.
        /// </summary>
        /// <inheritdoc />
        public DiscordRestUser(IDiscordHttpClient httpClient) : base(httpClient)
        {
        }

        // All api calls for users.

        #region User

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordUser>> GetCurrentUser(CancellationToken ct = default)
        {
            const string endpoint = "users/@me";
            var result = await HttpClient.GetAsync<DiscordUserData>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordUser>> GetUser(ulong userId, CancellationToken ct = default)
        {
            string endpoint = $"users/{userId.ToString()}";
            var result = await HttpClient.GetAsync<DiscordUserData>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordUser>> ModifyCurrentUser(DiscordModifyCurrentUser modifyCurrentUser, CancellationToken ct = default)
        {
            const string endpoint = "users/@me";
            var result = await HttpClient.PatchAsync<DiscordUserData, DiscordModifyCurrentUser>(endpoint, modifyCurrentUser, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }
        #endregion
        
        // All api calls for guild API calls.

        #region Guilds

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordPartialGuild>>> GetCurrentUserGuilds(CancellationToken ct = default)
        {
            const string endpoint = "users/@me/guilds";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordPartialGuildData>>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }
        
        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildMember>> GetCurrentUserGuildMember(ulong guildId, CancellationToken ct = default)
        {
            string endpoint = $"users/@me/guilds/{guildId}";
            var result = await HttpClient.GetAsync<DiscordGuildMemberData>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }
        
        /// <inheritdoc />
        public virtual async Task<Result> LeaveGuild(ulong guildId, CancellationToken ct = default)
        {
            var endpoint = $"users/@me/guilds/{guildId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, ct: ct).ConfigureAwait(false);
        }
        

        #endregion
        
        // All api calls for DMs.

        #region Dm

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordChannel>> CreateDm(DiscordCreateDm createDm, CancellationToken ct = default)
        {
            const string endpoint = "users/@me/channels";
            var result = await HttpClient.PostAsync<DiscordChannelData, DiscordCreateDm>(endpoint, createDm, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        #endregion
    }
}