using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Core.Common.Models.Guild
{
    /// <summary>
    ///     Represents a discord Guild Application Command Permissions Structure API model.
    ///     Docs: https://discord.com/developers/docs/interactions/slash-commands#application-command-permissions-object-guild-application-command-permissions-structure
    /// </summary>
    public interface IDiscordGuildApplicationCommandPermissions
    {
        /// <summary>
        ///     Unique id of the command.
        /// </summary>
        public ulong CommandId { get; set; }

        /// <summary>
        ///     The id of the application the command belongs to.
        /// </summary>
        public ulong ApplicationId { get; set; }

        /// <summary>
        ///     The id of the guild.
        /// </summary>
        public ulong GuildId { get; set; }

        /// <summary>
        ///     The permissions for the command in the guild.
        /// </summary>
        public IEnumerable<IDiscordApplicationCommandPermissions> Permissions { get; set; }
    }
}