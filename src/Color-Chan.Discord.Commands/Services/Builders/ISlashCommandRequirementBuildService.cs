using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;

namespace Color_Chan.Discord.Commands.Services.Builders;

/// <summary>
///     Holds all the methods to build <see cref="InteractionRequirementAttribute" /> for the slash commands.
/// </summary>
public interface ISlashCommandRequirementBuildService
{
    /// <summary>
    ///     Get a <see cref="IEnumerable{T}" /> of <see cref="InteractionRequirementAttribute" /> for a specific
    ///     <paramref name="command" />.
    /// </summary>
    /// <param name="command">
    ///     The command method that will be used to find the <see cref="InteractionRequirementAttribute" />
    ///     s.
    /// </param>
    /// <returns>
    ///     A <see cref="IEnumerable{T}" /> of <see cref="InteractionRequirementAttribute" /> for the given
    ///     <paramref name="command" />.
    /// </returns>
    IEnumerable<InteractionRequirementAttribute> GetCommandRequirements(MethodInfo command);
}