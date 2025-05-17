using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.MessageBuilders.Components;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace ComponentsV2.Commands;

public class HelloWorld : SlashCommandModule
{
    [SlashCommand("hello", "Says hello to the user.")]
    public Task<Result<IDiscordInteractionResponse>> InitButtonAsync()
    {
        var responseBuilder = new InteractionResponseBuilder()
            .WithMessageFlag(DiscordMessageFlags.IsComponentV2)
            .WithComponent(
                new LayoutComponentBuilder(DiscordComponentType.ActionRow)
                    .WithSubComponent(
                        new ButtonComponentBuilder()
                            .WithLabel("test 1")
                            .WithUrl("https://example.com")
                            .WithStyle(DiscordButtonStyle.Link)
                    )
                    .WithSubComponent(
                        new ButtonComponentBuilder()
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
            .WithComponent(new TextDisplayComponentBuilder().WithContent("- this is a test").Build())
            .WithComponent(new TextDisplayComponentBuilder().WithContent("# this is a test").Build())
            .WithComponent(new TextDisplayComponentBuilder().WithContent("## this is a test").Build())
            .WithComponent(new TextDisplayComponentBuilder().WithContent("### this is a test").Build())
            .WithComponent(
                new LayoutComponentBuilder(DiscordComponentType.Section)
                    .WithSubComponent(new TextDisplayComponentBuilder().WithContent("# section test"))
                    .WithAccessory(new ButtonComponentBuilder().WithLabel("test").WithStyle(DiscordButtonStyle.Danger).WithCustomId("test"))
                    .Build()
            );

        return Task.FromResult(FromSuccess(responseBuilder.Build()));
    }
}