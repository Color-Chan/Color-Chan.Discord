using System;
using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Extensions
{
    /// <summary>
    ///     Contains all the extensions methods for <see cref="DiscordGuildPermission" />.
    /// </summary>
    public static class DiscordGuildPermissionExtensions
    {
        /// <summary>
        ///     Get all the flags of <see cref="DiscordGuildPermission"/> separately in a list.
        /// </summary>
        /// <param name="permission">The permission flags.</param>
        /// <returns>
        ///     A list of <see cref="DiscordGuildPermission"/> flags.
        /// </returns>
        public static List<DiscordGuildPermission> ToList(this DiscordGuildPermission? permission)
        {
            if (permission is null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            return ToList(permission.Value);
        }
        
        /// <summary>
        ///     Get all the flags of <see cref="DiscordGuildPermission"/> separately in a list.
        /// </summary>
        /// <param name="permission">The permission flags.</param>
        /// <returns>
        ///     A list of <see cref="DiscordGuildPermission"/> flags.
        /// </returns>
        public static List<DiscordGuildPermission> ToList(this DiscordGuildPermission permission)
        {
            return Enum.GetValues(typeof(DiscordGuildPermission))
                       .Cast<Enum>()
                       .Where(permission.HasFlag)
                       .Cast<DiscordGuildPermission>()
                       .ToList();
        }
    }
}