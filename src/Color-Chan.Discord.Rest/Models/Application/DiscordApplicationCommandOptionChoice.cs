using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Rest.Models.Application;

public record DiscordApplicationCommandOptionChoice : IDiscordApplicationCommandOptionChoice
{
    public DiscordApplicationCommandOptionChoice(string name, object value)
    {
        Name = name;
        RawValue = value;
    }

    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public object RawValue { get; set; }
}