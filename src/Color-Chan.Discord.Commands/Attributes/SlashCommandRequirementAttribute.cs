using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Core;

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
        ///     Whether or not the requirement has been met.
        /// </returns>
        public abstract Task<SlashCommandRequirementResult> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services);
    }
}