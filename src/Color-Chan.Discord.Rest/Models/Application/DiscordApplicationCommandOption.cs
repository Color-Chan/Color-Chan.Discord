using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Rest.Models.Application;

/// <inheritdoc cref="IDiscordApplicationCommandOption" />
public record DiscordApplicationCommandOption : IDiscordApplicationCommandOption
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordApplicationCommandOptionData" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordApplicationCommandOptionData" />.</param>
    public DiscordApplicationCommandOption(DiscordApplicationCommandOptionData data)
    {
        Type = data.Type;
        Name = data.Name;
        Description = data.Description;
        IsRequired = data.IsRequired;
        Choices = data.Choices?.Select(choice => new DiscordApplicationCommandOptionChoice(choice.Name, choice.Value));
        SubOptions = data.SubOptions?.Select(subOption => new DiscordApplicationCommandOption(subOption));
        ChanelTypes = data.ChanelTypes;
        MinValue = data.MinValue;
        MaxValue = data.MaxValue;
        Autocomplete = data.Autocomplete;
    }

    /// <inheritdoc />
    public DiscordApplicationCommandOptionType Type { get; set; }

    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public string Description { get; set; }

    /// <inheritdoc />
    public bool? IsRequired { get; set; }

    /// <inheritdoc />
    public IEnumerable<IDiscordApplicationCommandOptionChoice>? Choices { get; set; }

    /// <inheritdoc />
    public IEnumerable<IDiscordApplicationCommandOption>? SubOptions { get; set; }

    /// <inheritdoc />
    public IEnumerable<DiscordChannelType>? ChanelTypes { get; init; }

    /// <inheritdoc />
    public int? MinValue { get; init; }

    /// <inheritdoc />
    public int? MaxValue { get; init; }

    /// <inheritdoc />
    public bool? Autocomplete { get; init; }
}