using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Rest.Models;

namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     The <see cref="IComponentBuilder"/> for <see cref="DiscordComponentType.Separator"/>s.
/// </summary>
public class SeparatorComponentBuilder : BaseComponentBuilder<SeparatorComponentBuilder>, IComponentBuilder
{
    private bool? _divider;
    private int? _spacing;

    /// <summary>
    ///     Sets the spacing for the separator component.
    /// </summary>
    /// <param name="spacing">The spacing value.</param>
    /// <returns>The updated <see cref="SeparatorComponentBuilder"/>.</returns>
    public SeparatorComponentBuilder WithSpacing(int spacing)
    {
        _spacing = spacing;
        return this;
    }

    /// <summary>
    ///     Sets whether the separator is a divider.
    /// </summary>
    /// <param name="isDivider">Whether the separator is a divider.</param>
    /// <returns>The updated <see cref="SeparatorComponentBuilder"/>.</returns>
    public SeparatorComponentBuilder AsDivider(bool isDivider)
    {
        _divider = isDivider;
        return this;
    }

    /// <inheritdoc />
    public IDiscordComponent Build()
    {
        return new DiscordComponent
        {
            Id = Id,
            Type = DiscordComponentType.Separator,
            Spacing = _spacing,
            Divider = _divider
        };
    }
}