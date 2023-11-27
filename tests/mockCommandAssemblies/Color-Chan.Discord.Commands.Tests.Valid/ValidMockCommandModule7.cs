#pragma warning disable 1998
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models.Interaction;
using FluentAssertions;

namespace Color_Chan.Discord.Commands.Tests.Valid;

[SlashCommandGroup("role", "a role command")]
public class ValidMockCommandModule7 : SlashCommandModule
{
    [SlashCommand("Command19", "the command")]
    public async Task<Result<IDiscordInteractionResponse>> CommandMethod19Async(
        [SlashCommandOption("role", "Role id.", true, DiscordApplicationCommandOptionType.Role)]
        ulong roleId
    )
    {
        Context.Should().NotBeNull();
        Context.Data.Should().NotBeNull();
        Context.Data.Resolved.Should().NotBeNull();
        Context.Data.Resolved?.Roles.Should().NotBeNull();

        var role = Context.Data.Resolved?.Roles?.FirstOrDefault(x => x.Key == roleId).Value;
        role.Should().NotBeNull();

        return FromSuccess(
            new DiscordInteractionResponse
            {
                Type = DiscordInteractionCallbackType.ChannelMessageWithSource,
                Data = new DiscordInteractionCallback
                {
                    Content = roleId.ToString()
                }
            }
        );
    }
}