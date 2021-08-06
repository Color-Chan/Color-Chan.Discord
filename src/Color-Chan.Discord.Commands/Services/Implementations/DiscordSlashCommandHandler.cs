using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Commands;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations
{
    /// <inheritdoc />
    public class DiscordSlashCommandHandler : IDiscordSlashCommandHandler
    {
        private readonly List<Func<IErrorResult, Task<Result<IDiscordInteractionResponse>>>> _errorHandlerMiddlewares = new();
        private readonly ILogger<DiscordSlashCommandHandler> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISlashCommandService _slashCommandService;

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordSlashCommandHandler" />.
        /// </summary>
        /// <param name="slashCommandService">The <see cref="ISlashCommandService" /> that will execute the slash command.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="logger">The logger.</param>
        public DiscordSlashCommandHandler(ISlashCommandService slashCommandService, IServiceProvider serviceProvider, ILogger<DiscordSlashCommandHandler> logger)
        {
            _slashCommandService = slashCommandService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <inheritdoc />
        public void RegisterErrorHandler(Func<IErrorResult, Task<Result<IDiscordInteractionResponse>>> errorHandler)
        {
            _errorHandlerMiddlewares.Add(errorHandler);
        }

        /// <inheritdoc />
        public async Task<IDiscordInteractionResponse> HandleSlashCommandAsync(IDiscordInteraction interaction)
        {
            if (interaction.Data is null)
            {
                throw new ArgumentNullException(nameof(interaction.Data), "Interaction data can not be null for a slash command!");
            }
            
            ISlashCommandContext context = new SlashCommandContext
            {
                User = interaction.User ?? interaction.GuildMember?.User ?? throw new NullReferenceException(nameof(context.User)),
                Message = interaction.Message,
                Member = interaction.GuildMember,
                GuildId = interaction.GuildId,
                ChannelId = interaction.ChannelId!.Value,
                ApplicationId = interaction.ApplicationId,
                CommandRequest = interaction.Data,
                InteractionId = interaction.Id,
                Token = interaction.Token
            };

            if (interaction.Data.Options is not null)
            {
                // Check if any of the used options was a sub command (group) and execute it.
                foreach (var option in interaction.Data.Options)
                {
                    switch (option.Type)
                    {
                        case DiscordApplicationCommandOptionType.SubCommand:
                            return await ExecuteSubCommandAsync(interaction.Data.Name, option, context).ConfigureAwait(false);
                        case DiscordApplicationCommandOptionType.SubCommandGroup:
                            return await ExecuteSubCommandGroupAsync(interaction.Data.Name, option, context).ConfigureAwait(false);
                    }
                }
            }

            // Execute normal slash command.
            var result = await _slashCommandService.ExecuteSlashCommandAsync(interaction.Data.Name, context, interaction.Data.Options?.ToList(), _serviceProvider).ConfigureAwait(false);

            // Return the response.
            if (result.IsSuccessful) return result.Entity!;
            return await HandleSlashCommandErrorResultAsync(result.ErrorResult!).ConfigureAwait(false);
        }

        /// <summary>
        ///     Handles an error that occured when a slash command executed.
        /// </summary>
        /// <param name="errorResult">The <see cref="IErrorResult" /> containing the error details.</param>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse" /> with the error response.
        /// </returns>
        private async Task<IDiscordInteractionResponse> HandleSlashCommandErrorResultAsync(IErrorResult errorResult)
        {
            // Try to handle the error that occured.
            foreach (var errorHandlerMiddleware in _errorHandlerMiddlewares)
            {
                var errorHandlerResult = await errorHandlerMiddleware.Invoke(errorResult).ConfigureAwait(false);
                if (errorHandlerResult.IsSuccessful)
                {
                    return errorHandlerResult.Entity!;
                }
            }

            _logger.LogWarning("An unhandled slash command error occured: {ErrorMessage}", errorResult.ErrorMessage);
            _logger.LogWarning("Sending default error message");

            //  Build the response embed.
            var errorEmbedBuilder = new DiscordEmbedBuilder()
                                    .WithTitle("Error")
                                    .WithDescription("Something went wrong!")
                                    .WithColor(Color.Red)
                                    .WithTimeStamp();

            // Build the response with the embed.
            var errorResponse = new SlashCommandResponseBuilder()
                                .WithEmbed(errorEmbedBuilder.Build())
                                .MakePrivate()
                                .Build();

            //  Return the response to Discord.
            return errorResponse;
        }

        /// <summary>
        ///     Executes a sub slash command.
        /// </summary>
        /// <param name="commandGroupName">The command group the sub command belongs to.</param>
        /// <param name="option">The options used with the command request.</param>
        /// <param name="context">The current context of the command request.</param>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse" /> with the the slash command response.
        /// </returns>
        private async Task<IDiscordInteractionResponse> ExecuteSubCommandAsync(string commandGroupName, IDiscordInteractionCommandOption option, ISlashCommandContext context)
        {
            var result = await _slashCommandService.ExecuteSlashCommandAsync(commandGroupName, option.Name, context, option.SubOptions?.ToList(), _serviceProvider).ConfigureAwait(false);

            // Return the response.
            if (result.IsSuccessful) return result.Entity!;
            return await HandleSlashCommandErrorResultAsync(result.ErrorResult!).ConfigureAwait(false);
        }

        /// <summary>
        ///     Executes a sub slash command in a sub command group.
        /// </summary>
        /// <param name="commandGroupName">The command group the sub command group belongs to.</param>
        /// <param name="option">The options used with the command request.</param>
        /// <param name="context">The current context of the command request.</param>
        /// <returns>
        ///     A <see cref="IDiscordInteractionResponse" /> with the the slash command response.
        /// </returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidSlashCommandGroupException"></exception>
        private async Task<IDiscordInteractionResponse> ExecuteSubCommandGroupAsync(string commandGroupName, IDiscordInteractionCommandOption option, ISlashCommandContext context)
        {
            if (option.SubOptions is null)
            {
                throw new NullReferenceException("A sub command group needs to sub commands");
            }

            // Try to find the sub command and execute it.
            foreach (var subOption in option.SubOptions)
            {
                if (subOption.Type == DiscordApplicationCommandOptionType.SubCommand)
                {
                    var result = await _slashCommandService.ExecuteSlashCommandAsync(commandGroupName, option.Name, subOption.Name, context, subOption.SubOptions?.ToList(), _serviceProvider)
                                                           .ConfigureAwait(false);
                    if (result.IsSuccessful) return result.Entity!;

                    // Return the response.
                    if (result.IsSuccessful) return result.Entity!;
                    return await HandleSlashCommandErrorResultAsync(result.ErrorResult!).ConfigureAwait(false);
                }
            }

            // The command group had no sub command.
            var exception = new InvalidSlashCommandGroupException($"Command group {commandGroupName} had no sub command");
            _logger.LogError(exception, "Command group {Name} had no sub command", commandGroupName);
            throw exception;
        }
    }
}