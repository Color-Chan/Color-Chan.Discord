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
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Valid
{
    [SlashCommandGroupAttribute("role", "a role command")]
    public class ValidMockCommandModule7 : SlashCommandModule
    {
        [SlashCommand("Command19", "the command")]
        public async Task<Result<IDiscordInteractionResponse>> CommandMethod19Async
        (
            [SlashCommandOption("role", "Role id.", true, DiscordApplicationCommandOptionType.Role)]
            ulong roleId
        )
        {
            Assert.NotNull(roleId);
            Assert.NotNull(SlashContext);
            Assert.NotNull(SlashContext.Data);
            Assert.NotNull(SlashContext.Data.Resolved);
            Assert.NotNull(SlashContext.Data.Resolved?.Roles);

            var role = SlashContext.Data.Resolved?.Roles?.FirstOrDefault(x => x.Key == roleId).Value;
            Assert.NotNull(role);

            return FromSuccess(new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.ChannelMessageWithSource,
                Data = new DiscordInteractionCallback
                {
                    Content = roleId.ToString()
                }
            });
        }
    }
}