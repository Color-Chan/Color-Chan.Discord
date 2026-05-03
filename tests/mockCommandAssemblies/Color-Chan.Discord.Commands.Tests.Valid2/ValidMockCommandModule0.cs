using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Tests.Valid2;

public class ValidMockCommandModule0 : SlashCommandModule
{
    [SlashCommand("AGoodCommand", "a unit test command.")]
    public Task<Result<IDiscordInteractionResponse>> AGoodCommand()
    {
        throw new Exception();
    }
}