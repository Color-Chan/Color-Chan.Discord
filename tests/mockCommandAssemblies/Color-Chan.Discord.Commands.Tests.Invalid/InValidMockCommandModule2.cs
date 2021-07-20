using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Commands.Tests.Invalid
{
    public abstract class InValidMockCommandModule2 : SlashCommandModule
    {
        [SlashCommand("Command4", "a unit test command.")]
        public abstract Task<IDiscordInteractionResponse> CommandMethod4Async();
    }
}