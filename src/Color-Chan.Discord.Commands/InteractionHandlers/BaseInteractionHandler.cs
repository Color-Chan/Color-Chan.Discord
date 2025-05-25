using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Commands.InteractionHandlers;

public class BaseInteractionHandler(
    IOptions<SlashCommandConfiguration> slashCommandConfiguration,
    IDiscordRestGuild restGuild,
    IDiscordRestChannel restChannel
)
{
    protected async Task<IDiscordChannel?> GetChannelAsync(IDiscordInteraction interaction)
    {
        if (!slashCommandConfiguration.Value.EnableAutoGetChannel || interaction.ChannelId is null) return null;
        var channelResult = await restChannel.GetChannelAsync(interaction.ChannelId.Value).ConfigureAwait(false);
        return channelResult.Entity;
    }

    protected async Task<IDiscordGuild?> GetGuildAsync(IDiscordInteraction interaction)
    {
        if (!slashCommandConfiguration.Value.EnableAutoGetGuild || interaction.GuildId is null) return null;

        var guildResult = await restGuild.GetGuildAsync(interaction.GuildId.Value, true).ConfigureAwait(false);
        return guildResult.Entity;
    }
}