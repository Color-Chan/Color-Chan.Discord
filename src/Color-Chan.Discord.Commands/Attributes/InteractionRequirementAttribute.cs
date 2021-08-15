using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     Sets a requirement for an interaction request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public abstract class InteractionRequirementAttribute : Attribute
    {
        /// <summary>
        ///     Checks if a specific requirement for a interaction request has met.
        /// </summary>
        /// <param name="context">The <see cref="IInteractionContext" /> of the interaction request.</param>
        /// <param name="services">The <see cref="IServiceProvider" /> containing all the necessary dependencies for the interaction request.</param>
        /// <returns>
        ///     The result of the interaction request requirement.
        /// </returns>
        public abstract Task<Result> CheckRequirementAsync(IInteractionContext context, IServiceProvider services);
    }
}