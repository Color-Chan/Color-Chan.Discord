#pragma warning disable 1998
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models.Interaction;

namespace Color_Chan.Discord.Commands.Tests.Valid;

public class ValidMockCommandModule8 : SlashCommandModule
{
    [SlashCommand("Command20", "a unit test command.")]
    public async Task<Result<IDiscordInteractionResponse>> CommandMethod20Async
    (
        [SlashCommandOption("name", "nullable string", false)]
        string? colorName,
        [SlashCommandOption("int", "nullable int", false)]
        int? colorNumber,
        [SlashCommandOption("user", "nullable ulong", false, DiscordApplicationCommandOptionType.User)]
        ulong? userId,
        [SlashCommandOption("booly", "nullable bool", false)]
        bool? boolValue,
        [SlashCommandOption("number", "nullable double", false)]
        double? number,
        [SlashCommandOption("channel", "nullable channel", false, DiscordApplicationCommandOptionType.Channel, DiscordChannelType.GuildText)]
        ulong? channel
    )
    {
        return FromSuccess(new DiscordInteractionResponse
        {
            Type = DiscordInteractionCallbackType.ChannelMessageWithSource
        });
    }
}