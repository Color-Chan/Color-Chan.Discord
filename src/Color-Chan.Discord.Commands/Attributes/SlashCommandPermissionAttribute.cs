using System;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     Makes the command available to a specific role or user.
    /// </summary>
    /// <remarks>
    ///     Not compatible with global slash command!
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class SlashCommandPermissionAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandPermissionAttribute" />.
        /// </summary>
        /// <param name="id">The id of the role or user.</param>
        /// <param name="type">Specifies the type that the ID belongs to.</param>
        /// <param name="allow">Whether to allow the user/role to use the command.</param>
        public SlashCommandPermissionAttribute(ulong id, DiscordApplicationCommandPermissionsType type, bool allow = true)
        {
            Id = id;
            Type = type;
            Allow = allow;
        }

        /// <summary>
        ///     The id of the role or user..
        /// </summary>
        public ulong Id { get; init; }

        /// <summary>
        ///     Specifies the type that the ID belongs to.
        /// </summary>
        public DiscordApplicationCommandPermissionsType Type { get; init; }

        /// <summary>
        ///     The id of the role or user.
        /// </summary>
        public bool Allow { get; init; }
    }
}