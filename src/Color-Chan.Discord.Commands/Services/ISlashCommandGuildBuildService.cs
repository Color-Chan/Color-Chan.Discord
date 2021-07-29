using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Info;

namespace Color_Chan.Discord.Commands.Services
{
    /// <summary>
    ///     Holds all the methods to build <see cref="ISlashCommandInfo" /> for guilds only.
    /// </summary>
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