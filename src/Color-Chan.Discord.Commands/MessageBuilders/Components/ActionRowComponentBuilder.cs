using System;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Rest.Models;

namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     The <see cref="IComponentBuilder"/> for <see cref="DiscordComponentType.ActionRow"/>s.
/// </summary>
public class ActionRowComponentBuilder : BaseLayoutComponentBuilder<ActionRowComponentBuilder>, IComponentBuilder
{
    private const int MaxSelectComponentCount = 1;
    private const int MaxButtonComponents = 5;

    /// <inheritdoc />
    public override ActionRowComponentBuilder WithSubComponent(IComponentBuilder subComponentBuilder)
    {
        SubComponentBuilders.Add(subComponentBuilder);
        return this;
    }

    /// <summary>
    ///     Builds the <see cref="IDiscordComponent"/> from the current state of the builder.
    /// </summary>
    /// <returns>
    ///     The build <see cref="IDiscordComponent"/> <see cref="DiscordComponentType.ActionRow"/> component.
    /// </returns>
    public override IDiscordComponent Build()
    {
        var subComponents = SubComponentBuilders.Select(subComponentBuilder => subComponentBuilder.Build()).ToList();
        foreach (var discordComponent in subComponents)
        {
            if (subComponents.Count > MaxSelectComponentCount &&
                discordComponent.Type is DiscordComponentType.StringSelect or DiscordComponentType.UserSelect or DiscordComponentType.RoleSelect or DiscordComponentType.MentionableSelect)
            {
                throw new ArgumentOutOfRangeException(nameof(SubComponentBuilders), $"An action row can not have more than {MaxSelectComponentCount} select component");
            }

            if (subComponents.Count > MaxButtonComponents && discordComponent.Type is DiscordComponentType.Button)
            {
                throw new ArgumentOutOfRangeException(nameof(SubComponentBuilders), $"An action row can not have more than {MaxButtonComponents} buttons");
            }
        }

        var types = subComponents.Select(subComponent => subComponent.Type).Distinct().Count();
        if (types > 1)
        {
            throw new ArgumentException("An action row can not have more than one type of component", nameof(SubComponentBuilders));
        }

        return new DiscordComponent
        {
            Id = Id,
            Type = DiscordComponentType.ActionRow,
            ChildComponents = subComponents,
        };
    }
}