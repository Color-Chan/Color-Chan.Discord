using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     Sets a requirement for an interaction command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public abstract class SlashCommandRequirementAttribute : Attribute
    {
        /// <summary>
        ///     Checks if a specific requirement for a command has met.
        /// </summary>
        /// <param name="context">The <see cref="ISlashCommandContext" /> of the command.</param>
        /// <param name="services">The <see cref="IServiceProvider" /> containing all the necessary dependencies for the command.</param>
        /// <returns>
        ///     The result of the slash command requirement.
        /// </returns>
        public abstract Task<Result> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services);
    }
}