using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Commands.Info
{
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
        IEnumerable<KeyValuePair<string, string>>? Choices { get; init; }
    }
}