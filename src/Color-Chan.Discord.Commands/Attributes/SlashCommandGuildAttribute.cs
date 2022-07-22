using System;

namespace Color_Chan.Discord.Commands.Attributes;

/// <summary>
///     Makes a method available as an guild only slash command.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class SlashCommandGuildAttribute : Attribute
{
    /// <summary>
    ///     Registers a methods as a guild only slash command for a specific guild.
    /// </summary>
    /// <param name="guildId">The id of the guild that will get access to the slash command.</param>
    public SlashCommandGuildAttribute(ulong guildId)
    {
        GuildId = guildId;
    }

    /// <summary>
    ///     The id of the guild that will get access to the slash command.
    /// </summary>
    public ulong GuildId { get; }
}