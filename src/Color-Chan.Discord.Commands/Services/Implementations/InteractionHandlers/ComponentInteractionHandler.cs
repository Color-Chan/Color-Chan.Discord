using System;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Services.InteractionHandlers;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Commands.Services.Implementations.InteractionHandlers;

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
        ArgumentNullException.ThrowIfNull(interaction.Data);
        ArgumentNullException.ThrowIfNull(interaction.Data.CustomId);

        var guild = await GetGuildAsync(interaction);
        var channel = await GetChannelAsync(interaction);

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
            Permissions = interaction.Permissions,
            Channel = channel,
            Guild = guild
        };

        var customId = GetCustomId(interaction, context);

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
                _logger.LogWarning("Interaction: {Id} : Failed to acknowledge interaction command, reason: {Message}", interaction.Id.ToString(), acknowledgeResult.ErrorResult?.ErrorMessage);
            }
            else
            {
                _logger.LogDebug("Interaction: {Id} : Acknowledged component interaction", interaction.Id.ToString());
                acknowledged = true;
            }
        }

        // Local method to execute the command.
        async Task<Result<IDiscordInteractionResponse>> Handler()
        {
            return await _componentService.ExecuteComponentInteractionAsync(componentInfo, context, _serviceProvider).ConfigureAwait(false);
        }

        // Execute the pipelines and the component interaction.
        var result = await _serviceProvider.GetServices<IInteractionPipeline>()
                                           .Aggregate((InteractionHandlerDelegate)Handler, (next, pipeline) => () => pipeline.HandleAsync(context, next))().ConfigureAwait(false);

        // Return the response.
        if (result.IsSuccessful)
        {
            _logger.LogInformation("Interaction: {Id} : Component interaction returned successfully", interaction.Id.ToString());
            return new InternalInteractionResponse(acknowledged, result.Entity!);
        }

        _logger.LogWarning("Interaction: {Id} : Component interaction returned unsuccessfully, reason: {ErrorReason}", interaction.Id.ToString(), result.ErrorResult?.ErrorMessage);
        if (_options.SendDefaultErrorMessage)
        {
            _logger.LogWarning("Interaction: {Id} : Sending default error message", interaction.Id.ToString());
            return new InternalInteractionResponse(acknowledged, new InteractionResponseBuilder().DefaultErrorMessage());
        }

        throw new ComponentInteractionResultException($"Component interaction request {interaction.Id} returned unsuccessfully, {result.ErrorResult?.ErrorMessage}");
    }

    private string GetCustomId(IDiscordInteraction interaction, IComponentContext context)
    {
        var customId = context.Data.CustomId!;
        if (!customId.Contains(_options.CustomIdDataSeparator))
        {
            return customId;
        }

        _logger.LogDebug("Interaction: {Id} : Parsing custom id arguments", interaction.Id.ToString());
        var customIdData = context.Data.CustomId!.Split(_options.CustomIdDataSeparator).ToList();
        customId = customIdData.First();

        customIdData.RemoveAt(0);
        context.Args.AddRange(customIdData);

        return customId;
    }

    private async Task<IDiscordChannel?> GetChannelAsync(IDiscordInteraction interaction)
    {
        if (!_options.EnableAutoGetChannel || interaction.ChannelId is null)
            return null;

        var channelResult = await _restChannel.GetChannelAsync(interaction.ChannelId.Value).ConfigureAwait(false);
        _logger.LogDebug("Interaction: {Id} : Fetched channel data {ChannelId}", interaction.Id.ToString(), interaction.ChannelId.Value.ToString());
        return channelResult.Entity;
    }

    private async Task<IDiscordGuild?> GetGuildAsync(IDiscordInteraction interaction)
    {
        if (!_options.EnableAutoGetGuild || interaction.GuildId is null)
            return null;

        var guildResult = await _restGuild.GetGuildAsync(interaction.GuildId.Value, true).ConfigureAwait(false);
        _logger.LogDebug("Interaction: {Id} : Fetched guild data {GuildId}", interaction.Id.ToString(), interaction.GuildId.Value.ToString());
        return guildResult.Entity;
    }
}