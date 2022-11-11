using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Rest.Models.Application;

/// <inheritdoc cref="IDiscordApplicationCommandOptionChoice" />
public record DiscordApplicationCommandOptionChoice(string Name, object RawValue) : IDiscordApplicationCommandOptionChoice
{
    /// <inheritdoc />
    public string Name { get; set; } = Name;

    /// <inheritdoc />
    public object RawValue { get; set; } = RawValue;
}