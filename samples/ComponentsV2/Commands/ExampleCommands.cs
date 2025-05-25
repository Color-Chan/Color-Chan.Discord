using System.Drawing;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.MessageBuilders.Components;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace ComponentsV2.Commands;

/// <summary>
///     The command module for all the 'example' sub command.
/// </summary>
[SlashCommandGroup("example", "A command group that contains all the 'example' sub commands.")]
public class ExampleCommands : SlashCommandModule
{
    [SlashCommand("action-row", "Send an action row with a button components.")]
    public Task<Result<IDiscordInteractionResponse>> ActionRowExample()
    {
        var actionRowBuilder = new ActionRowComponentBuilder()
            .WithSubComponent(
                new ButtonComponentBuilder()
                    .WithLabel("Click Me!")
                    .WithStyle(DiscordButtonStyle.Primary)
                    .WithCustomId("example:action-row-button")
            )
            .WithSubComponent(
                new ButtonComponentBuilder()
                    .WithLabel("Don't Click Me!")
                    .WithStyle(DiscordButtonStyle.Danger)
                    .WithCustomId("example:action-row-button-2")
            )
            .WithSubComponent(
                new ButtonComponentBuilder()
                    .WithLabel("Info")
                    .WithStyle(DiscordButtonStyle.Link)
                    .WithUrl("https://colorchan.com/")
            );

        var responseBuilder = new InteractionResponseBuilder()
            .WithMessageFlag(DiscordMessageFlags.IsComponentV2)
            .WithComponent(actionRowBuilder.Build());

        return Task.FromResult(FromSuccess(responseBuilder.Build()));
    }

    [SlashCommand("section", "Send a section example.")]
    public Task<Result<IDiscordInteractionResponse>> SectionExample()
    {
        var sectionBuilder = new SectionComponentBuilder()
            .WithSubComponent(new TextDisplayComponentBuilder().WithContent("# Release notes V1.0.0"))
            .WithSubComponent(
                new TextDisplayComponentBuilder().WithContent(
                    "There is a new update that supports components v2!\n" +
                    "- New components\n" +
                    "- Make components more customizable\n" +
                    "- More control over the look and feel of your bot!"
                )
            )
            .WithSubComponent(new TextDisplayComponentBuilder().WithContent("-# The update is live now!"))
            .WithAccessory(
                new ThumbnailComponentBuilder()
                    .WithUrl("https://minio.proxied.brammys.com/screenshots/2025/05/kLmL64rMqU9A0h8R.png")
                    .WithDescription("Update logo")
            );

        var responseBuilder = new InteractionResponseBuilder()
            .WithMessageFlag(DiscordMessageFlags.IsComponentV2)
            .WithComponent(sectionBuilder.Build());

        return Task.FromResult(FromSuccess(responseBuilder.Build()));
    }

    [SlashCommand("media", "Send a media example.")]
    public Task<Result<IDiscordInteractionResponse>> MediaExample()
    {
        var responseBuilder = new InteractionResponseBuilder()
            .WithMessageFlag(DiscordMessageFlags.IsComponentV2)
            .WithComponent(new TextDisplayComponentBuilder().WithContent("# Random screenshots:").Build())
            .WithComponent(
                new MediaGalleryComponentBuilder()
                    .WithMedia("https://minio.proxied.brammys.com/screenshots/2025/05/ekahV4j6CmCUrRbs.png")
                    .WithMedia("https://minio.proxied.brammys.com/screenshots/2025/05/fTPosPI6JoKIF1Kp.png")
                    .WithMedia("https://minio.proxied.brammys.com/screenshots/2025/05/anyFxgvRG1rNy2rW.png")
                    .Build()
            );

        return Task.FromResult(FromSuccess(responseBuilder.Build()));
    }

    [SlashCommand("container", "Send a container example.")]
    public Task<Result<IDiscordInteractionResponse>> ContainerExample()
    {
        var containerBuilder = new ContainerComponentBuilder()
            .WithAccentColor(Color.Aquamarine)
            .WithSpoiler(true)
            .WithSubComponent(new TextDisplayComponentBuilder().WithContent("This is a container component with a text display."))
            .WithSubComponent(new SeparatorComponentBuilder().WithSpacing(2).WithDivider(false))
            .WithSubComponent(new TextDisplayComponentBuilder().WithContent("Is this great?"))
            .WithSubComponent(new SeparatorComponentBuilder())
            .WithSubComponent(
                new ActionRowComponentBuilder()
                    .WithSubComponent(
                        new ButtonComponentBuilder()
                            .WithLabel("This is great!")
                            .WithStyle(DiscordButtonStyle.Primary)
                            .WithCustomId("example:container-button")
                    )
                    .WithSubComponent(
                        new ButtonComponentBuilder()
                            .WithLabel("This is great! 2")
                            .WithStyle(DiscordButtonStyle.Secondary)
                            .WithCustomId("example:container-button2")
                    )
            );

        var responseBuilder = new InteractionResponseBuilder()
            .WithMessageFlag(DiscordMessageFlags.IsComponentV2)
            .WithComponent(containerBuilder.Build());

        return Task.FromResult(FromSuccess(responseBuilder.Build()));
    }
}