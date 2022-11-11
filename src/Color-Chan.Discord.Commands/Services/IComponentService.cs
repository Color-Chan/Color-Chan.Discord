using System;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Services;

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

    /// <summary>
    ///     Executes a component interaction.
    /// </summary>
    /// <param name="context">The context of the interaction.</param>
    /// <param name="serviceProvider">The services needed for the interaction.</param>
    /// <returns>
    ///     The <see cref="Result" /> containing the result of the component interaction execution.
    /// </returns>
    Task<Result<IDiscordInteractionResponse>> ExecuteComponentInteractionAsync(IComponentContext context, IServiceProvider serviceProvider);

    /// <summary>
    ///     Executes a component interaction.
    /// </summary>
    /// <param name="componentInfo">The <see cref="IComponentInfo" /> that will be executed.</param>
    /// <param name="context">The context of the interaction.</param>
    /// <param name="serviceProvider">The services needed for the interaction.</param>
    /// <returns>
    ///     The <see cref="Result" /> containing the result of the component interaction execution.
    /// </returns>
    Task<Result<IDiscordInteractionResponse>> ExecuteComponentInteractionAsync(IComponentInfo componentInfo, IComponentContext context, IServiceProvider serviceProvider);

    /// <summary>
    ///     Search for a registered component.
    /// </summary>
    /// <param name="customId">The <see cref="IComponentInfo.CustomId" /> of the component.</param>
    /// <returns>
    ///     A <see cref="IComponentInfo" /> if one was found.
    ///     Null if no <see cref="IComponentInfo" /> was found.
    /// </returns>
    IComponentInfo? SearchComponent(string customId);
}