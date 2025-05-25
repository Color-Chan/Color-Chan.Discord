using System.Drawing;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.MessageBuilders.Components;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace ComponentsV2.Commands;

[SlashCommandGroup("hello", "A command group that contains all the 'hello' sub commands.")]
public class HelloWorld : SlashCommandModule
{
    [SlashCommand("world", "Send a messaging contains componentsV2 items.")]
    public Task<Result<IDiscordInteractionResponse>> InitButtonAsync()
    {
        var responseBuilder = new InteractionResponseBuilder()
            .WithMessageFlag(DiscordMessageFlags.IsComponentV2)
            .WithComponent(
                new ContainerComponentBuilder()
                    .WithAccentColor(Color.BlueViolet)
                    .WithSpoiler(true)
                    .WithSubComponent(
                        new TextDisplayComponentBuilder().WithContent(
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                        )
                    )
                    .WithSubComponent(new MediaGalleryComponentBuilder().WithMedia("https://minio.proxied.brammys.com/screenshots/2025/05/oXqvVuzEHGg8XKnT.png"))
                    .Build()
            );

        return Task.FromResult(FromSuccess(responseBuilder.Build()));
    }
}