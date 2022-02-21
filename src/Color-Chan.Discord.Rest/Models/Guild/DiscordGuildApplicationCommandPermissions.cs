using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models.Application;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Rest.Models.Application;

namespace Color_Chan.Discord.Rest.Models.Guild
{
    /// <inheritdoc cref="IDiscordGuildApplicationCommandPermissions"/>
    public class DiscordGuildApplicationCommandPermissions : IDiscordGuildApplicationCommandPermissions
    {
        /// <summary>
        ///     Initializes a new <see cref="DiscordGuildApplicationCommandPermissions"/>
        /// </summary>
        /// <param name="data">The data needed to create the <see cref="DiscordGuildApplicationCommandPermissions"/>.</param>
        public DiscordGuildApplicationCommandPermissions(DiscordGuildApplicationCommandPermissionsData data)
        {
            CommandId = data.CommandId;
            ApplicationId = data.ApplicationId;
            GuildId = data.GuildId;
            Permissions = data.Permissions.Select(x => new DiscordApplicationCommandPermissions(x));
        }

        /// <inheritdoc />
        public ulong CommandId { get; set; }

        /// <inheritdoc />
        public ulong ApplicationId { get; set; }

        /// <inheritdoc />
        public ulong GuildId { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordApplicationCommandPermissions> Permissions { get; set; }
    }
}