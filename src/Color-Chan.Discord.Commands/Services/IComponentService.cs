using System;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Modules;

namespace Color_Chan.Discord.Commands.Services
{
    /// <summary>
    ///     Holds all methods to setup, build and execute interaction components.
    /// </summary>
    public interface IComponentService
    {
        /// <summary>
        ///     Adds all components in an <paramref name="assembly" /> to the component registry.
        /// </summary>
        /// <param name="assembly">The assembly where the <see cref="IComponentInteractionModule" /> are located.</param>
        /// <exception cref="Exception">Thrown when 2 or more component had the same custom id.</exception>
        Task AddComponentsAsync(Assembly assembly);
    }
}