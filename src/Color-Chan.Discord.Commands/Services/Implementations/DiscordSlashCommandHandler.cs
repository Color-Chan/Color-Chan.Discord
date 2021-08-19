using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
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
        private readonly IDiscordRestApplication _restApplication;
        private readonly IDiscordRestChannel _restChannel;
        private readonly IDiscordRestGuild _restGuild;
        private readonly IServiceProvider _serviceProvider;
        private readonly SlashCommandConfiguration _slashCommandConfiguration;
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
        /// <param name="restApplication">The rest class for application calls.</param>
        public DiscordSlashCommandHandler(ISlashCommandService slashCommandService, IServiceProvider serviceProvider, ILogger<DiscordSlashCommandHandler> logger,
                                          IDiscordRestApplication restApplication, IDiscordRestGuild restGuild, IDiscordRestChannel restChannel,
                                          IOptions<SlashCommandConfiguration> slashCommandConfiguration)
        {
            _slashCommandService = slashCommandService;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _restApplication = restApplication;
            _restGuild = restGuild;
            _restChannel = restChannel;
            _slashCommandConfiguration = slashCommandConfiguration.Value;
        }

        /// <inheritdoc />
        public async Task<InternalInteractionResponse> HandleSlashCommandAsync(IDiscordInteraction interaction)
        {
            if (interaction.Data is null)
            {
                throw new ArgumentNullException(nameof(interaction.Data), $"{nameof(interaction.Data)} can not be null for a slash command!");
            }

            IDiscordGuild? guild = null;
            if (_slashCommandConfiguration.EnableAutoGetGuild && interaction.GuildId is not null)
            {
                var guildResult = await _restGuild.GetGuildAsync(interaction.GuildId.Value, true).ConfigureAwait(false);
                guild = guildResult.Entity;
                _logger.LogDebug("Interaction: {Id} : Fetched guild data {GuildId}", interaction.Id.ToString(), interaction.GuildId.Value.ToString());
            }

            IDiscordChannel? channel = null;
            if (_slashCommandConfiguration.EnableAutoGetGuild && interaction.ChannelId is not null)
            {
                var channelResult = await _restChannel.GetChannelAsync(interaction.ChannelId.Value).ConfigureAwait(false);
                channel = channelResult.Entity;
                _logger.LogDebug("Interaction: {Id} : Fetched channel data {ChannelId}", interaction.Id.ToString(), interaction.ChannelId.Value.ToString());
            }

            ISlashCommandContext context = new SlashCommandContext
            {
                User = interaction.User ?? interaction.GuildMember?.User ?? throw new NullReferenceException(nameof(context.User)),
                Message = interaction.Message,
                Member = interaction.GuildMember,
                GuildId = interaction.GuildId,
                ChannelId = interaction.ChannelId ?? interaction.User?.Id ?? throw new NullReferenceException(nameof(context.User)),
                ApplicationId = interaction.ApplicationId,
                Data = interaction.Data,
                InteractionId = interaction.Id,
                Token = interaction.Token,
                Channel = channel,
                Guild = guild
            };

            IEnumerable<IDiscordInteractionOption>? options = null;

            // Get the command name and the options.
            if (interaction.Data.Options is null)
            {
                options = interaction.Data.Options;
                context.SlashCommandName = new[] { interaction.Data.Name };
            }
            else
            {
                // Check if any of the used options is a sub command (group).
                foreach (var option in interaction.Data.Options)
                {
                    switch (option.Type)
                    {
                        case DiscordApplicationCommandOptionType.SubCommand:
                            context.SlashCommandName = new[] { interaction.Data.Name, option.Name };
                            options = option.SubOptions;
                            break;
                        case DiscordApplicationCommandOptionType.SubCommandGroup:
                            if (option.SubOptions is null) throw new NullReferenceException("A sub command group needs to have options");
                            context.SlashCommandName = new[] { interaction.Data.Name, option.Name };
                            foreach (var subOption in option.SubOptions)
                            {
                                if (subOption.Type == DiscordApplicationCommandOptionType.SubCommand)
                                {
                                    context.SlashCommandName = context.SlashCommandName.Append(subOption.Name);
                                    options = subOption.SubOptions;
                                }
                            }
                            break;
                    }
                }
            }
            
            // Search for a top level or sub command.
            var arr = context.SlashCommandName.ToArray();
            var count = arr.Length;
            ISlashCommandInfo? commandInfo = null;
            ISlashCommandOptionInfo? optionInfo = null;
            switch (count)
            {
                case 1:
                {
                    commandInfo = _slashCommandService.SearchSlashCommand(arr[0]);
                    break;
                }
                case 2:
                {
                    optionInfo = _slashCommandService.SearchSlashCommand(arr[0], arr[1]);
                    break;
                }
                case 3:
                {
                    optionInfo = _slashCommandService.SearchSlashCommand(arr[0], arr[1], arr[2]);
                    break;
                }
            }

            if (commandInfo is null && optionInfo is null)
            {
                throw new NullReferenceException($"Failed to find the requested interaction command {interaction.Data.Name}");
            }

            // Acknowledge the slash command request if needed.
            var acknowledged = false;
            if (commandInfo is not null && commandInfo.Acknowledge || optionInfo is not null && optionInfo.Acknowledge)
            {
                var acknowledgeResponse = new DiscordInteractionResponseData
                {
                    Type = DiscordInteractionResponseType.DeferredChannelMessageWithSource
                };

                var acknowledgeResult = await _restApplication.CreateInteractionResponseAsync(interaction.Id, interaction.Token, acknowledgeResponse).ConfigureAwait(false);
                if (!acknowledgeResult.IsSuccessful)
                {
                    _logger.LogWarning("Interaction: {Id} : Failed to acknowledge interaction command, reason: {Message}", interaction.Id.ToString(), acknowledgeResult.ErrorResult?.ErrorMessage);
                }
                else
                {
                    _logger.LogDebug("Interaction: {Id} : Acknowledged interaction command", interaction.Id.ToString());
                    acknowledged = true;
                }
            }

            // Local method to execute the command.
            async Task<Result<IDiscordInteractionResponse>> Handler()
            {
                if (commandInfo is not null)
                {
                    return await _slashCommandService.ExecuteSlashCommandAsync(commandInfo, context, options?.ToList(), _serviceProvider).ConfigureAwait(false);
                }

                return await _slashCommandService.ExecuteSlashCommandAsync(optionInfo!, context, options?.ToList(), _serviceProvider).ConfigureAwait(false);
            }

            // Execute the pipelines and the command.
            var result = await _serviceProvider.GetServices<ISlashCommandPipeline>()
                                               .Aggregate((SlashCommandHandlerDelegate)Handler, (next, pipeline) => () => pipeline.HandleAsync(context, next))().ConfigureAwait(false);

            // Return the response.
            if (result.IsSuccessful)
            {
                _logger.LogWarning("Interaction: {Id} : Slash command interaction returned successfully", interaction.Id.ToString());
                return new InternalInteractionResponse(acknowledged, result.Entity!);
            }

            _logger.LogWarning("Interaction: {Id} : Slash command interaction returned unsuccessfully, reason: {ErrorReason}", interaction.Id.ToString(), result.ErrorResult?.ErrorMessage);
            if (_slashCommandConfiguration.SendDefaultErrorMessage)
            {
                _logger.LogWarning("Interaction: {Id} : Sending default error message", interaction.Id.ToString());
                return new InternalInteractionResponse(acknowledged, new InteractionResponseBuilder().DefaultErrorMessage());
            }

            throw new SlashCommandResultException($"Command request {interaction.Id} returned unsuccessfully, {result.ErrorResult?.ErrorMessage}");
        }
    }
}