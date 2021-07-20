using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Info;

namespace Color_Chan.Discord.Commands.Services
{
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
    }
}