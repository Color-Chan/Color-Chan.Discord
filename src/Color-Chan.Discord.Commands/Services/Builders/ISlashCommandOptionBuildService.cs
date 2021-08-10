using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Commands.Services.Builders
{
    /// <summary>
    ///     Holds all methods to build <see cref="ISlashCommandOptionInfo" />s for <see cref="ISlashCommandInfo" />s.
    /// </summary>
    public interface ISlashCommandOptionBuildService
    {
        /// <summary>
        ///     Get all all the data from the parameters with the <see cref="SlashCommandOptionAttribute" />.
        /// </summary>
        /// <param name="command">The <see cref="MethodInfo" /> that will be used to find the parameters.</param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="ISlashCommandOptionInfo" /> containing all the data for the options.
        /// </returns>
        IEnumerable<ISlashCommandOptionInfo> GetCommandOptions(MethodInfo command);

        /// <summary>
        ///     Builds a <see cref="IEnumerable{T}" /> of <see cref="DiscordApplicationCommandOptionData" />s from
        ///     <see cref="ISlashCommandInfo" />s.
        /// </summary>
        /// <param name="commandOptionInfos">
        ///     The <see cref="ISlashCommandInfo" /> that will be converted to
        ///     <see cref="DiscordApplicationCommandOptionData" />.
        /// </param>
        /// <returns>
        ///     The generated <see cref="IEnumerable{T}" /> of <see cref="DiscordApplicationCommandOptionData" />s
        /// </returns>
        /// <exception cref="UpdateSlashCommandException">Thrown when the command exceeds the maximum allowed options.</exception>
        IEnumerable<DiscordApplicationCommandOptionData>? BuildSlashCommandsOptions(IEnumerable<ISlashCommandOptionInfo>? commandOptionInfos);

        /// <summary>
        ///     Builds the choices for a command option.
        /// </summary>
        /// <param name="choicePairs">
        ///     The <see cref="IEnumerable{T}" /> of <see cref="KeyValuePair{TKey,TValue}" />
        ///     where the key is the choice name and the value is the choice value.
        /// </param>
        /// <returns>
        ///     The generated <see cref="IEnumerable{T}" /> of <see cref="DiscordApplicationCommandOptionChoiceData" />.
        /// </returns>
        /// <exception cref="UpdateSlashCommandException">Thrown when the command options exceeds the maximum allowed choices.</exception>
        IEnumerable<DiscordApplicationCommandOptionChoiceData>? BuildChoiceData(IEnumerable<KeyValuePair<string, string>>? choicePairs);
    }
}