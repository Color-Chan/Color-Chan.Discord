#pragma warning disable 1998
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models.Interaction;

namespace Color_Chan.Discord.Commands.Tests.Valid
{
    [SlashCommandGroupAttribute("add", "Adds something to a thing.")]
    public class ValidMockCommandModule6 : SlashCommandModule
    {
        [SlashCommandGroupAttribute("role", "Adds a role to the server.")]
        [SlashCommand("Command18", "a unit test command.")]
        public async Task<Result<IDiscordInteractionResponse>> CommandMethod1Async
        (
            [SlashCommandOption("Role name", "A role name.")]
            string roleName,
            [SlashCommandOption("Number", "A random number.", false)]
            int number
        )
        {
            return FromSuccess(new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.ChannelMessageWithSource,
                Data = new DiscordInteractionCommandCallback
                {
                    Content = $"{nameof(roleName)}:{roleName}:{nameof(number)}:{number}"
                }
            });
        }
    }
}