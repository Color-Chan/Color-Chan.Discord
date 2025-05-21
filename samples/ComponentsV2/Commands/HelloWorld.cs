using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.MessageBuilders.Components;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
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
                new ActionRowComponentBuilder()
                    .WithSubComponent(
                        new ButtonComponentBuilder()
                            .WithId(5)
                            .WithLabel("test 1")
                            .WithUrl("https://example.com")
                            .WithStyle(DiscordButtonStyle.Link)
                    )
                    .WithSubComponent(
                        new ButtonComponentBuilder()
                            .WithId(7)
                            .WithLabel("test 2")
                            .WithUrl("https://example.com")
                            .WithStyle(DiscordButtonStyle.Link)
                    )
                    .WithSubComponent(
                        new ButtonComponentBuilder()
                            .WithId(1)
                            .WithLabel("test 3")
                            .WithUrl("https://example.com")
                            .WithStyle(DiscordButtonStyle.Link)
                    )
                    .Build()
            )
            .WithComponent(new TextDisplayComponentBuilder().WithId(4).WithContent("- this is a test").Build())
            .WithComponent(new TextDisplayComponentBuilder().WithId(10).WithContent("# this is a test").Build())
            .WithComponent(new TextDisplayComponentBuilder().WithId(8).WithContent("## this is a test").Build())
            .WithComponent(new TextDisplayComponentBuilder().WithContent("### this is a test").Build())
            .WithComponent(
                new SectionComponentBuilder()
                    .WithSubComponent(new TextDisplayComponentBuilder().WithContent("# section test"))
                    .WithAccessory(new ButtonComponentBuilder().WithLabel("test").WithStyle(DiscordButtonStyle.Danger).WithCustomId("test"))
                    .Build()
            );

        return Task.FromResult(FromSuccess(responseBuilder.Build()));
    }
}