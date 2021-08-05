﻿using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;

namespace Color_Chan.Discord.Commands.Info
{
    /// <inheritdoc />
    public class SlashCommandInfo : ISlashCommandInfo
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandInfo" />.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="description">The description of the command.</param>
        /// <param name="defaultPermission">Whether the command is enabled by default when the app is added to a guild.</param>
        /// <param name="command">The <see cref="MethodInfo" /> containing the method of the command.</param>
        /// <param name="module">The command module containing the <see cref="CommandMethod" />.</param>
        public SlashCommandInfo(string name, string description, bool defaultPermission, MethodInfo command, TypeInfo module)
        {
            CommandName = name;
            Description = description;
            DefaultPermission = defaultPermission;
            CommandMethod = command;
            ParentModule = module;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandInfo" />.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="description">The description of the command.</param>
        /// <param name="defaultPermission">Whether the command is enabled by default when the app is added to a guild.</param>
        /// <param name="module">The command module containing the <see cref="CommandMethod" />.</param>
        public SlashCommandInfo(string name, string description, bool defaultPermission, TypeInfo module)
        {
            CommandName = name;
            Description = description;
            DefaultPermission = defaultPermission;
            ParentModule = module;
        }

        /// <inheritdoc />
        public string CommandName { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public bool DefaultPermission { get; set; }

        /// <inheritdoc />
        public MethodInfo? CommandMethod { get; set; }

        /// <inheritdoc />
        public TypeInfo ParentModule { get; set; }

        /// <inheritdoc />
        public IEnumerable<SlashCommandGuildAttribute>? Guilds { get; set; }

        /// <inheritdoc />
        public IEnumerable<SlashCommandRequirementAttribute>? Requirements { get; set; }
        
        /// <inheritdoc />
        public IEnumerable<SlashCommandPermissionAttribute>? Permissions { get; set; }

        /// <inheritdoc />
        public List<ISlashCommandOptionInfo>? CommandOptions { get; set; }
    }
}