﻿using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Tests.Valid;

public class ValidMockCommandModule2 : SlashCommandModule
{
    [SlashCommand("Command4", "a unit test command.")]
    public Task<Result<IDiscordInteractionResponse>> CommandMethod4Async()
    {
        throw new Exception();
    }

    [SlashCommand("Command5", "a unit test command.")]
    public Task<Result<IDiscordInteractionResponse>> CommandMethod5Async()
    {
        throw new Exception();
    }

    [SlashCommand("Command6", "a unit test command.")]
    public Task<Result<IDiscordInteractionResponse>> CommandMethod6Async()
    {
        throw new Exception();
    }

    [SlashCommand("Command7", "a unit test command.")]
    public Task<Result<IDiscordInteractionResponse>> CommandMethod7Async()
    {
        throw new Exception();
    }
}