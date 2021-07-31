#pragma warning disable 1998
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Rest.Models.Interaction;

namespace Color_Chan.Discord.Commands.Tests.Valid
{
    public class ValidMockCommandModule6 : SlashCommandModule
    {
        [SlashCommand("Command18", "a unit test command.")]
        public async Task<IDiscordInteractionResponse> CommandMethod1Async
        (
            [SlashCommandOption("Role name", "A role name.")]
            string roleName,
            [SlashCommandOption("Number", "A random number.", false)]
            int number
        )
        {
            return new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.ChannelMessageWithSource,
                Data = new DiscordInteractionCommandCallback
                {
                    Content = $"{nameof(roleName)}:{roleName}:{nameof(number)}:{number}"
                }
            };
        }
    }
}