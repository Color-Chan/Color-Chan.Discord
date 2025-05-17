using System;
using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Rest.Models;

namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     This class serves as a builder for creating message components.
/// </summary>
public class LayoutComponentBuilder(DiscordComponentType type) : IComponentBuilder
{
    private readonly List<IComponentBuilder> _subComponentBuilders = [];

    /// <summary>
    ///     Adds a subcomponent to the layout component.
    /// </summary>
    /// <param name="subComponentBuilder">The subcomponent builder to be added.</param>
    /// <returns>
    ///     The updated <see cref="LayoutComponentBuilder" />.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the component type is not valid for a subcomponent.</exception>
    public LayoutComponentBuilder WithSubComponent(IComponentBuilder subComponentBuilder)
    {
        if (type is (DiscordComponentType.ActionRow or DiscordComponentType.Section or DiscordComponentType.Container))
        {
            throw new ArgumentException("Invalid component type for a sub component builder.", nameof(type));
        }

        _subComponentBuilders.Add(subComponentBuilder);
        return this;
    }

    /// <inheritdoc />
    public IDiscordComponent Build()
    {
        if (type is not (DiscordComponentType.ActionRow or DiscordComponentType.Section or DiscordComponentType.Separator or DiscordComponentType.Container))
        {
            throw new ArgumentException("Invalid component type for layout component builder.", nameof(type));
        }

        return new DiscordComponent
        {
            Type = type,
            ChildComponents = _subComponentBuilders.Select(subComponentBuilder => subComponentBuilder.Build()).ToList()
        };
    }
}