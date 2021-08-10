using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Models.Contexts;
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
                ChannelId = interaction.ChannelId ?? interaction.User?.Id ?? throw new NullReferenceException(nameof(context.User)),
                ApplicationId = interaction.ApplicationId,
                CommandRequest = interaction.Data,
                InteractionId = interaction.Id,
                Token = interaction.Token,
                Channel = channel,
                Guild = guild
            };

            IEnumerable<IDiscordInteractionCommandOption>? options = null;
            
            // Get the command name and the options.
            if (interaction.Data.Options is null)
            {
                options = interaction.Data.Options;
                context.SlashCommandName = new []{interaction.Data.Name};
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
            
            // Local method to execute the command.
            async Task<Result<IDiscordInteractionResponse>> Handler()
            {
                return await _slashCommandService.ExecuteSlashCommandAsync(context, options?.ToList(), _serviceProvider).ConfigureAwait(false);
            }

            // Execute the pipelines and the command.
            var result = await _serviceProvider.GetServices<ISlashCommandPipeline>()
                                               .Aggregate((SlashCommandHandlerDelegate)Handler, (next, pipeline) => () => pipeline.HandleAsync(context, next))().ConfigureAwait(false);

            // Return the response.
            if (result.IsSuccessful) return result.Entity!;

            if (_slashCommandConfiguration.SendDefaultErrorMessage)
            {
                _logger.LogWarning("Sending default error message");
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
    }
}