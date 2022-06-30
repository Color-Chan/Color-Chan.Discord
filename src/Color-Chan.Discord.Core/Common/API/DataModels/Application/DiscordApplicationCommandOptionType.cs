namespace Color_Chan.Discord.Core.Common.API.DataModels.Application;

/// <summary>
///     Represents a discord Application Command Option Type API model.
///     https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-type
/// </summary>
public enum DiscordApplicationCommandOptionType
{
    /// <summary>
    ///     The command option is a sub command.
    /// </summary>
    SubCommand = 1,

    /// <summary>
    ///     The command option is a sub command group.
    /// </summary>
    SubCommandGroup = 2,

    /// <summary>
    ///     A <see cref="string" /> value.
    /// </summary>
    String = 3,

    /// <summary>
    ///     Any integer between -2^53 and 2^53.
    /// </summary>
    Integer = 4,

    /// <summary>
    ///     A <see cref="bool" /> value.
    /// </summary>
    Boolean = 5,

    /// <summary>
    ///     A user mention.
    /// </summary>
    User = 6,

    /// <summary>
    ///     Includes all channel types + categories
    /// </summary>
    Channel = 7,

    /// <summary>
    ///     A role mention.
    /// </summary>
    Role = 8,

    /// <summary>
    ///     Includes users and roles.
    /// </summary>
    Mentionable = 9,

    /// <summary>
    ///     Any double between -2^53 and 2^53.
    /// </summary>
    Number = 10
}