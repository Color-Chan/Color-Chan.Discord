using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Contexts;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Services
{
    /// <summary>
    ///     Holds all the methods to execute all <see cref="SlashCommandRequirementAttribute" />s for a slash command.
    /// </summary>
    public interface ISlashCommandRequirementService
    {
        /// <summary>
        ///     Executes all <see cref="SlashCommandRequirementAttribute" />s for a <see cref="ISlashCommandInfo" />.
        /// </summary>
        /// <param name="requirements">The requirements that will be executed.</param>
        /// <param name="context">The <see cref="ISlashCommandContext" /> containing the current data for the command.</param>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider" /> containing the dependencies for the
        ///     <see cref="SlashCommandRequirementAttribute" />s.
        /// </param>
        /// <returns>
        ///     The <see cref="Result" /> containing the result of executing the slash command requirements.
        /// </returns>
        Task<Result> ExecuteSlashCommandRequirementsAsync(IEnumerable<SlashCommandRequirementAttribute>? requirements, ISlashCommandContext context, IServiceProvider serviceProvider);
    }
}