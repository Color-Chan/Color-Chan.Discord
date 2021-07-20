﻿using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;

namespace Color_Chan.Discord.Commands.Info
{
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
        ///     The <see cref="MethodInfo" /> containing the method of the command.
        /// </summary>
        public MethodInfo CommandMethod { get; set; }

        /// <summary>
        ///     The command module containing the <see cref="CommandMethod" />.
        /// </summary>
        public TypeInfo ParentModule { get; set; }

        /// <summary>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandGuildAttribute" /> containing the IDs of the guilds that
        ///     will get access to this slash command.
        /// </summary>
        public IEnumerable<SlashCommandGuildAttribute>? Guilds { get; set; }

        /// <summary>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandRequirementAttribute" />s containing all the requirements
        ///     to
        ///     execute the command.
        /// </summary>
        public IEnumerable<SlashCommandRequirementAttribute>? Requirements { get; set; }

        /// <summary>
        ///     The options for the slash command.
        /// </summary>
        public IEnumerable<ISlashCommandOptionInfo>? CommandOptions { get; set; }
    }
}