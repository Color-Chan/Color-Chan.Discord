using System;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Rest.Models;

namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     The <see cref="IComponentBuilder"/> for <see cref="DiscordComponentType.Button"/>s.
/// </summary>
public class ButtonComponentBuilder : BaseComponentBuilder<ButtonComponentBuilder>, IComponentBuilder
{
    internal const int MaxCustomIdLength = 100;
    internal const int MaxLabelLength = 80;
    
    private string? _label;
    private string? _url;
    private string? _customId;
    private ulong? _skuId;
    private DiscordButtonStyle? _style;
    private bool? _disabled;
    private IDiscordEmoji? _emoji;
    
    /// <summary>
    ///     Adds a label to the button component.
    /// </summary>
    /// <param name="label">The label to be added.</param>
    /// <returns>
    ///     The updated <see cref="ButtonComponentBuilder" />.
    /// </returns>
    public ButtonComponentBuilder WithLabel(string label)
    {
        _label = label;
        return this;
    }
    
    /// <summary>
    ///     Adds an emoji to the button component.
    /// </summary>
    /// <param name="emoji">The emoji to be added.</param>
    /// <returns>
    ///     The updated <see cref="ButtonComponentBuilder" />.
    /// </returns>
    public ButtonComponentBuilder WithEmoji(IDiscordEmoji emoji)
    {
        _emoji = emoji;
        return this;
    }
    
    /// <summary>
    ///     Adds a style to the button component.
    /// </summary>
    /// <param name="style">The style of the button.</param>
    /// <returns>
    ///     The updated <see cref="ButtonComponentBuilder" />.
    /// </returns>
    public ButtonComponentBuilder WithStyle(DiscordButtonStyle style)
    {
        _style = style;
        return this;
    }
    
    /// <summary>
    ///     Adds a link button to the component.
    /// </summary>
    /// <param name="disabled">The disabled state of the button.</param>
    /// <returns>
    ///     The updated <see cref="ButtonComponentBuilder" />.
    /// </returns>
    public ButtonComponentBuilder WithDisabled(bool disabled)
    {
        _disabled = disabled;
        return this;
    }
    
    /// <summary>
    ///     Adds a URL to the button component.
    /// </summary>
    /// <param name="url">The URL to be added.</param>
    /// <returns>
    ///     The updated <see cref="ButtonComponentBuilder" />.
    /// </returns>
    public ButtonComponentBuilder WithUrl(string url)
    {
        _url = url;
        return this;
    }
    
    /// <summary>
    ///     Adds a custom ID to the button component.
    /// </summary>
    /// <param name="customId">The custom ID to be added.</param>
    /// <returns>
    ///     The updated <see cref="ButtonComponentBuilder" />.
    /// </returns>
    public ButtonComponentBuilder WithCustomId(string customId)
    {
        if (customId is not null && customId.Length > MaxCustomIdLength)
            throw new ArgumentOutOfRangeException(nameof(customId), $"{nameof(customId)} can not be longer than {MaxCustomIdLength} characters.");
        
        _customId = customId;
        return this;
    }

    /// <summary>
    ///     Adds a SKU ID to the button component.
    /// </summary>
    /// <param name="skuId">The SKU ID to be added.</param>
    /// <returns>
    ///     The updated <see cref="ButtonComponentBuilder" />.
    /// </returns>
    public ButtonComponentBuilder WithSkuId(ulong skuId)
    {
        _skuId = skuId;
        return this;
    }

    /// <inheritdoc />
    public IDiscordComponent Build()
    {
        if(string.IsNullOrEmpty(_label) && _emoji is null) throw new ArgumentException("Label or emoji must be set.");
        if (_style is null) throw new ArgumentException("Style must be set.");
        if (string.IsNullOrEmpty(_url) && string.IsNullOrEmpty(_customId)) throw new ArgumentException("URL or custom ID must be set.");
        
        return new DiscordComponent
        {
            Id = Id,
            Label = _label,
            CustomId = _customId,
            Emoji = _emoji,
            Disabled = _disabled,
            ButtonStyle = _style,
            Type = DiscordComponentType.Button,
            Url = _url,
            SkuId = _skuId
        };
    }
}