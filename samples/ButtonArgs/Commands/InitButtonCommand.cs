#pragma warning disable 1998
using System.Threading.Tasks;
using ButtonArgs.Components;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace ButtonArgs.Commands;

/// <summary>
///     The command module for all the 'init' sub command.
/// </summary>
[SlashCommandGroup("init", "A command group that contains all the 'init' sub commands.")]
public class InitButtonCommand : SlashCommandModule
{
    /// <summary>
    ///     initializes a button component.
    /// </summary>
    /// <example>
    ///     /init button
    /// </example>
    [UserRateLimit(2, 10)] // Sets the rate limit for this command to 2 requests per 10 seconds per user.
    [SlashCommand("button", "Initializes a button component.")]
    public async Task<Result<IDiscordInteractionResponse>> InitButtonAsync
    (
        [SlashCommandOption("args", "A string argument", true)]
        string args,
        [SlashCommandOption("args2", "A string argument", false)]
        string? args2,
        [SlashCommandOption("args3", "A string argument", false)]
        string? args3
    )
    {
        var customId = $"{ArgsButtonComponent.ButtonId};{args}";
        if (args2 is not null) customId += $";{args2}";
        if (args3 is not null) customId += $";{args3}";

        var actionRowBuilder = new ActionRowComponentBuilder()
            .WithButton("Don't press me!", DiscordButtonStyle.Danger, customId);

        var responseBuilder = new InteractionResponseBuilder()
                              .WithContent("Press the button below!")
                              .WithComponent(actionRowBuilder.Build());

        //  Return the response to Discord.
        return FromSuccess(responseBuilder.Build());
    }
}