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
    private const int ActionRowMaxButtons = 5;
    private readonly List<IComponentBuilder> _subComponentBuilders = [];
    private IComponentBuilder? _accessory;

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
        if (type == DiscordComponentType.ActionRow && _subComponentBuilders.Count >= ActionRowMaxButtons)
        {
            throw new ArgumentOutOfRangeException(nameof(_subComponentBuilders), $"An action row can not have more then {ActionRowMaxButtons} buttons");
        }

        _subComponentBuilders.Add(subComponentBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a button accessory to the layout component.
    /// </summary>
    /// <param name="buttonAccessory">The button accessory to be added.</param>
    /// <returns>
    ///     The updated <see cref="LayoutComponentBuilder" />.
    /// </returns>
    /// <remarks>
    ///     Only valid for <see cref="DiscordComponentType.Section" />.
    /// </remarks>
    public LayoutComponentBuilder WithAccessory(ButtonComponentBuilder buttonAccessory)
    {
        return InternalWithAccessory(buttonAccessory);
    }

    private LayoutComponentBuilder InternalWithAccessory(IComponentBuilder accessory)
    {
        if (type is not DiscordComponentType.Section)
        {
            throw new ArgumentException("Invalid component type for accessory.", nameof(type));
        }

        _accessory = accessory;
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
            Accessory = _accessory?.Build(),
            ChildComponents = _subComponentBuilders.Select(subComponentBuilder => subComponentBuilder.Build()).ToList()
        };
    }
}