using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Commands.Models.Info
{
    /// <summary>
    ///     Contains all the information about a slash command option.
    /// </summary>
    public interface ISlashCommandOptionInfo
    {
        /// <summary>
        ///     The name of the option..
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        ///     The description of the option.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        ///     The type of the parameter.
        /// </summary>
        public DiscordApplicationCommandOptionType Type { get; init; }

        /// <summary>
        ///     Whether or not the option is required.
        /// </summary>
        public bool? IsRequired { get; init; }

        /// <summary>
        ///     A list of <see cref="KeyValuePair{TKey,TValue}" /> where each key is a choice name, and the value is the raw value
        ///     of the choice.
        /// </summary>
        IEnumerable<KeyValuePair<string, object>>? Choices { get; init; }

        /// <summary>
        ///     The <see cref="MethodInfo" /> containing the method of the sub command.
        /// </summary>
        /// <remarks>
        ///     null when the option is not a sub command.
        /// </remarks>
        public MethodInfo? CommandMethod { get; set; }

        /// <summary>
        ///     The command module containing the <see cref="CommandMethod" />.
        /// </summary>
        /// <remarks>
        ///     null when the option is not a sub command.
        /// </remarks>
        public TypeInfo? ParentModule { get; set; }

        /// <summary>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandGuildAttribute" /> containing the IDs of the guilds that
        ///     will get access to this slash command.
        /// </summary>
        /// <remarks>
        ///     null when the option is not a sub command, or when it is a sub command but the command is global.
        /// </remarks>
        public IEnumerable<SlashCommandGuildAttribute>? Guilds { get; set; }

        /// <summary>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="InteractionRequirementAttribute" />s containing all the requirements
        ///     to
        ///     execute the command.
        /// </summary>
        /// <remarks>
        ///     null when the option is not a sub command, or when the sub command doesn't have any requirements.
        /// </remarks>
        public IEnumerable<InteractionRequirementAttribute>? Requirements { get; set; }

        /// <summary>
        ///     The options for the slash command.
        /// </summary>
        /// <remarks>
        ///     null when the option is not a sub command, or when the sub command doesn't have any options.
        /// </remarks>
        public List<ISlashCommandOptionInfo>? CommandOptions { get; set; }
    }
}