using System;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Models.Contexts;
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
        private readonly ILogger<ComponentInteractionHandler> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IComponentService _componentService;
        private readonly ComponentInteractionConfiguration _options;

        /// <summary>
        ///     Initializes a new instance of <see cref="ComponentInteractionHandler" />.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" /> for <see cref="ComponentInteractionHandler" />.</param>
        /// <param name="serviceProvider">The services needed to execute the component interactions.</param>
        /// <param name="componentService">The <see cref="IComponentService"/> used to search and execute the correct components.</param>
        /// <param name="options">The <see cref="ComponentInteractionConfiguration"/> containing the configuration data for component interactions.</param>
        public ComponentInteractionHandler(ILogger<ComponentInteractionHandler> logger, IServiceProvider serviceProvider, IComponentService componentService, 
                                           IOptions<ComponentInteractionConfiguration> options)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _componentService = componentService;
            _options = options.Value;
        }

        /// <inheritdoc />
        public async Task<IDiscordInteractionResponse> HandleComponentInteractionAsync(IDiscordInteraction interaction)
        {
            if (interaction.Data is null)
            {
                throw new ArgumentNullException(nameof(interaction.Data), $"{nameof(interaction.Data)} can not be null for a component interaction!");
            }
            
            InteractionContext context = new ()
            {
                User = interaction.User ?? interaction.GuildMember?.User ?? throw new NullReferenceException(nameof(context.User)),
                Message = interaction.Message,
                Member = interaction.GuildMember,
                GuildId = interaction.GuildId,
                ChannelId = interaction.ChannelId ?? interaction.User?.Id ?? throw new NullReferenceException(nameof(context.User)),
                ApplicationId = interaction.ApplicationId,
                Data = interaction.Data,
                InteractionId = interaction.Id,
                Token = interaction.Token
            };
            
            // Local method to execute the command.
            async Task<Result<IDiscordInteractionResponse>> Handler()
            {
                return await _componentService.ExecuteComponentInteractionAsync(context, _serviceProvider).ConfigureAwait(false);
            }

            // Execute the pipelines and the component interaction.
            var result = await _serviceProvider.GetServices<IComponentInteractionPipeline>()
                                               .Aggregate((ComponentInteractionHandlerDelegate)Handler, (next, pipeline) => () => pipeline.HandleAsync(context, next))().ConfigureAwait(false);

            // Return the response.
            if (result.IsSuccessful) return result.Entity!;

            if (_options.SendDefaultErrorMessage)
            {
                _logger.LogWarning("Sending default error message");
                return new SlashCommandResponseBuilder().DefaultErrorMessage();
            }

            throw new ComponentInteractionResultException($"Component interaction request {interaction.Id} returned unsuccessfully, {result.ErrorResult?.ErrorMessage}");
        }
    }
}