using System;
using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     This class serves as a builder for creating message components.
/// </summary>
public abstract class BaseLayoutComponentBuilder<T> : BaseComponentBuilder<T>, IComponentBuilder where T : BaseLayoutComponentBuilder<T>
{
    /// <summary>
    ///     The subcomponents of the layout component.
    /// </summary>
    protected readonly List<IComponentBuilder> SubComponentBuilders = [];

    /// <summary>
    ///     Adds a subcomponent to the layout component.
    /// </summary>
    /// <param name="subComponentBuilder">The subcomponent builder to be added.</param>
    /// <returns>
    ///     The updated <see cref="BaseLayoutComponentBuilder{T}" />.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the component type is not valid for a subcomponent.</exception>
    public abstract T WithSubComponent(IComponentBuilder subComponentBuilder);

    /// <inheritdoc />
    public abstract IDiscordComponent Build();
}