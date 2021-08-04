using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Params.Application;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Application;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models.Application;
using Color_Chan.Discord.Rest.Models.Guild;

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

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordApplicationCommand>>> GetGlobalApplicationCommandsAsync(ulong applicationId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordApplicationCommandData>>(endpoint, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> CreateGlobalApplicationCommandAsync(ulong applicationId,
                                                                                                          DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands";
            var result = await HttpClient.PostAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> GetGlobalApplicationCommandAsync(ulong applicationId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands/{commandId.ToString()}";
            var result = await HttpClient.GetAsync<DiscordApplicationCommandData>(endpoint, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> EditGlobalApplicationCommandAsync(ulong applicationId, ulong commandId,
                                                                                                        DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands/{commandId.ToString()}";
            var result = await HttpClient.PatchAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
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
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordApplicationCommand>>> BulkOverwriteGlobalApplicationCommandsAsync(ulong applicationId,
                                                                                                                                 IEnumerable<DiscordCreateApplicationCommand>
                                                                                                                                     commandParams, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands";
            var result = await HttpClient.PutAsync<IReadOnlyList<DiscordApplicationCommandData>, IEnumerable<DiscordCreateApplicationCommand>>(endpoint, commandParams, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> CreateGuildApplicationCommandAsync(ulong applicationId, ulong guildId,
                                                                                                         DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands";
            var result = await HttpClient.PostAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> GetGuildApplicationCommandAsync(ulong applicationId, ulong guildId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}";
            var result = await HttpClient.GetAsync<DiscordApplicationCommandData>(endpoint, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordApplicationCommand>> EditGuildApplicationCommandAsync(ulong applicationId, ulong guildId, ulong commandId,
                                                                                                       DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}";
            var result = await HttpClient.PatchAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
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
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>> GetGuildApplicationCommandPermissions(
            ulong applicationId, ulong guildId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/permissions";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordGuildApplicationCommandPermissionsData>>(endpoint, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>> GetApplicationCommandPermissions(
            ulong applicationId, ulong guildId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}/permissions";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordGuildApplicationCommandPermissionsData>>(endpoint, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordGuildApplicationCommandPermissions>> EditApplicationCommandPermissions(
            ulong applicationId, ulong guildId, ulong commandId, DiscordEditApplicationCommandPermissions body, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}/permissions";
            var result = await HttpClient.PutAsync<DiscordGuildApplicationCommandPermissionsData, DiscordEditApplicationCommandPermissions>(endpoint, body, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <summary>
        ///     Batch edits permissions for all commands in a guild.
        /// </summary>
        /// <remarks>
        ///     You can only add up to 10 permission overwrites for a command.
        /// </remarks>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="guildId">The id of the guild.</param>
        /// <param name="body">The <see cref="DiscordBatchEditApplicationCommandPermissions" />s containing the new permission data for all the commands.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of
        ///     <see cref="IDiscordGuildApplicationCommandPermissions" /> with the request results.
        /// </returns>
        public virtual async Task<Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>> BatchEditApplicationCommandPermissions(
            ulong applicationId, ulong guildId, IEnumerable<DiscordBatchEditApplicationCommandPermissions> body, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/permissions";
            var result = await HttpClient.PutAsync<IReadOnlyList<DiscordGuildApplicationCommandPermissionsData>, IEnumerable<DiscordBatchEditApplicationCommandPermissions>>(endpoint, body, ct: ct)
                                         .ConfigureAwait(false);
            return ConvertResult(result);
        }

        private Result<IDiscordApplicationCommand> ConvertResult(Result<DiscordApplicationCommandData> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordApplicationCommand>.FromError(null, result.ErrorResult);

            return Result<IDiscordApplicationCommand>.FromSuccess(new DiscordApplicationCommand(result.Entity));
        }

        private Result<IReadOnlyList<IDiscordApplicationCommand>> ConvertResult(Result<IReadOnlyList<DiscordApplicationCommandData>> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordApplicationCommand>>.FromError(null, result.ErrorResult);

            var roles = new List<IDiscordApplicationCommand>();
            foreach (var roleData in result.Entity) roles.Add(new DiscordApplicationCommand(roleData));

            return Result<IReadOnlyList<IDiscordApplicationCommand>>.FromSuccess(roles);
        }

        private Result<IDiscordGuildApplicationCommandPermissions> ConvertResult(Result<DiscordGuildApplicationCommandPermissionsData> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordGuildApplicationCommandPermissions>.FromError(null, result.ErrorResult);

            return Result<IDiscordGuildApplicationCommandPermissions>.FromSuccess(new DiscordGuildApplicationCommandPermissions(result.Entity));
        }

        private Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>> ConvertResult(Result<IReadOnlyList<DiscordGuildApplicationCommandPermissionsData>> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>.FromError(null, result.ErrorResult);

            var roles = new List<IDiscordGuildApplicationCommandPermissions>();
            foreach (var roleData in result.Entity) roles.Add(new DiscordGuildApplicationCommandPermissions(roleData));

            return Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>.FromSuccess(roles);
        }
    }
}