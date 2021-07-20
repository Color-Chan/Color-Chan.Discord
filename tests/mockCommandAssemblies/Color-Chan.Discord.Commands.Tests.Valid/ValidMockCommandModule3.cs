using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Commands.Tests.Valid
{
    public class ValidMockCommandModule3 : SlashCommandModule
    {
        [SlashCommand("CommandWithError1", "a unit test command.")]
        public Task<IDiscordInteractionResponse> CommandWithErrorMethod1Async()
        {
            throw new Exception("Command error 1");
        }

        [SlashCommand("CommandWithError2", "a unit test command.")]
        public Task<IDiscordInteractionResponse> CommandWithErrorMethod2Async()
        {
            throw new Exception("Command error 2");
        }
    }
}