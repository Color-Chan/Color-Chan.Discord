using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Guild
{
    public record DiscordGuildWelcomeScreen : IDiscordGuildWelcomeScreen
    {
        public DiscordGuildWelcomeScreen(DiscordGuildWelcomeScreenData dataWelcomeScreen)
        {
            Description = dataWelcomeScreen.Description;
            WelcomeChannels = dataWelcomeScreen.WelcomeChannels.Select(data=>new DiscordGuildWelcomeChannel(data));
        }

        /// <inheritdoc />
        public string? Description { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordGuildWelcomeChannel> WelcomeChannels { get; set; } = new List<IDiscordGuildWelcomeChannel>();
    }
}