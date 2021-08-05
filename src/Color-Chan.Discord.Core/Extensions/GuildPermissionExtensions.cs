using System;
using System.Diagnostics.CodeAnalysis;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Extensions
{
    internal static class GuildPermissionExtensions
    {
        /// <summary>
        ///     Convert a permission <see cref="ReadOnlySpan{T}" /> of <see cref="char" /> into a
        ///     <see cref="DiscordGuildPermission" />.
        /// </summary>
        /// <param name="permissions">
        ///     The <see cref="ReadOnlySpan{T}" /> of <see cref="char" /> that will be converter into a
        ///     <see cref="DiscordGuildPermission" />.
        /// </param>
        /// <returns>
        ///     The converted <see cref="DiscordGuildPermission" />.
        /// </returns>
        public static DiscordGuildPermission ConvertToGuildPermission(this ReadOnlySpan<char> permissions)
        {
            var number = ulong.Parse(permissions);
            return (DiscordGuildPermission)Enum.ToObject(typeof(DiscordGuildPermission), number);
        }

        /// <summary>
        ///     Convert a permission <see cref="string" /> into a <see cref="DiscordGuildPermission" />.
        /// </summary>
        /// <param name="permissions">
        ///     The <see cref="string" /> that will be converter into a <see cref="DiscordGuildPermission" />.
        /// </param>
        /// <returns>
        ///     The converted <see cref="DiscordGuildPermission" />.
        /// </returns>
        public static DiscordGuildPermission ConvertToGuildPermission(this string permissions)
        {
            var number = ulong.Parse(permissions);
            return (DiscordGuildPermission)Enum.ToObject(typeof(DiscordGuildPermission), number);
        }

        /// <summary>
        ///     Try to parse a <see cref="string" /> into a <see cref="DiscordGuildPermission" />.
        /// </summary>
        /// <param name="permissionString">
        ///     The <see cref="string" /> that will be converter into a
        ///     <see cref="DiscordGuildPermission" />.
        /// </param>
        /// <param name="permission">The converted <see cref="DiscordGuildPermission" />.</param>
        /// <returns>
        ///     Whether or not the <see cref="permissionString" /> has been converted to a <see cref="DiscordGuildPermission" />.
        /// </returns>
        public static bool TryParseDiscordGuildPermission(this string? permissionString, [NotNullWhen(true)] out DiscordGuildPermission? permission)
        {
            permission = default;

            if (ulong.TryParse(permissionString, out var permissionTemp))
            {
                permission = (DiscordGuildPermission)Enum.ToObject(typeof(DiscordGuildPermission), permissionTemp);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Convert a permission <see cref="DiscordGuildPermission" /> into a <see cref="string" />.
        /// </summary>
        /// <param name="permissions">
        ///     The <see cref="DiscordGuildPermission" /> that will be converter into a <see cref="string" />
        ///     .
        /// </param>
        /// <returns>
        ///     The converted <see cref="string" />.
        /// </returns>
        public static string ConvertToString(this DiscordGuildPermission permissions)
        {
            return ((ulong)permissions).ToString();
        }
    }
}