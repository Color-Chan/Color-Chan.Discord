using System;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Rest.Models;

namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     The <see cref="IComponentBuilder"/> for <see cref="DiscordComponentType.TextDisplay"/>s.
/// </summary>
public class TextDisplayComponentBuilder : BaseComponentBuilder<TextDisplayComponentBuilder>, IComponentBuilder
{
    private string? _content;
    
    /// <summary>
    ///     Adds a content to the text display component.
    /// </summary>
    /// <param name="content">The content to be added.</param>
    /// <returns>
    ///     The updated <see cref="TextDisplayComponentBuilder" />.
    /// </returns>
    public TextDisplayComponentBuilder WithContent(string content)
    {
        _content = content;
        return this;
    }
    
    /// <inheritdoc />
    public IDiscordComponent Build()
    {
        ArgumentNullException.ThrowIfNull(_content);
        
        return new DiscordComponent
        {
            Id = Id,
            Content = _content
        };
    }
}