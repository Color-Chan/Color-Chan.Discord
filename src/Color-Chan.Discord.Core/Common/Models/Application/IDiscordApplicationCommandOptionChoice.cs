namespace Color_Chan.Discord.Core.Common.Models.Application;

/// <summary>
///     Represents a discord Application Command Option Choice Structure API model.
///     Docs:
///     https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-choice-structure
/// </summary>
public interface IDiscordApplicationCommandOptionChoice
{
    /// <summary>
    ///     1-100 character choice name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Value of the choice, up to 100 characters if string.
    /// </summary>
    public object RawValue { get; set; }
}