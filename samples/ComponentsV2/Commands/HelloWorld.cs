using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace ComponentsV2.Commands;

public class HelloWorld : SlashCommandModule
{
    [SlashCommand("hello", "Says hello to the user.")]
    public Task<Result<IDiscordInteractionResponse>> InitButtonAsync()
    {
        var components = new ActionRowComponentBuilder()
            .WithButton("test", DiscordButtonStyle.Link, url: "https://google.com")
            .WithButton("test", DiscordButtonStyle.Link, url: "https://google.com")
            .WithButton("test", DiscordButtonStyle.Link, url: "https://google.com")
            .WithButton("test", DiscordButtonStyle.Link, url: "https://google.com");
        
        var responseBuilder = new InteractionResponseBuilder()
            .WithComponent(components.Build())
            .UseComponentsV2();

        return Task.FromResult(FromSuccess(responseBuilder.Build()));
    }
}