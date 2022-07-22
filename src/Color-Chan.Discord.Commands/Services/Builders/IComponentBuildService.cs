using System;
using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Models.Info;

namespace Color_Chan.Discord.Commands.Services.Builders;

/// <summary>
///     Holds all the methods to build the <see cref="IComponentInfo" />.
/// </summary>
public interface IComponentBuildService
{
    /// <summary>
    ///     Builds all the <see cref="IComponentInfo" />s of an <paramref name="assembly" />.
    /// </summary>
    /// <param name="assembly">The assembly where the components are located.</param>
    /// <returns>
    ///     All the <see cref="IComponentInfo" />s the provided <paramref name="assembly" />.
    /// </returns>
    /// <exception cref="NullReferenceException">Thrown when a valid method did not have a <see cref="ComponentAttribute" />.</exception>
    IEnumerable<IComponentInfo> BuildComponentInfos(Assembly assembly);
}