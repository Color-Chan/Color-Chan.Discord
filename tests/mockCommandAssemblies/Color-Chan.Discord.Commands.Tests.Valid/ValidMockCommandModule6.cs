﻿#pragma warning disable 1998
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models.Interaction;

namespace Color_Chan.Discord.Commands.Tests.Valid;

[SlashCommandGroup("add", "Adds something to a thing.")]
public class ValidMockCommandModule6 : SlashCommandModule
{
    [SlashCommandGroup("role", "Adds a role to the server.")]
    [SlashCommand("Command18", "a unit test command.")]
    public async Task<Result<IDiscordInteractionResponse>> CommandMethod1Async
    (
        [SlashCommandOption("RoleName", "A RoleName.")]
        string roleName,
        [SlashCommandOption("Number", "A random number.", false)]
        int number
    )
    {
        return FromSuccess(new DiscordInteractionResponse
        {
            Type = DiscordInteractionCallbackType.ChannelMessageWithSource,
            Data = new DiscordInteractionCallback
            {
                Content = $"{nameof(roleName)}:{roleName}:{nameof(number)}:{number.ToString()}"
            }
        });
    }
}