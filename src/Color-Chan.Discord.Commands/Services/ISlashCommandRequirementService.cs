using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Core;

namespace Color_Chan.Discord.Commands.Services
{
    /// <summary>
    ///     Holds all the methods to execute all <see cref="SlashCommandRequirementAttribute"/>s for a slash command.
    /// </summary>
    public interface ISlashCommandRequirementService
    {
        /// <summary>
        ///     Executes all <see cref="SlashCommandRequirementAttribute" />s for a <see cref="ISlashCommandInfo" />.
        /// </summary>
        /// <param name="commandInfo">
        ///     The <see cref="ISlashCommandInfo" /> containing the
        ///     <see cref="SlashCommandRequirementAttribute" />s.
        /// </param>
        /// <param name="context">The <see cref="ISlashCommandContext" /> containing the current data for the command.</param>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider" /> containing the dependencies for the
        ///     <see cref="SlashCommandRequirementAttribute" />s.
        /// </param>
        /// <returns>
        ///     todo:
        /// </returns>
        Task<List<string>> ExecuteSlashCommandRequirementsAsync(ISlashCommandInfo commandInfo, ISlashCommandContext context, IServiceProvider serviceProvider);
    }
}