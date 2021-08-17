using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.Params.Application;
using Color_Chan.Discord.Core.Common.API.Params.Webhook;
using Color_Chan.Discord.Core.Common.Models.Application;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Message;
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
        Task<Result<IDiscordApplicationCommand>> CreateGlobalApplicationCommandAsync(ulong applicationId, DiscordCreateApplicationCommand command, CancellationToken ct = default);

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

        /// <summary>
        ///     Fetches command permissions for all commands for your application in a guild.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="guildId">The id of the guild.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of
        ///     <see cref="IDiscordGuildApplicationCommandPermissions" /> with the request results.
        /// </returns>
        Task<Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>> GetGuildApplicationCommandPermissionsAsync(
            ulong applicationId, ulong guildId, CancellationToken ct = default);

        /// <summary>
        ///     Fetches command permissions for a command for your application in a guild.
        /// </summary>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="guildId">The id of the guild.</param>
        /// <param name="commandId">The id of the command.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of
        ///     <see cref="IDiscordGuildApplicationCommandPermissions" /> with the request results.
        /// </returns>
        Task<Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>> GetGuildApplicationCommandPermissionsAsync(
            ulong applicationId, ulong guildId, ulong commandId, CancellationToken ct = default);

        /// <summary>
        ///     Edits command permissions for a specific command for your application in a guild.
        /// </summary>
        /// <remarks>
        ///     You can only add up to 10 permission overwrites for a command.
        /// </remarks>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="guildId">The id of the guild.</param>
        /// <param name="commandId">The id of the command.</param>
        /// <param name="body">The <see cref="EditGuildApplicationCommandPermissionsAsync" /> containing the new permission data.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordGuildApplicationCommandPermissions" /> with the request results.
        /// </returns>
        Task<Result<IDiscordGuildApplicationCommandPermissions>> EditGuildApplicationCommandPermissionsAsync(
            ulong applicationId, ulong guildId, ulong commandId, DiscordEditApplicationCommandPermissions body, CancellationToken ct = default);

        /// <summary>
        ///     Batch edits permissions for all commands in a guild.
        /// </summary>
        /// <remarks>
        ///     You can only add up to 10 permission overwrites for a command.
        /// </remarks>
        /// <param name="applicationId">The id of the application.</param>
        /// <param name="guildId">The id of the guild.</param>
        /// <param name="body">
        ///     The <see cref="DiscordBatchEditApplicationCommandPermissions" />s containing the new permission data
        ///     for all the commands.
        /// </param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of
        ///     <see cref="IDiscordGuildApplicationCommandPermissions" /> with the request results.
        /// </returns>
        Task<Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>> BatchEditApplicationCommandPermissionsAsync(
            ulong applicationId, ulong guildId, IEnumerable<DiscordBatchEditApplicationCommandPermissions> body, CancellationToken ct = default);

        /// <summary>
        ///     Create a response to an Interaction from the gateway.
        /// </summary>
        /// <param name="interactionId">The interaction id.</param>
        /// <param name="token">The token of the interaction.</param>
        /// <param name="response">The new response.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result" /> with the request results.
        /// </returns>
        Task<Result> CreateInteractionResponseAsync(ulong interactionId, string token, DiscordInteractionResponseData response, CancellationToken ct = default);

        /// <summary>
        ///     Returns the initial Interaction response.
        /// </summary>
        /// <param name="applicationId">The ID of the application.</param>
        /// <param name="token">The token of the interaction.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordMessage" /> with the request results.
        /// </returns>
        Task<Result<IDiscordMessage>> GetOriginalInteractionResponseAsync(ulong applicationId, string token, CancellationToken ct = default);

        /// <summary>
        ///     Edits the initial Interaction response.
        /// </summary>
        /// <param name="applicationId">The ID of the application.</param>
        /// <param name="token">The token of the interaction.</param>
        /// <param name="webhookMessage">The new message.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordMessage" /> with the request results.
        /// </returns>
        Task<Result<IDiscordMessage>> EditOriginalInteractionResponseAsync(ulong applicationId, string token, DiscordEditWebhookMessage webhookMessage, CancellationToken ct = default);

        /// <summary>
        ///     Deletes the initial Interaction response.
        /// </summary>
        /// <param name="applicationId">The ID of the application.</param>
        /// <param name="token">The token of the interaction.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result" /> with the request results.
        /// </returns>
        Task<Result> DeleteOriginalInteractionResponseAsync(ulong applicationId, string token, CancellationToken ct = default);

        /// <summary>
        ///     Create a followup message for an Interaction.
        /// </summary>
        /// <param name="applicationId">The ID of the application.</param>
        /// <param name="token">The token of the interaction.</param>
        /// <param name="followupMessage">The follow up message of the interaction.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordMessage" /> with the request results.
        /// </returns>
        Task<Result<IDiscordMessage>> CreateFollowupMessageAsync(ulong applicationId, string token, DiscordCreateFollowupMessage followupMessage, CancellationToken ct = default);

        /// <summary>
        ///     Returns a followup message for an Interaction.
        /// </summary>
        /// <remarks>
        ///     Does not support ephemeral followups.
        /// </remarks>
        /// <param name="applicationId">The ID of the application.</param>
        /// <param name="token">The token of the interaction.</param>
        /// <param name="messageId">The ID of the followup message.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordMessage" /> with the request results.
        /// </returns>
        Task<Result<IDiscordMessage>> GetFollowupMessageAsync(ulong applicationId, string token, ulong messageId, CancellationToken ct = default);

        /// <summary>
        ///     Edits a followup message for an Interaction.
        /// </summary>
        /// <remarks>
        ///     Does not support ephemeral followups.
        /// </remarks>
        /// <param name="applicationId">The ID of the application.</param>
        /// <param name="token">The token of the interaction.</param>
        /// <param name="messageId">The ID of the followup message.</param>
        /// <param name="webhookMessage">The edited followup message.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result{T}" /> of <see cref="IDiscordMessage" /> with the request results.
        /// </returns>
        Task<Result<IDiscordMessage>> EditFollowupMessageAsync(ulong applicationId, string token, ulong messageId, DiscordEditWebhookMessage webhookMessage, CancellationToken ct = default);

        /// <summary>
        ///     Deletes a followup message for an Interaction.
        /// </summary>
        /// <remarks>
        ///     Does not support ephemeral followups.
        /// </remarks>
        /// <param name="applicationId">The ID of the application.</param>
        /// <param name="token">The token of the interaction.</param>
        /// <param name="messageId">The ID of the followup message.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result" /> with the request results.
        /// </returns>
        Task<Result> DeleteFollowupMessageAsync(ulong applicationId, string token, ulong messageId, CancellationToken ct = default);
    }
}