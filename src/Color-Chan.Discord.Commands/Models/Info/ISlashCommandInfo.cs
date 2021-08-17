using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;

namespace Color_Chan.Discord.Commands.Models.Info
{
    /// <summary>
    ///     Contains all the information about a slash command.
    /// </summary>
    public interface ISlashCommandInfo
    {
        /// <summary>
        ///     The name of the command.
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        ///     The description of the command.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Whether the command is enabled by default when the app is added to a guild.
        /// </summary>
        public bool DefaultPermission { get; set; }

        /// <summary>
        ///     The <see cref="MethodInfo" /> containing the method of the command.
        /// </summary>
        /// <remarks>
        ///     null when the command is a command group.
        /// </remarks>
        public MethodInfo? CommandMethod { get; set; }

        /// <summary>
        ///     The command module containing the <see cref="CommandMethod" />.
        /// </summary>
        public TypeInfo ParentModule { get; set; }

        /// <summary>
        ///     Whether or not the command should be automatically acknowledge to prevent the token
        ///     from turning inactive after 3 seconds.
        /// </summary>
        public bool Acknowledge { get; }

        /// <summary>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandGuildAttribute" /> containing the IDs of the guilds that
        ///     will get access to this slash command.
        /// </summary>
        public IEnumerable<SlashCommandGuildAttribute>? Guilds { get; set; }

        /// <summary>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="InteractionRequirementAttribute" />s containing all the requirements
        ///     to execute the command.
        /// </summary>
        public IEnumerable<InteractionRequirementAttribute>? Requirements { get; set; }

        /// <summary>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandPermissionAttribute" />s containing the permission data
        ///     for this command.
        /// </summary>
        /// <remarks>
        ///     Always null on global slash command.
        /// </remarks>
        public IEnumerable<SlashCommandPermissionAttribute>? Permissions { get; set; }

        /// <summary>
        ///     The options for the slash command.
        /// </summary>
        public List<ISlashCommandOptionInfo>? CommandOptions { get; set; }
    }
}