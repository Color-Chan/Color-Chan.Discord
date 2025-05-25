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
                new SectionComponentBuilder()
                    .WithAccessory(new ThumbnailComponentBuilder().WithUrl("https://minio.proxied.brammys.com/screenshots/2025/05/rO14p3jrl5A8cirC.png"))
                    .WithSubComponent(new TextDisplayComponentBuilder().WithContent("test text"))
                    .Build()
            );

        return Task.FromResult(FromSuccess(responseBuilder.Build()));
    }
}