using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Pipelines;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Commands.Services.Implementations
{
    /// <inheritdoc />
    public class DiscordSlashCommandHandler : IDiscordSlashCommandHandler
    {
        private readonly ILogger<DiscordSlashCommandHandler> _logger;
        private readonly IDiscordRestGuild _restGuild;
        private readonly IDiscordRestChannel _restChannel;
        private readonly SlashCommandConfiguration _slashCommandConfiguration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISlashCommandService _slashCommandService;

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordSlashCommandHandler" />.
        /// </summary>
        /// <param name="slashCommandService">The <see cref="ISlashCommandService" /> that will execute the slash command.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="restGuild">The rest class for Guilds.</param>
        /// <param name="restChannel">The rest class for Channels.</param>
        /// <param name="slashCommandConfiguration">The configuration class for slash commands.</param>
        public DiscordSlashCommandHandler(ISlashCommandService slashCommandService, IServiceProvider serviceProvider, ILogger<DiscordSlashCommandHandler> logger,
                                          IDiscordRestGuild restGuild, IDiscordRestChannel restChannel, IOptions<SlashCommandConfiguration> slashCommandConfiguration)
        {
            _slashCommandService = slashCommandService;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _restGuild = restGuild;
            _restChannel = restChannel;
            _slashCommandConfiguration = slashCommandConfiguration.Value;
        }

        /// <inheritdoc />
        public async Task<IDiscordInteractionResponse> HandleSlashCommandAsync(IDiscordInteraction interaction)
        {
            if (interaction.Data is null)
            {
                throw new ArgumentNullException(nameof(interaction.Data), "Interaction data can not be null for a slash command!");
            }

            IDiscordGuild? guild = null;
            if (_slashCommandConfiguration.EnableAutoGetGuild && interaction.GuildId is not null)
            {
                var guildResult = await _restGuild.GetGuildAsync(interaction.GuildId.Value, true).ConfigureAwait(false);
                guild = guildResult.Entity;
            }
            
            IDiscordChannel? channel = null;
            if (_slashCommandConfiguration.EnableAutoGetGuild && interaction.ChannelId is not null)
            {
                var channelResult = await _restChannel.GetChannelAsync(interaction.ChannelId.Value).ConfigureAwait(false);
                channel = channelResult.Entity;
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
                Token = interaction.Token,
                Channel = channel,
                Guild = guild
            };

            Task<Result<IDiscordInteractionResponse>>? commandTask = null;
            if (interaction.Data.Options is not null)
            {
                // Check if any of the used options was a sub command (group) and execute it.
                foreach (var option in interaction.Data.Options)
                {
                    commandTask = option.Type switch
                    {
                        DiscordApplicationCommandOptionType.SubCommand => ExecuteSubCommandAsync(interaction.Data.Name, option, context),
                        DiscordApplicationCommandOptionType.SubCommandGroup => ExecuteSubCommandGroupAsync(interaction.Data.Name, option, context),
                        _ => commandTask
                    };
                }
            }

            // Execute normal slash command.
            commandTask = _slashCommandService.ExecuteSlashCommandAsync(interaction.Data.Name, context, interaction.Data.Options?.ToList(), _serviceProvider);
            
            // Execute the the pipelines and commands.
            var pipelines = _serviceProvider.GetServices<ISlashDiscordPipeline>();
            Task<Result<IDiscordInteractionResponse>> Handler() => commandTask;
            var result = await pipelines.Aggregate((SlashCommandHandlerDelegate) Handler, (next, pipeline) => () => pipeline.HandleAsync(context, next))();
            
            // Return the response.
            if (result.IsSuccessful) return result.Entity!;

            if (_slashCommandConfiguration.SendDefaultErrorMessage)
            {
                return DefaultErrorMessage();
            }

            throw new SlashCommandResultException($"Command request {interaction.Id} returned unsuccessfully, {result.ErrorResult?.ErrorMessage}");
        }
        
        /// <summary>
        ///     Get a default error message response.
        /// </summary>
        private IDiscordInteractionResponse DefaultErrorMessage()
        {
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
        private async Task<Result<IDiscordInteractionResponse>> ExecuteSubCommandAsync(string commandGroupName, IDiscordInteractionCommandOption option, ISlashCommandContext context)
        {
            return await _slashCommandService.ExecuteSlashCommandAsync(commandGroupName, option.Name, context, option.SubOptions?.ToList(), _serviceProvider).ConfigureAwait(false);
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
        /// <exception cref="NullReferenceException">Thrown when the sub options are null.</exception>
        /// <exception cref="InvalidSlashCommandGroupException">Thrown when no sub command has been found.</exception>
        private async Task<Result<IDiscordInteractionResponse>> ExecuteSubCommandGroupAsync(string commandGroupName, IDiscordInteractionCommandOption option, ISlashCommandContext context)
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
                    return await _slashCommandService.ExecuteSlashCommandAsync(commandGroupName, option.Name, subOption.Name, context, subOption.SubOptions?.ToList(), _serviceProvider)
                                                           .ConfigureAwait(false);
                }
            }

            // The command group had no sub command.
            var exception = new InvalidSlashCommandGroupException($"Command group {commandGroupName} had no sub command");
            _logger.LogError(exception, "Command group {Name} had no sub command", commandGroupName);
            throw exception;
        }
    }
}