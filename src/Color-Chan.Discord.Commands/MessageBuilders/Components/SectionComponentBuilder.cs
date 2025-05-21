using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Rest.Models;

namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     The <see cref="IComponentBuilder"/> for <see cref="DiscordComponentType.Section"/>s.
/// </summary>
public class SectionComponentBuilder : BaseLayoutComponentBuilder<SectionComponentBuilder>, IComponentBuilder
{
    private IComponentBuilder? _accessory;

    /// <inheritdoc />
    public override SectionComponentBuilder WithSubComponent(IComponentBuilder subComponentBuilder)
    {
        SubComponentBuilders.Add(subComponentBuilder);
        return this;
    }
    
    /// <summary>
    ///     Adds a button accessory to the layout component.
    /// </summary>
    /// <param name="buttonAccessory">The button accessory to be added.</param>
    /// <returns>
    ///     The updated <see cref="SectionComponentBuilder" />.
    /// </returns>
    /// <remarks>
    ///     Only valid for <see cref="DiscordComponentType.Section" />.
    /// </remarks>
    public SectionComponentBuilder WithAccessory(ButtonComponentBuilder buttonAccessory)
    {
        _accessory = buttonAccessory;
        return this;
    }

    /// <summary>
    ///     Builds the <see cref="IDiscordComponent"/> from the current state of the builder.
    /// </summary>
    /// <returns>
    ///     The build <see cref="IDiscordComponent"/> <see cref="DiscordComponentType.Section"/> component.
    /// </returns>
    public override IDiscordComponent Build()
    {
        return new DiscordComponent
        {
            Id = Id,
            Type = DiscordComponentType.Section,
            Accessory = _accessory?.Build(),
            ChildComponents = SubComponentBuilders.Select(subComponentBuilder => subComponentBuilder.Build()).ToList()
        };
    }
}