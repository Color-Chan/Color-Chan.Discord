using System;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Commands.Models.Contexts;
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
    public class ComponentInteractionHandler : IComponentInteractionHandler
    {
        private readonly IComponentService _componentService;
        private readonly ILogger<ComponentInteractionHandler> _logger;
        private readonly ComponentInteractionConfiguration _options;
        private readonly IDiscordRestApplication _restApplication;
        private readonly IDiscordRestChannel _restChannel;
        private readonly IDiscordRestGuild _restGuild;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        ///     Initializes a new instance of <see cref="ComponentInteractionHandler" />.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" /> for <see cref="ComponentInteractionHandler" />.</param>
        /// <param name="serviceProvider">The services needed to execute the component interactions.</param>
        /// <param name="componentService">The <see cref="IComponentService" /> used to search and execute the correct components.</param>
        /// <param name="options">
        ///     The <see cref="ComponentInteractionConfiguration" /> containing the configuration data for
        ///     component interactions.
        /// </param>
        /// <param name="restGuild">The rest class for Guilds.</param>
        /// <param name="restChannel">The rest class for Channels.</param>
        /// <param name="application">The rest class the Application calls.</param>
        public ComponentInteractionHandler(ILogger<ComponentInteractionHandler> logger, IServiceProvider serviceProvider, IComponentService componentService,
                                           IOptions<ComponentInteractionConfiguration> options, IDiscordRestGuild restGuild, IDiscordRestChannel restChannel, IDiscordRestApplication application)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _componentService = componentService;
            _restGuild = restGuild;
            _restChannel = restChannel;
            _restApplication = application;
            _options = options.Value;
        }

        /// <inheritdoc />
        public async Task<InternalInteractionResponse> HandleComponentInteractionAsync(IDiscordInteraction interaction)
        {
            if (interaction.Data is null)
            {
                throw new ArgumentNullException(nameof(interaction.Data), $"{nameof(interaction.Data)} can not be null for a component interaction!");
            }

            if (interaction.Data.CustomId is null)
            {
                throw new ArgumentNullException(nameof(interaction.Data), $"{nameof(interaction.Data.CustomId)} can not be null for a component interaction!");
            }

            IDiscordGuild? guild = null;
            if (_options.EnableAutoGetGuild && interaction.GuildId is not null)
            {
                var guildResult = await _restGuild.GetGuildAsync(interaction.GuildId.Value, true).ConfigureAwait(false);
                guild = guildResult.Entity;
            }

            IDiscordChannel? channel = null;
            if (_options.EnableAutoGetGuild && interaction.ChannelId is not null)
            {
                var channelResult = await _restChannel.GetChannelAsync(interaction.ChannelId.Value).ConfigureAwait(false);
                channel = channelResult.Entity;
            }

            ComponentContext context = new()
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

            var customId = context.Data.CustomId;
            if (customId.Contains(_options.CustomIdDataSeparator))
            {
                var customIdData = context.Data.CustomId.Split(_options.CustomIdDataSeparator).ToList();
                customId = customIdData.First();
                
                customIdData.RemoveAt(0);
                context.Args.AddRange(customIdData);
            }
            
            var componentInfo = _componentService.SearchComponent(customId);
            if (componentInfo is null)
            {
                throw new NullReferenceException($"Failed to find the requested interaction component {interaction.Data.CustomId}");
            }

            // Acknowledge the component interaction request if needed.
            var acknowledged = false;
            if (componentInfo.Acknowledge)
            {
                var acknowledgeResponse = new DiscordInteractionResponseData
                {
                    Type = componentInfo.EditOriginalMessage ? DiscordInteractionResponseType.DeferredUpdateMessage : DiscordInteractionResponseType.DeferredChannelMessageWithSource
                };

                var acknowledgeResult = await _restApplication.CreateInteractionResponseAsync(interaction.Id, interaction.Token, acknowledgeResponse).ConfigureAwait(false);
                if (!acknowledgeResult.IsSuccessful)
                {
                    _logger.LogWarning("Failed to acknowledge interaction command {Id}, reason: {Message}", interaction.Id.ToString(), acknowledgeResult.ErrorResult?.ErrorMessage);
                }
                else
                {
                    _logger.LogDebug("Acknowledged component interaction {Id}", interaction.Id.ToString());
                    acknowledged = true;
                }
            }

            // Local method to execute the command.
            async Task<Result<IDiscordInteractionResponse>> Handler()
            {
                return await _componentService.ExecuteComponentInteractionAsync(componentInfo, context, _serviceProvider).ConfigureAwait(false);
            }

            // Execute the pipelines and the component interaction.
            var result = await _serviceProvider.GetServices<IComponentInteractionPipeline>()
                                               .Aggregate((ComponentInteractionHandlerDelegate)Handler, (next, pipeline) => () => pipeline.HandleAsync(context, next))().ConfigureAwait(false);

            // Return the response.
            if (result.IsSuccessful) return new InternalInteractionResponse(acknowledged, result.Entity!);

            if (_options.SendDefaultErrorMessage)
            {
                _logger.LogWarning("Sending default error message");
                return new InternalInteractionResponse(acknowledged, new InteractionResponseBuilder().DefaultErrorMessage());
            }

            throw new ComponentInteractionResultException($"Component interaction request {interaction.Id} returned unsuccessfully, {result.ErrorResult?.ErrorMessage}");
        }
    }
}