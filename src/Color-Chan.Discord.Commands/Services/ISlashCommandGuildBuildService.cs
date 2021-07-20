using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;

namespace Color_Chan.Discord.Commands.Services
{
    public interface ISlashCommandGuildBuildService
    {
        /// <summary>
        ///     Get a <see cref="IEnumerable{T}" /> of <see cref="SlashCommandGuildAttribute" /> for a specific
        ///     <paramref name="command" />.
        /// </summary>
        /// <param name="command">The command method that will be used to find the <see cref="SlashCommandGuildAttribute" />s.</param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="SlashCommandGuildAttribute" /> for the given
        ///     <paramref name="command" />.
        /// </returns>
        IEnumerable<SlashCommandGuildAttribute> GetCommandGuilds(MethodInfo command);
    }
}