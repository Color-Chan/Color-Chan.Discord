#pragma warning disable 1998
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models.Interaction;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Tests.Valid;

public class ValidMockCommandModule4 : SlashCommandModule
{
    private readonly ILogger<ValidMockCommandModule4> _logger;

    public ValidMockCommandModule4(ILogger<ValidMockCommandModule4> logger)
    {
        _logger = logger;
    }

    [SlashCommand("Command10", "a unit test command.")]
    public async Task<Result<IDiscordInteractionResponse>> CommandMethod10Async()
    {
        _logger.LogInformation("Command10 executed");
        return FromSuccess(new DiscordInteractionResponse
        {
            Type = DiscordInteractionResponseType.ChannelMessageWithSource
        });
    }

    [SlashCommand("Command11", "a unit test command.")]
    public async Task<Result<IDiscordInteractionResponse>> CommandMethod11Async()
    {
        _logger.LogInformation("Command11 executed");
        return FromSuccess(new DiscordInteractionResponse
        {
            Type = DiscordInteractionResponseType.ChannelMessageWithSource
        });
    }

    [SlashCommand("Command12", "a unit test command.")]
    public async Task<Result<IDiscordInteractionResponse>> CommandMethod12Async()
    {
        _logger.LogInformation("Command12 executed");
        return FromSuccess(new DiscordInteractionResponse
        {
            Type = DiscordInteractionResponseType.ChannelMessageWithSource
        });
    }

    [SlashCommand("Command13", "a unit test command.")]
    public async Task<Result<IDiscordInteractionResponse>> CommandMethod13Async()
    {
        _logger.LogInformation("Command13 executed");
        return FromSuccess(new DiscordInteractionResponse
        {
            Type = DiscordInteractionResponseType.ChannelMessageWithSource
        });
    }
}