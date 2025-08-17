using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.InteractionHandlers;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Configurations;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.Params.Webhook;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Parsers.Interfaces;
using Color_Chan.Discord.Rest.Models.Interaction;
using Color_Chan.Discord.Rest.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Controllers;

/// <summary>
///     The api controller that will receive all the interaction events from discord.
/// </summary>
[ApiController]
[ApiVersion("1")]
[Route("api/v{apiVersion}/discord")]
public class DiscordInteractionController : ControllerBase
{
    private const string SignatureHeader = "X-Signature-Ed25519";
    private const string TimeStampHeader = "X-Signature-Timestamp";
    private const string ReturnContentType = "application/json";
    private readonly IDiscordInteractionAuthService _authService;
    private readonly InteractionsConfiguration _configuration;
    private readonly DiscordTokens _discordTokens;
    private readonly ILogger<DiscordInteractionController> _logger;
    private readonly IDiscordInteractionParser _discordInteractionParser;
    private readonly IEnumerable<IInteractionHandler> _interactionHandlers;
    private readonly IDiscordRestApplication _restApplication;

    /// <summary>
    ///     Initializes a new instance of <see cref="DiscordInteractionController" />.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="authService">The <see cref="IDiscordInteractionAuthService" /> that will verify the request.</param>
    /// <param name="discordInteractionParser">The parser for all the discord interactions.</param>
    /// <param name="interactionHandlers">The interaction handlers that will handle the interactions.</param>
    /// <param name="configuration">The configurations for interactions.</param>
    /// <param name="restApplication">The REST class for application api calls.</param>
    /// <param name="discordTokens">The bot tokens and IDs.</param>
    public DiscordInteractionController(
        ILogger<DiscordInteractionController> logger,
        IDiscordInteractionAuthService authService,
        IDiscordInteractionParser discordInteractionParser,
        IEnumerable<IInteractionHandler> interactionHandlers,
        IOptions<InteractionsConfiguration> configuration,
        IDiscordRestApplication restApplication,
        DiscordTokens discordTokens
    )
    {
        _authService = authService;
        _logger = logger;
        _discordInteractionParser = discordInteractionParser;
        _interactionHandlers = interactionHandlers;
        _configuration = configuration.Value;
        _restApplication = restApplication;
        _discordTokens = discordTokens;
    }

    /// <summary>
    ///     Handles an interaction event from discord.
    /// </summary>
    /// <returns>
    ///     An <see cref="ActionResult" /> with the json result of the request.
    /// </returns>
    [HttpPost("interaction")]
    public async Task<ActionResult> HandleInteractionRequestAsync()
    {
        if (!await ValidateSignatureAsync().ConfigureAwait(false))
        {
            _logger.LogWarning("Interaction {Id} : Failed to verify interaction command", "unknown");
            return Unauthorized("Failed to verify signatures");
        }

        var interactionData = await _discordInteractionParser.ParseInteractionAsync(Request.Body).ConfigureAwait(false);
        _logger.LogDebug("Interaction {Id} : Verified", interactionData.Id.ToString());

        var interactionHandler = _interactionHandlers.FirstOrDefault(handler => handler.CanHandle(new DiscordInteraction(interactionData)));
        if (interactionHandler is null)
        {
            _logger.LogWarning("Interaction {Id} : No handler found for interaction type {Type}", interactionData.Id.ToString(), interactionData.RequestType);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var interactionResponse = await interactionHandler.HandleInteractionAsync(new DiscordInteraction(interactionData)).ConfigureAwait(false);

        // Check if the response contains a message.
        if (interactionResponse.Response is null)
        {
            if (interactionData.RequestType != DiscordInteractionRequestType.MessageComponent)
            {
                throw new NullReferenceException("The interaction response can not be null for Application Commands.");
            }

            _logger.LogDebug("Interaction {Id} : Returning empty interaction response to discord", interactionData.Id.ToString());
            return SerializeResult(new DiscordInteractionResponseData { Type = DiscordInteractionCallbackType.DeferredUpdateMessage });
        }

        if (!interactionResponse.Acknowledged)
        {
            _logger.LogDebug("Interaction {Id} : Returning interaction response to discord", interactionData.Id.ToString());
            return SerializeResult(interactionResponse.Response);
        }

        // Send an edit request.
        var responseData = interactionResponse.Response.Data?.ToDataModel();
        var editResponse = new DiscordEditWebhookMessage
        {
            Content = responseData?.Content,
            Embeds = responseData?.Embeds,
            Components = responseData?.Components,
            AllowedMentions = responseData?.AllowedMentions,
            Flags = responseData?.Flags,
        };

        _logger.LogDebug("Interaction {Id} : Editing original interaction response", interactionData.Id.ToString());
        var responseResult = await _restApplication.EditOriginalInteractionResponseAsync(_discordTokens.ApplicationId, interactionData.Token, editResponse).ConfigureAwait(false);
        if (responseResult.IsSuccessful) return Ok();

        if (responseResult.ErrorResult is DiscordHttpErrorResult httpErrorResult)
        {
            // Send an error response.
            _logger.LogWarning(
                "Interaction {Id} : Failed to edit interaction response, reason: {Message}, details: {Details}",
                interactionData.Id.ToString(),
                responseResult.ErrorResult?.ErrorMessage,
                JsonSerializer.Serialize(httpErrorResult.ErrorData)
            );
            return StatusCode(StatusCodes.Status500InternalServerError, responseResult.ErrorResult);
        }

        // Send an error response.
        _logger.LogWarning("Interaction {Id} : Failed to edit interaction response, reason: {Message}", interactionData.Id.ToString(), responseResult.ErrorResult?.ErrorMessage);
        return StatusCode(StatusCodes.Status500InternalServerError, responseResult.ErrorResult);
    }

    private async Task<bool> ValidateSignatureAsync()
    {
        // Check if the request has the headers needed for verification.
        if (!Request.Headers.TryGetValue(SignatureHeader, out var signature) || !Request.Headers.TryGetValue(TimeStampHeader, out var timeStamp))
        {
            return false;
        }

        if (_configuration.VerifyInteractions && !await _authService.VerifySignatureAsync(signature.ToString(), timeStamp.ToString(), Request.Body).ConfigureAwait(false))
        {
            return false;
        }

        return true;
    }

    private ContentResult SerializeResult(IDiscordInteractionResponse result)
    {
        var data = result.ToDataModel();
        return SerializeResult(data);
    }

    private ContentResult SerializeResult(DiscordInteractionResponseData data)
    {
        var json = _discordInteractionParser.SerializeInteraction(data);
        return Content(json, ReturnContentType, Encoding.UTF8);
    }
}