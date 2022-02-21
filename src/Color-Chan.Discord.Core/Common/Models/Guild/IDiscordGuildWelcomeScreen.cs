using System.Collections.Generic;

namespace Color_Chan.Discord.Core.Common.Models.Guild
{
    /// <summary>
    ///     Represents a discord Welcome Screen Structure API model.
    ///     Docs: https://discord.com/developers/docs/resources/guild#welcome-screen-object-welcome-screen-structure
    /// </summary>
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