#pragma warning disable 1998
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace HelloWorldButton.Components
{
    public class HelloWorldComponent : ComponentInteractionModule
    {
        public const string HelloWorldComponentId = "hello_world";

        [Component(HelloWorldComponentId, DiscordComponentType.Button)]
        public async Task<Result<IDiscordInteractionResponse>> HelloWorldButtonAsync()
        {
            var responseBuilder = new SlashCommandResponseBuilder()
                                  .WithContent("Hello world!")
                                  .MakePrivate();
            
            return Result<IDiscordInteractionResponse>.FromSuccess(responseBuilder.Build());
        }
    }
}