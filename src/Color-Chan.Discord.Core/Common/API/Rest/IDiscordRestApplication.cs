using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.Params;
using Color_Chan.Discord.Core.Common.Models.Application;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Core.Common.API.Rest
{
    /// <summary>
    ///     Contains all the API calls mentioned in the Application object documentation.
    ///     https://discord.com/developers/docs/resources/application
    /// </summary>
    public interface IDiscordRestApplication
    {
        /// <summary>
        ///     Fetch all of the global application commands for your application.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of <see cref="IDiscordApplicationCommand" />
        ///     with the request results.
        /// </returns>
        Task<Result<IReadOnlyList<IDiscordApplicationCommand>>> GetGlobalApplicationCommandsAsync(ulong applicationId, CancellationToken ct = default);

        /// <summary>
        ///     Create a new global application command.
        ///     New global application commands will be available in all guilds after 1 hour.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="command">
        ///     The <see cref="DiscordCreateApplicationCommand" /> containing the application
        ///     command details.
        /// </param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <remarks>
        ///     Creating a command with the same name as an existing command for your application will overwrite the old command.
        /// </remarks>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordApplicationCommand" /> with the request results.
        /// </returns>
        Task<Result<IDiscordApplicationCommand>> CreateGlobalApplicationCommandAsync(ulong applicationId,
                                                                                     DiscordCreateApplicationCommand command, CancellationToken ct = default
        );

        /// <summary>
        ///     Fetch a global application command for your application..
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="commandId">The id of the application command.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordApplicationCommand" /> with the request results.
        /// </returns>
        Task<Result<IDiscordApplicationCommand>> GetGlobalApplicationCommandAsync(ulong applicationId, ulong commandId, CancellationToken ct = default);

        /// <summary>
        ///     Edit a global application command. Updates will be available in all guilds after 1 hour.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="commandId">The id of the application command.</param>
        /// <param name="command">
        ///     The <see cref="DiscordCreateApplicationCommand" /> containing the new
        ///     application command details.
        /// </param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordApplicationCommand" /> with the request results.
        /// </returns>
        Task<Result<IDiscordApplicationCommand>> EditGlobalApplicationCommandAsync(ulong applicationId, ulong commandId,
                                                                                   DiscordCreateApplicationCommand command, CancellationToken ct = default);

        /// <summary>
        ///     Deletes a global application command.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="commandId">The id of the application command.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result" /> with the request results.
        /// </returns>
        Task<Result> DeleteGlobalApplicationCommandAsync(ulong applicationId, ulong commandId, CancellationToken ct = default);

        /// <summary>
        ///     Fetch all of the guild application commands for the application for a specific guild..
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="guildId">The guild id.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of <see cref="IDiscordApplicationCommand" />
        ///     with the request results.
        /// </returns>
        Task<Result<IReadOnlyList<IDiscordApplicationCommand>>> GetGuildApplicationCommandsAsync(ulong applicationId, ulong guildId, CancellationToken ct = default);

        /// <summary>
        ///     Overwrites existing application commands that are registered globally for the application.
        ///     Updates will be available in all guilds after 1 hour.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="commandParams">
        ///     A <see cref="IEnumerable{T}" /> of
        ///     <see cref="DiscordCreateApplicationCommand" /> containing the new application command details.
        /// </param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of <see cref="IDiscordApplicationCommand" />
        ///     with the request results.
        /// </returns>
        Task<Result<IReadOnlyList<IDiscordApplicationCommand>>> BulkOverwriteGlobalApplicationCommandsAsync(ulong applicationId,
                                                                                                            IEnumerable<DiscordCreateApplicationCommand> commandParams,
                                                                                                            CancellationToken ct = default);

        /// <summary>
        ///     Create a new guild application command.
        ///     New guild application commands will be available in the guild immediately.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="guildId">The guild id.</param>
        /// <param name="command">
        ///     The <see cref="DiscordCreateApplicationCommand" /> containing the application
        ///     command details.
        /// </param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordApplicationCommand" /> with the request results.
        /// </returns>
        Task<Result<IDiscordApplicationCommand>> CreateGuildApplicationCommandAsync(ulong applicationId, ulong guildId,
                                                                                    DiscordCreateApplicationCommand command, CancellationToken ct = default
        );

        /// <summary>
        ///     Fetch a guild application command for your application.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="guildId">The guild id.</param>
        /// <param name="commandId">The id of the application command.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordApplicationCommand" /> with the request results.
        /// </returns>
        Task<Result<IDiscordApplicationCommand>> GetGuildApplicationCommandAsync(ulong applicationId, ulong guildId, ulong commandId, CancellationToken ct = default);

        /// <summary>
        ///     Edit a guild application command.
        ///     Updates for guild commands will be available immediately.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="guildId">The guild id.</param>
        /// <param name="commandId">The id of the application command.</param>
        /// <param name="command">
        ///     The <see cref="DiscordCreateApplicationCommand" /> containing the new
        ///     application command details.
        /// </param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordApplicationCommand" /> with the request results.
        /// </returns>
        Task<Result<IDiscordApplicationCommand>> EditGuildApplicationCommandAsync(ulong applicationId, ulong guildId, ulong commandId,
                                                                                  DiscordCreateApplicationCommand command, CancellationToken ct = default);

        /// <summary>
        ///     Delete a guild application command.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="guildId">The guild id.</param>
        /// <param name="commandId">The id of the application command.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result" /> with the request results.
        /// </returns>
        Task<Result> DeleteGuildApplicationCommandAsync(ulong applicationId, ulong guildId, ulong commandId, CancellationToken ct = default);

        /// <summary>
        ///     Overwrites existing commands for a guild.
        ///     Updates for guild commands will be available immediately.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="guildId">The guild id.</param>
        /// <param name="commandParams">
        ///     A <see cref="IEnumerable{T}" /> of
        ///     <see cref="DiscordCreateApplicationCommand" /> containing the new guild application command details.
        /// </param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of <see cref="IDiscordApplicationCommand" />
        ///     with the request results.
        /// </returns>
        Task<Result<IReadOnlyList<IDiscordApplicationCommand>>> BulkOverwriteGuildApplicationCommandsAsync(ulong applicationId, ulong guildId,
                                                                                                           IEnumerable<DiscordCreateApplicationCommand> commandParams,
                                                                                                           CancellationToken ct = default);
    }
}