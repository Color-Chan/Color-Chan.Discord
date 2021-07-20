using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;

namespace Color_Chan.Discord.Commands.Services
{
    public interface ISlashCommandRequirementBuildService
    {
        /// <summary>
        ///     Get a <see cref="IEnumerable{T}" /> of <see cref="SlashCommandRequirementAttribute" /> for a specific
        ///     <paramref name="command" />.
        /// </summary>
        /// <param name="command">
        ///     The command method that will be used to find the <see cref="SlashCommandRequirementAttribute" />
        ///     s.
        /// </param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandRequirementAttribute" /> for the given
        ///     <paramref name="command" />.
        /// </returns>
        IEnumerable<SlashCommandRequirementAttribute> GetCommandRequirements(MethodInfo command);
    }
}