using System;

namespace Color_Chan.Discord.Commands.Models
{
    [Flags]
    public enum SlashCommandsAutoSync
    {
        /// <summary>
        ///     Disable the slash command auto sync.
        /// </summary>
        Disabled = 1,

        /// <summary>
        ///     Auto sync new and existing slash commands.
        /// </summary>
        AddUpdate = 2,

        /// <summary>
        ///     Auto sync deleted slash commands.
        /// </summary>
        Delete = 3
    }
}