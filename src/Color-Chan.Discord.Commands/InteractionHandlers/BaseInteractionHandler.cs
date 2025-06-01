using System;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Commands.InteractionHandlers;

/// <summary>
///     The base class for all interaction handlers.
/// </summary>
/// <param name="slashCommandConfiguration">The configuration for slash commands.</param>
/// <param name="restGuild">The REST class for guild API calls.</param>
/// <param name="restChannel">The REST class for channel API calls.</param>
/// <param name="logger">The logger for logging messages.</param>
public class BaseInteractionHandler(
    IOptions<SlashCommandConfiguration> slashCommandConfiguration,
    IDiscordRestGuild restGuild,
    IDiscordRestChannel restChannel,
    IDiscordRestApplication restApplication,
    ILogger logger
)
{
    protected InternalInteractionResponse GetInternalInteractionResponse(Result<IDiscordInteractionResponse> result, bool acknowledged, ulong interactionId)
    {
        // Return the response.
        if (result.IsSuccessful)
        {
            logger.LogInformation("Interaction: {Id} : Slash command interaction returned successfully", interactionId.ToString());
            return new InternalInteractionResponse(acknowledged, result.Entity!);
        }

        logger.LogWarning("Interaction: {Id} : Slash command interaction returned unsuccessfully, reason: {ErrorReason}", interactionId.ToString(), result.ErrorResult?.ErrorMessage);
        if (slashCommandConfiguration.Value.SendDefaultErrorMessage)
        {
            logger.LogWarning("Interaction: {Id} : Sending default error message", interactionId.ToString());
            return new InternalInteractionResponse(acknowledged, new InteractionResponseBuilder().DefaultErrorMessage());
        }
        
        logger.LogError("Interaction: {Id} : Failed to handle interaction command, reason: {ErrorReason}", interactionId.ToString(), result.ErrorResult?.ErrorMessage);
        throw new InteractionResultException($"Command request {interactionId} returned unsuccessfully, {result.ErrorResult?.ErrorMessage}");
    }
    
    protected async Task<bool> AcknowledgedIfRequiredAsync(IDiscordInteraction interaction, DiscordInteractionCallbackType callbackType)
    {
        var acknowledgeResponse = new DiscordInteractionResponseData
        {
            Type = callbackType
        };

        var acknowledgeResult = await restApplication.CreateInteractionResponseAsync(interaction.Id, interaction.Token, acknowledgeResponse).ConfigureAwait(false);
        if (!acknowledgeResult.IsSuccessful)
        {
            logger.LogWarning("Interaction: {Id} : Failed to acknowledge interaction command, reason: {Message}", interaction.Id.ToString(), acknowledgeResult.ErrorResult?.ErrorMessage);
            return false;
        }

        logger.LogDebug("Interaction: {Id} : Acknowledged interaction command", interaction.Id.ToString());
        return true;
    }

    protected async Task<IInteractionContext> GetInteractionContextAsync(IDiscordInteraction interaction)
    {
        ArgumentNullException.ThrowIfNull(interaction.Data);

        var guild = await GetGuildAsync(interaction).ConfigureAwait(false);
        var channel = await GetChannelAsync(interaction).ConfigureAwait(false);

        IInteractionContext context = new InteractionContext()
        {
            User = interaction.User ?? interaction.GuildMember?.User ?? throw new NullReferenceException(nameof(context.User)),
            Message = interaction.Message,
            Member = interaction.GuildMember,
            GuildId = interaction.GuildId,
            ChannelId = interaction.ChannelId ?? interaction.User?.Id ?? throw new NullReferenceException(nameof(context.User)),
            ApplicationId = interaction.ApplicationId,
            Data = interaction.Data!,
            InteractionId = interaction.Id,
            Token = interaction.Token,
            Channel = channel,
            Guild = guild,
            Permissions = interaction.Permissions,
            Entitlements = interaction.Entitlements
        };

        return context;
    }
    
    private async Task<IDiscordChannel?> GetChannelAsync(IDiscordInteraction interaction)
    {
        if (!slashCommandConfiguration.Value.EnableAutoGetChannel || interaction.ChannelId is null) return null;
        var channelResult = await restChannel.GetChannelAsync(interaction.ChannelId.Value).ConfigureAwait(false);
        return channelResult.Entity;
    }

    private async Task<IDiscordGuild?> GetGuildAsync(IDiscordInteraction interaction)
    {
        if (!slashCommandConfiguration.Value.EnableAutoGetGuild || interaction.GuildId is null) return null;

        var guildResult = await restGuild.GetGuildAsync(interaction.GuildId.Value, true).ConfigureAwait(false);
        return guildResult.Entity;
    }
}