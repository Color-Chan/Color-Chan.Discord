﻿using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Commands.Services.InteractionHandlers;
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
    private readonly IDiscordSlashCommandHandler _commandHandler;
    private readonly IComponentInteractionHandler _componentInteractionHandler;
    private readonly InteractionsConfiguration _configuration;
    private readonly DiscordTokens _discordTokens;
    private readonly ILogger<DiscordInteractionController> _logger;
    private readonly IDiscordInteractionParser _discordInteractionParser;
    private readonly IDiscordRestApplication _restApplication;

    /// <summary>
    ///     Initializes a new instance of <see cref="DiscordInteractionController" />.
    /// </summary>
    /// <param name="authService">The <see cref="IDiscordInteractionAuthService" /> that will verify the request.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="discordInteractionParser">The parser for all the discord interactions.</param>
    /// <param name="commandHandler">The command handler for all the slash commands.</param>
    /// <param name="configuration">The configurations for interactions.</param>
    /// <param name="restApplication">The REST class for application api calls.</param>
    /// <param name="discordTokens">The bot tokens and IDs.</param>
    /// <param name="componentInteractionHandler">The handler for the component interaction requests.</param>
    public DiscordInteractionController(IDiscordInteractionAuthService authService, ILogger<DiscordInteractionController> logger, IDiscordInteractionParser discordInteractionParser,
                                        IDiscordSlashCommandHandler commandHandler, IOptions<InteractionsConfiguration> configuration, IDiscordRestApplication restApplication,
                                        DiscordTokens discordTokens, IComponentInteractionHandler componentInteractionHandler)
    {
        _authService = authService;
        _logger = logger;
        _discordInteractionParser = discordInteractionParser;
        _commandHandler = commandHandler;
        _configuration = configuration.Value;
        _restApplication = restApplication;
        _discordTokens = discordTokens;
        _componentInteractionHandler = componentInteractionHandler;
    }

    /// <summary>
    ///     Handles an interaction event from discord.
    /// </summary>
    /// <returns>
    ///     An <see cref="ActionResult" /> with the json result of the request.
    /// </returns>
    /// <exception cref="JsonException">Thrown when the provided request body isn't valid.</exception>
    [HttpPost("interaction")]
    public async Task<ActionResult> HandleInteractionRequestAsync()
    {
        // Check if the request has the headers needed for verification.
        if (!Request.Headers.ContainsKey(SignatureHeader) || !Request.Headers.ContainsKey(TimeStampHeader))
        {
            return UnauthorizedInteraction();
        }

        // Verify the interaction request.
        var signature = Request.Headers[SignatureHeader].ToString();
        var timeStamp = Request.Headers[TimeStampHeader].ToString();

        if (_configuration.VerifyInteractions && !await _authService.VerifySignatureAsync(signature, timeStamp, Request.Body).ConfigureAwait(false))
        {
            return UnauthorizedInteraction();
        }

        var interactionData = await _discordInteractionParser.ParseInteractionAsync(Request.Body).ConfigureAwait(false);
        _logger.LogDebug("Interaction {Id} : Verified", interactionData.Id.ToString());

        // Execute the correct interaction type.
        var interactionResponse = interactionData.RequestType switch
        {
            DiscordInteractionRequestType.Ping => InternalInteractionResponse.PingResponse(),
            DiscordInteractionRequestType.ApplicationCommand => await _commandHandler.HandleSlashCommandAsync(new DiscordInteraction(interactionData)).ConfigureAwait(false),
            DiscordInteractionRequestType.MessageComponent => await _componentInteractionHandler.HandleComponentInteractionAsync(new DiscordInteraction(interactionData)).ConfigureAwait(false),
            _ => throw new ArgumentOutOfRangeException()
        };

        // Check if the response contains a message.
        if (interactionResponse.Response is null)
        {
            if (interactionData.RequestType != DiscordInteractionRequestType.MessageComponent)
            {
                throw new NullReferenceException("The interaction response can not be null for Application Commands.");
            }

            _logger.LogDebug("Interaction {Id} : Returning empty interaction response to discord", interactionData.Id.ToString());
            return SerializeResult(new DiscordInteractionResponseData
            {
                Type = DiscordInteractionCallbackType.DeferredUpdateMessage
            });
        }

        if (!interactionResponse.Acknowledged)
        {
            // Send the response back to discord.
            _logger.LogDebug("Interaction {Id} : Returning interaction response to discord", interactionData.Id.ToString());
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return SerializeResult(interactionResponse.Response);
        }

        // Send an edit request.
        var responseData = interactionResponse.Response.Data?.ToDataModel();
        var editResponse = new DiscordEditWebhookMessage
        {
            Content = responseData?.Content,
            Embeds = responseData?.Embeds,
            Components = responseData?.Components,
            AllowedMentions = responseData?.AllowedMentions
        };

        _logger.LogDebug("Interaction {Id} : Editing original interaction response", interactionData.Id.ToString());
        var responseResult = await _restApplication.EditOriginalInteractionResponseAsync(_discordTokens.ApplicationId, interactionData.Token, editResponse).ConfigureAwait(false);
        if (responseResult.IsSuccessful) return Ok();

        if (responseResult.ErrorResult is DiscordHttpErrorResult httpErrorResult)
        {
            // Send an error response.
            _logger.LogWarning("Interaction {Id} : Failed to edit interaction response, reason: {Message}, details: {Details}",
                               interactionData.Id.ToString(),
                               responseResult.ErrorResult?.ErrorMessage,
                               JsonSerializer.Serialize(httpErrorResult.ErrorData));
            return StatusCode(StatusCodes.Status500InternalServerError, responseResult.ErrorResult);
        }

        // Send an error response.
        _logger.LogWarning("Interaction {Id} : Failed to edit interaction response, reason: {Message}", interactionData.Id.ToString(), responseResult.ErrorResult?.ErrorMessage);
        return StatusCode(StatusCodes.Status500InternalServerError, responseResult.ErrorResult);
    }

    /// <summary>
    ///     Serializes a <see cref="IDiscordInteractionResponse" /> to json content.
    /// </summary>
    /// <param name="result">The <see cref="IDiscordInteractionResponse" /> that will be serialized.</param>
    /// <returns>
    ///     The serialized <see cref="IDiscordInteractionResponse" />.
    /// </returns>
    private ContentResult SerializeResult(IDiscordInteractionResponse result)
    {
        var data = result.ToDataModel();
        return SerializeResult(data);
    }

    /// <summary>
    ///     Serializes a <see cref="DiscordInteractionResponseData" /> to json content.
    /// </summary>
    /// <param name="data">The <see cref="DiscordInteractionResponseData" /> that will be serialized.</param>
    /// <returns>
    ///     The serialized <see cref="DiscordInteractionResponseData" />.
    /// </returns>
    private ContentResult SerializeResult(DiscordInteractionResponseData data)
    {
        return Content(_discordInteractionParser.SerializeInteraction(data), ReturnContentType, Encoding.UTF8);
    }

    private ActionResult UnauthorizedInteraction()
    {
        _logger.LogWarning("Interaction {Id} : Failed to verify interaction command", "unknown");
        return Unauthorized("Failed to verify signatures");
    }
}