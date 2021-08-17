#pragma warning disable 1998
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using HelloWorldButton.Components;

namespace HelloWorldButton.Commands
{
    /// <summary>
    ///     The command module for all the 'hello' sub command.
    /// </summary>
    [SlashCommandGroup("hello", "A command group that contains all the 'hello' sub commands.")]
    public class HelloWorldButtonCommand : SlashCommandModule
    {
        /// <summary>
        ///     initializes a hello world component button.
        /// </summary>
        /// <example>
        ///     /hello world
        /// </example>
        [UserRateLimit(2, 10)] // Sets the rate limit for this command to 2 requests per 10 seconds per user.
        [SlashCommand("world", "Initializes a button component.")]
        public async Task<Result<IDiscordInteractionResponse>> InitButtonAsync()
        {
            var actionRowBuilder = new ActionRowComponentBuilder()
                .WithButton("Hello world", DiscordButtonStyle.Primary, HelloWorldComponent.HelloWorldComponentId);

            var responseBuilder = new InteractionResponseBuilder()
                                  .WithContent("hello world button")
                                  .WithComponent(actionRowBuilder.Build());
            
            //  Return the response to Discord.
            return FromSuccess(responseBuilder.Build());
        }
    }
}