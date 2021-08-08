using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Tests.Invalid
{
    internal class InValidMockCommandModule3 : SlashCommandModule
    {
        [SlashCommand("Command5", "a unit test command.")]
        public Task<Result<IDiscordInteractionResponse>> CommandMethod5Async()
        {
            throw new Exception("Command error reason 5");
        }
    }
}