using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Rest.Models;

namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     The <see cref="IComponentBuilder"/> for <see cref="Container"/>s.
/// </summary>
public class ContainerComponentBuilder : BaseLayoutComponentBuilder<ContainerComponentBuilder>, IComponentBuilder
{
    private Color? _accentColor;
    private bool? _spoiler;

    /// <summary>
    ///     Sets whether the container component should be marked as a spoiler.
    /// </summary>
    /// <param name="spoiler">True if the container is a spoiler, false otherwise.</param>
    /// <returns>
    ///     The updated <see cref="ContainerComponentBuilder" />.
    /// </returns>
    public ContainerComponentBuilder WithSpoiler(bool spoiler)
    {
        _spoiler = spoiler;
        return this;
    }

    /// <summary>
    ///     Sets the accent color for the container component.
    /// </summary>
    /// <param name="color">The accent color to be set for the container.</param>
    /// <returns>
    ///     The updated <see cref="ContainerComponentBuilder" />.
    /// </returns>
    public ContainerComponentBuilder WithAccentColor(Color color)
    {
        _accentColor = color;
        return this;
    }

    /// <inheritdoc />
    public override ContainerComponentBuilder WithSubComponent(IComponentBuilder subComponentBuilder)
    {
        SubComponentBuilders.Add(subComponentBuilder);
        return this;
    }

    /// <inheritdoc />
    public override IDiscordComponent Build()
    {
        var subComponents = SubComponentBuilders.Select(subComponentBuilder => subComponentBuilder.Build()).ToList();
        if (subComponents.Any(subComponent =>
                subComponent.Type is not (
                    DiscordComponentType.ActionRow or
                    DiscordComponentType.TextDisplay or
                    DiscordComponentType.Section or
                    DiscordComponentType.MediaGallery or
                    DiscordComponentType.Separator or
                    DiscordComponentType.File)
            ))
        {
            throw new InvalidEnumArgumentException("Container components can only contain ActionRow, TextDisplay, Section, MediaGallery, Separator, or File components.");
        }

        return new DiscordComponent
        {
            Id = Id,
            Type = DiscordComponentType.Container,
            AccentColor = _accentColor,
            Spoiler = _spoiler,
            ChildComponents = subComponents
        };
    }
}