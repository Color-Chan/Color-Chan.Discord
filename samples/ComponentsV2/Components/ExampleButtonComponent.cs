using System.Drawing;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.MessageBuilders.Components;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace ComponentsV2.Components;

/// <summary>
///     The component module for the example button components.
/// </summary>
public class ExampleButtonComponent : ComponentInteractionModule
{
    public const string ButtonId = "button_with_example";

    /// <summary>
    ///     Edits a message with a component with the id of <see cref="ButtonId" />.
    /// </summary>
    [Component(ButtonId, DiscordComponentType.Button, false, true)]
    public Task<Result<IDiscordInteractionResponse>> ExampleButtonButtonAsync()
    {
        var containerBuilder = new ContainerComponentBuilder()
            .WithAccentColor(Color.Aquamarine)
            .WithSubComponent(new TextDisplayComponentBuilder().WithContent("This is a container component with a text display."));

        var responseBuilder = new InteractionResponseBuilder()
            .WithMessageFlag(DiscordMessageFlags.IsComponentV2)
            .WithComponent(containerBuilder.Build());

        return Task.FromResult(FromSuccess(responseBuilder.Build()));
    }
}