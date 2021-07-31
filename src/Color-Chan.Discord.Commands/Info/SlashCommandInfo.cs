using System.Collections.Generic;
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
        /// <param name="command">The <see cref="MethodInfo" /> containing the method of the command.</param>
        /// <param name="module">The command module containing the <see cref="CommandMethod" />.</param>
        /// <param name="requirements">
        ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandRequirementAttribute" />s
        ///     containing all the requirements to execute the command.
        /// </param>
        /// <param name="commandOptions">
        ///     The <see cref="IEnumerable{T}" /> of <see cref="ISlashCommandOptionInfo" /> containing the
        ///     options for the slash command.
        /// </param>
        /// <param name="guilds">
        ///     The <see cref="IEnumerable{T}" /> of <see cref="SlashCommandGuildAttribute" /> containing the IDs
        ///     of the guilds that will get access to this slash command.
        /// </param>
        public SlashCommandInfo(string name, string description, MethodInfo command, TypeInfo module, IEnumerable<SlashCommandRequirementAttribute>? requirements = null,
                                IEnumerable<ISlashCommandOptionInfo>? commandOptions = null, IEnumerable<SlashCommandGuildAttribute>? guilds = null)
        {
            CommandName = name;
            Description = description;
            CommandMethod = command;
            Requirements = requirements;
            CommandOptions = commandOptions;
            Guilds = guilds;
            ParentModule = module;
        }

        /// <inheritdoc />
        public string CommandName { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public MethodInfo CommandMethod { get; set; }

        /// <inheritdoc />
        public TypeInfo ParentModule { get; set; }

        /// <inheritdoc />
        public IEnumerable<SlashCommandGuildAttribute>? Guilds { get; set; }

        /// <inheritdoc />
        public IEnumerable<SlashCommandRequirementAttribute>? Requirements { get; set; }

        /// <inheritdoc />
        public IEnumerable<ISlashCommandOptionInfo>? CommandOptions { get; set; }
    }
}