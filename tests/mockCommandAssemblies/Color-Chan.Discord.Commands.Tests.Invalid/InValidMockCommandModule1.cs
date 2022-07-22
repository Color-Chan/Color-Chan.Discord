using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Tests.Invalid;

public class InValidMockCommandModule1 : SlashCommandModule
{
    [SlashCommand("Command1", "a unit test command.")]
    public static Task<Result<IDiscordInteractionResponse>> CommandMethod1Async()
    {
        throw new Exception("Command error reason 1");
    }

    [SlashCommand("Command2", "a unit test command.")]
    public static Task<string> CommandMethod2Async()
    {
        return Task.FromResult("result");
    }
}