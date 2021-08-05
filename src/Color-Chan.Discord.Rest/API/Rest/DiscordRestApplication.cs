using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.Params.Application;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Application;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.API.Rest
{
    /// <inheritdoc cref="IDiscordRestApplication" />
    public class DiscordRestApplication : DiscordRestBase, IDiscordRestApplication
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRestApplication" />.
        /// </summary>
        /// <inheritdoc />
        public DiscordRestApplication(IDiscordHttpClient httpClient) : base(httpClient)
        {
        }

        #region Application commands

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordApplicationCommand>>> GetGlobalApplicationCommandsAsync(ulong applicationId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordApplicationCommandData>>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> CreateGlobalApplicationCommandAsync(ulong applicationId,
                                                                                                          DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands";
            var result = await HttpClient.PostAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> GetGlobalApplicationCommandAsync(ulong applicationId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands/{commandId.ToString()}";
            var result = await HttpClient.GetAsync<DiscordApplicationCommandData>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> EditGlobalApplicationCommandAsync(ulong applicationId, ulong commandId,
                                                                                                        DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands/{commandId.ToString()}";
            var result = await HttpClient.PatchAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result> DeleteGlobalApplicationCommandAsync(ulong applicationId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands/{commandId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, ct: ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordApplicationCommand>>> GetGuildApplicationCommandsAsync(ulong applicationId, ulong guildId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordApplicationCommandData>>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordApplicationCommand>>> BulkOverwriteGlobalApplicationCommandsAsync(ulong applicationId,
                                                                                                                                 IEnumerable<DiscordCreateApplicationCommand>
                                                                                                                                     commandParams, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands";
            var result = await HttpClient.PutAsync<IReadOnlyList<DiscordApplicationCommandData>, IEnumerable<DiscordCreateApplicationCommand>>(endpoint, commandParams, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> CreateGuildApplicationCommandAsync(ulong applicationId, ulong guildId,
                                                                                                         DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands";
            var result = await HttpClient.PostAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> GetGuildApplicationCommandAsync(ulong applicationId, ulong guildId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}";
            var result = await HttpClient.GetAsync<DiscordApplicationCommandData>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> EditGuildApplicationCommandAsync(ulong applicationId, ulong guildId, ulong commandId,
                                                                                                       DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}";
            var result = await HttpClient.PatchAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result> DeleteGuildApplicationCommandAsync(ulong applicationId, ulong guildId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, ct: ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordApplicationCommand>>> BulkOverwriteGuildApplicationCommandsAsync(ulong applicationId, ulong guildId,
                                                                                                                                IEnumerable<DiscordCreateApplicationCommand>
                                                                                                                                    commandParams, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands";
            var result = await HttpClient.PutAsync<IReadOnlyList<DiscordApplicationCommandData>, IEnumerable<DiscordCreateApplicationCommand>>(endpoint, commandParams, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        #endregion

        #region Application command permissions

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>> GetGuildApplicationCommandPermissions(
            ulong applicationId, ulong guildId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/permissions";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordGuildApplicationCommandPermissionsData>>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>> GetGuildApplicationCommandPermissions(
            ulong applicationId, ulong guildId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}/permissions";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordGuildApplicationCommandPermissionsData>>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildApplicationCommandPermissions>> EditGuildApplicationCommandPermissions(
            ulong applicationId, ulong guildId, ulong commandId, DiscordEditApplicationCommandPermissions body, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}/permissions";
            var result = await HttpClient.PutAsync<DiscordGuildApplicationCommandPermissionsData, DiscordEditApplicationCommandPermissions>(endpoint, body, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>> BatchEditApplicationCommandPermissions(
            ulong applicationId, ulong guildId, IEnumerable<DiscordBatchEditApplicationCommandPermissions> body, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/permissions";
            var result = await HttpClient.PutAsync<IReadOnlyList<DiscordGuildApplicationCommandPermissionsData>, IEnumerable<DiscordBatchEditApplicationCommandPermissions>>(endpoint, body, ct: ct)
                                         .ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result> CreateInteractionResponse(ulong interactionId, string token, DiscordInteractionResponseData response, CancellationToken ct = default)
        {
            string endpoint = $"/interactions/{interactionId.ToString()}/{token}/callback";
            return await HttpClient.PostAsync(endpoint, response, ct: ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordInteractionResponse>> GetOriginalInteractionResponse(ulong interactionId, string token, CancellationToken ct = default)
        {
            string endpoint = $"/interactions/{interactionId.ToString()}/{token}/callback";
            var result = await HttpClient.GetAsync<DiscordInteractionResponseData>(endpoint, ct: ct).ConfigureAwait(false);
            return ApiResultConverters.ConvertResult(result);
        }

        #endregion
    }
}