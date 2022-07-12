using Color_Chan.Discord.Core.Common.API.DataModels.Select;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Select;

namespace Color_Chan.Discord.Rest.Models.Select;

/// <inheritdoc cref="IDiscordSelectOption" />
public record DiscordSelectOption : IDiscordSelectOption
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordSelectOption" />
    /// </summary>
    public DiscordSelectOption(DiscordSelectOptionData data)
    {
        Label = data.Label;
        Value = data.Value;
        Description = data.Description;
        Emoji = data.Emoji is not null ? new DiscordEmoji(data.Emoji) : null;
        Default = data.Default;
    }

    /// <summary>
    ///     Initializes a new <see cref="DiscordSelectOption" />
    /// </summary>
    /// <param name="label">The label for the select option.</param>
    /// <param name="value">The actual value for the select option.</param>
    public DiscordSelectOption(string label, string value)
    {
        Label = label;
        Value = value;
    }

    /// <inheritdoc />
    public string Label { get; init; }

    /// <inheritdoc />
    public string Value { get; init; }

    /// <inheritdoc />
    public string? Description { get; init; }

    /// <inheritdoc />
    public IDiscordEmoji? Emoji { get; init; }

    /// <inheritdoc />
    public bool? Default { get; init; }

    /// <inheritdoc />
    public DiscordSelectOptionData ToDataModel()
    {
        return new DiscordSelectOptionData
        {
            Label = Label,
            Value = Value,
            Description = Description,
            Emoji = Emoji?.ToDataModel(),
            Default = Default
        };
    }
}