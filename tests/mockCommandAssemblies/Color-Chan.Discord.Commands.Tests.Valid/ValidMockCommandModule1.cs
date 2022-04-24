using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Commands.Tests.Valid.Requirements;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Tests.Valid
{
    [SlashCommandGuild(ulong.MinValue)]
    public class ValidMockCommandModule1 : SlashCommandModule
    {
        [SlashCommand("Command1", "a unit test command.")]
        [BoolRequirement(false)]
        [BoolRequirement(false)]
        [BoolRequirement(false)]
        [BoolRequirement(true)]
        [SlashCommandGuild(ulong.MaxValue)]
        public Task<Result<IDiscordInteractionResponse>> CommandMethod1Async
        (
            [SlashCommandOption("RoleName", "A RoleName.")]
            [SlashCommandChoice("roleName 1", "RoleName1")]
            [SlashCommandChoice("RoleName 2", "RoleName2")]
            [SlashCommandChoice("RoleName 3", "RoleName3")]
            [SlashCommandChoice("RoleName 4", "RoleName4")]
            string roleName,
            [SlashCommandOption("Number", "A random number.", false)]
            [SlashCommandChoice("value name 1", "1")]
            [SlashCommandChoice("value name 1", "2")]
            [SlashCommandChoice("value name 1", "3")]
            [SlashCommandChoice("value name 1", "4")]
            int number
        )
        {
            throw new Exception();
        }

        [SlashCommand("Command2", "a unit test command.")]
        [BoolRequirement(true)]
        [BoolRequirement(true)]
        public Task<Result<IDiscordInteractionResponse>> CommandMethod2Async
        (
            [SlashCommandOption("RoleName", "A RoleName.")]
            [SlashCommandChoice("RoleName 1", "RoleName1")]
            [SlashCommandChoice("RoleName 2", "RoleName2")]
            [SlashCommandChoice("RoleName 3", "RoleName3")]
            [SlashCommandChoice("RoleName 4", "RoleName4")]
            string roleName,
            [SlashCommandOption("Number", "A random number.", false)]
            [SlashCommandChoice("Value name 1", "1")]
            [SlashCommandChoice("Value name 2", "2")]
            [SlashCommandChoice("Value name 3", "3")]
            [SlashCommandChoice("Value name 4", "4")]
            int number
        )
        {
            throw new Exception();
        }

        [SlashCommand("Command3", "a unit test command.")]
        [BoolRequirement(false)]
        public Task<Result<IDiscordInteractionResponse>> CommandMethod3Async()
        {
            throw new Exception();
        }
    }
}