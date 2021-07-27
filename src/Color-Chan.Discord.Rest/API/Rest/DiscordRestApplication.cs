using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.Params;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.API.Rest
{
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
        public virtual async Task<Result<IReadOnlyList<DiscordApplicationCommandData>>> GetGlobalApplicationCommandsAsync(ulong applicationId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands";
            return await HttpClient.GetAsync<IReadOnlyList<DiscordApplicationCommandData>>(endpoint, ct: ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<DiscordApplicationCommandData>> CreateGlobalApplicationCommandAsync(ulong applicationId,
                                                                                                             DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands";
            return await HttpClient.PostAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<DiscordApplicationCommandData>> GetGlobalApplicationCommandAsync(ulong applicationId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands/{commandId.ToString()}";
            return await HttpClient.GetAsync<DiscordApplicationCommandData>(endpoint, ct: ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<DiscordApplicationCommandData>> EditGlobalApplicationCommandAsync(ulong applicationId, ulong commandId,
                                                                                                           DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands/{commandId.ToString()}";
            return await HttpClient.PatchAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result> DeleteGlobalApplicationCommandAsync(ulong applicationId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands/{commandId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, ct: ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<DiscordApplicationCommandData>>> GetGuildApplicationCommandsAsync(ulong applicationId, ulong guildId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands";
            return await HttpClient.GetAsync<IReadOnlyList<DiscordApplicationCommandData>>(endpoint, ct: ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<DiscordApplicationCommandData>>> BulkOverwriteGlobalApplicationCommandsAsync(ulong applicationId,
                                                                                                                                    IEnumerable<DiscordCreateApplicationCommand>
                                                                                                                                        commandParams, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/commands";
            return await HttpClient.PutAsync<IReadOnlyList<DiscordApplicationCommandData>, IEnumerable<DiscordCreateApplicationCommand>>(endpoint, commandParams, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<DiscordApplicationCommandData>> CreateGuildApplicationCommandAsync(ulong applicationId, ulong guildId,
                                                                                                            DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands";
            return await HttpClient.PostAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<DiscordApplicationCommandData>> GetGuildApplicationCommandAsync(ulong applicationId, ulong guildId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}";
            return await HttpClient.GetAsync<DiscordApplicationCommandData>(endpoint, ct: ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<DiscordApplicationCommandData>> EditGuildApplicationCommandAsync(ulong applicationId, ulong guildId, ulong commandId,
                                                                                                          DiscordCreateApplicationCommand command, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}";
            return await HttpClient.PatchAsync<DiscordApplicationCommandData, DiscordCreateApplicationCommand>(endpoint, command, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result> DeleteGuildApplicationCommandAsync(ulong applicationId, ulong guildId, ulong commandId, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands/{commandId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, ct: ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<DiscordApplicationCommandData>>> BulkOverwriteGuildApplicationCommandsAsync(ulong applicationId, ulong guildId,
                                                                                                                                   IEnumerable<DiscordCreateApplicationCommand>
                                                                                                                                       commandParams, CancellationToken ct = default)
        {
            string endpoint = $"applications/{applicationId.ToString()}/guilds/{guildId.ToString()}/commands";
            return await HttpClient.PutAsync<IReadOnlyList<DiscordApplicationCommandData>, IEnumerable<DiscordCreateApplicationCommand>>(endpoint, commandParams, ct).ConfigureAwait(false);
        }
    }
}