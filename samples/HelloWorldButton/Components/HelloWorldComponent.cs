#pragma warning disable 1998
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace HelloWorldButton.Components
{
    /// <summary>
    ///     The component module for the hello world components.
    /// </summary>
    public class HelloWorldComponent : ComponentInteractionModule
    {
        public const string HelloWorldComponentId = "hello_world";

        /// <summary>
        ///     Edits a message with a component with the id of <see cref="HelloWorldComponentId"/>.
        /// </summary>
        [Component(HelloWorldComponentId, DiscordComponentType.Button)]
        public async Task<Result<IDiscordInteractionResponse>> HelloWorldButtonAsync()
        {
            var response = new SlashCommandResponseBuilder()
                           .WithContent("Hello world!")
                           .EmptyComponents()
                           .Build(DiscordInteractionResponseType.UpdateMessage);

            return FromSuccess(response);
        }
    }
}