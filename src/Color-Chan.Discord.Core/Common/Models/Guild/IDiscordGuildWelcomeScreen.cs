using System.Collections.Generic;

namespace Color_Chan.Discord.Core.Common.Models.Guild
{
    public interface IDiscordGuildWelcomeScreen
    {
        /// <summary>
        ///     The server description shown in the welcome screen.
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        ///     The server description shown in the welcome screen.
        /// </summary>
        IEnumerable<IDiscordGuildWelcomeChannel> WelcomeChannels { get; set; }
    }
}