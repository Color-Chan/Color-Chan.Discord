#pragma warning disable 1998
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Commands.Tests.Valid.Requirements;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Rest.Models.Interaction;

namespace Color_Chan.Discord.Commands.Tests.Valid
{
    [BoolRequirement(true)]
    [BoolRequirement(true)]
    [BoolRequirement(true)]
    [BoolRequirement(true)]
    public class ValidMockCommandModule5 : SlashCommandModule
    {
        [SlashCommand("Command14", "a unit test command.")]
        [BoolRequirement(true)]
        [BoolRequirement(true)]
        [BoolRequirement(true)]
        public async Task<IDiscordInteractionResponse> CommandMethod14Async()
        {
            return new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.ChannelMessageWithSource
            };
        }

        [SlashCommand("Command15", "a unit test command.")]
        [BoolRequirement(true)]
        [BoolRequirement(true)]
        public async Task<IDiscordInteractionResponse> CommandMethod15Async()
        {
            return new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.ChannelMessageWithSource
            };
        }

        [SlashCommand("Command16", "a unit test command.")]
        [BoolRequirement(true)]
        [BoolRequirement(true)]
        [BoolRequirement(true)]
        [BoolRequirement(true)]
        public async Task<IDiscordInteractionResponse> CommandMethod16Async()
        {
            return new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.ChannelMessageWithSource
            };
        }

        [SlashCommand("Command17", "a unit test command.")]
        [BoolRequirement(true)]
        [BoolRequirement(true)]
        public async Task<IDiscordInteractionResponse> CommandMethod17Async()
        {
            return new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.ChannelMessageWithSource
            };
        }
    }
}