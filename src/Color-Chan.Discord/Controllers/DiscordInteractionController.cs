using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Commands;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Rest.Models.Interaction;
using Color_Chan.Discord.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Controllers
{
    /// <summary>
    ///     The api controller that will receive all the interaction events from discord.
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{apiVersion}/discord")]
    public class DiscordInteractionController : ControllerBase
    {
        private const string ReturnContentType = "application/json";
        private readonly IDiscordInteractionAuthService _authService;
        private readonly ILogger<DiscordInteractionController> _logger;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISlashCommandService _slashCommandService;

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordInteractionController" />.
        /// </summary>
        /// <param name="authService">The <see cref="IDiscordInteractionAuthService" /> that will verify the request.</param>
        /// <param name="slashCommandService">The <see cref="ISlashCommandService" /> that will handle slash command interactions.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="serializerOptions">The JSON options used for serialization.</param>
        public DiscordInteractionController(IDiscordInteractionAuthService authService, ISlashCommandService slashCommandService, ILogger<DiscordInteractionController> logger,
                                            IServiceProvider serviceProvider, IOptions<JsonSerializerOptions> serializerOptions)
        {
            _authService = authService;
            _slashCommandService = slashCommandService;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _serializerOptions = serializerOptions.Value;
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
            // Todo: Benchmark

            // Get the raw body data.
            using var bodyReader = new StreamReader(Request.Body);
            if (bodyReader.BaseStream.CanSeek) bodyReader.BaseStream.Seek(0, SeekOrigin.Begin);
            string rawBody = await bodyReader.ReadToEndAsync().ConfigureAwait(false);

            // Verify the interaction request.
            var signature = Request.Headers["X-Signature-Ed25519"].ToString();
            var timeStamp = Request.Headers["X-Signature-Timestamp"].ToString();
            if (!_authService.VerifySignature(signature, timeStamp, rawBody))
            {
                _logger.LogWarning("Failed to verify interaction command");
                return Unauthorized("Failed to verify signatures");
            }

            // Convert the JSON body to a DiscordInteractionData object.
            if (Request.Body.CanSeek) Request.Body.Seek(0, SeekOrigin.Begin);
            var interactionData = await JsonSerializer.DeserializeAsync<DiscordInteractionData>(Request.Body, _serializerOptions).ConfigureAwait(false);
            if (interactionData is null) throw new JsonException("Failed to deserialize JSON body to DiscordInteractionData");

            _logger.LogDebug("Verified Interaction {Id}", interactionData.Id.ToString());

            // Execute the correct interaction type.
            var result = interactionData.Type switch
            {
                DiscordInteractionType.Ping => DiscordInteractionResponse.PingResponse().ToDataModel(),
                DiscordInteractionType.MessageComponent => throw new NotSupportedException("MessageComponent interactions are currently not supported yet!"),
                DiscordInteractionType.ApplicationCommand => await HandleSlashCommandAsync(new DiscordInteraction(interactionData)).ConfigureAwait(false),
                _ => throw new NotSupportedException("This interaction type is currently not supported!")
            };

            return Content(JsonSerializer.Serialize(result, result.GetType(), _serializerOptions), ReturnContentType, Encoding.UTF8);
        }

        private async Task<DiscordInteractionResponseData> HandleSlashCommandAsync(IDiscordInteraction interaction)
        {
            // Todo: make a dedicated interaction slash command handler.

            if (interaction.Data is null) throw new ArgumentNullException(nameof(interaction.Data), "Interaction data can not be null for a slash command!");

            var context = new SlashCommandContext(interaction.Data)
            {
                User = interaction.User,
                Message = interaction.Message,
                Member = interaction.GuildMember,
                GuildId = interaction.GuildId,
                ChannelId = interaction.ChannelId!.Value,
                ApplicationId = interaction.ApplicationId,
                CommandRequest = interaction.Data
            };

            // Todo: improve this mess.

            if (interaction.Data.Options is not null)
            {
                // Check if any of the used options was a sub command (group) and execute it.
                foreach (var option in interaction.Data.Options)
                {
                    switch (option.Type)
                    {
                        case DiscordApplicationCommandOptionType.SubCommand:
                            return await ExecuteSubCommand(interaction.Data.Name, option, context).ConfigureAwait(false);
                        case DiscordApplicationCommandOptionType.SubCommandGroup:
                            return await ExecuteSubCommandGroup(interaction.Data.Name, option, context).ConfigureAwait(false);
                    }
                }
            }
            
            // Execute normal slash command.
            var result = await _slashCommandService.ExecuteSlashCommandAsync(interaction.Data.Name, context, interaction.Data.Options?.ToList(), _serviceProvider).ConfigureAwait(false);
            if (result.IsSuccessful) return result.Entity!.ToDataModel();

            // Todo: error handling.
            throw new NotImplementedException();
        }

        private async Task<DiscordInteractionResponseData> ExecuteSubCommand(string commandGroupName, IDiscordInteractionCommandOption option, ISlashCommandContext context)
        {
            var result = await _slashCommandService.ExecuteSlashCommandAsync(commandGroupName, option.Name, context, option.SubOptions?.ToList(), _serviceProvider).ConfigureAwait(false);
            if (result.IsSuccessful) return result.Entity!.ToDataModel();

            // Todo: error handling.
            throw new NotImplementedException();
        }

        private async Task<DiscordInteractionResponseData> ExecuteSubCommandGroup(string commandGroupName, IDiscordInteractionCommandOption option, ISlashCommandContext context)
        {
            if (option.SubOptions is null)
            {
                throw new NullReferenceException("A sub command group needs to sub commands");
            }

            foreach (var subOption in option.SubOptions)
            {
                if (subOption.Type == DiscordApplicationCommandOptionType.SubCommand)
                {
                    var result = await _slashCommandService.ExecuteSlashCommandAsync(commandGroupName, option.Name, subOption.Name, context, subOption.SubOptions?.ToList(), _serviceProvider).ConfigureAwait(false);
                    if (result.IsSuccessful) return result.Entity!.ToDataModel();

                    // Todo: error handling.
                    throw new NotImplementedException();
                }
            }

            _logger.LogWarning("Command group {Name} had no sub command", commandGroupName);
            return new DiscordInteractionResponseData();
        }
    }
}