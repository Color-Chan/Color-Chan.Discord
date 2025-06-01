using System.Collections.Generic;

namespace Color_Chan.Discord.Commands.Models.Contexts;

/// <inheritdoc cref="ISlashCommandContext" />
public class SlashCommandContext : InteractionContext, ISlashCommandContext
{
    /// <summary>
    ///     The default constructor for the <see cref="SlashCommandContext"/> class.
    /// </summary>
    public SlashCommandContext()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SlashCommandContext"/> class with the specified interaction context and slash command name.
    /// </summary>
    public SlashCommandContext(IInteractionContext context)
    {
        User = context.User;
        Message = context.Message;
        Member = context.Member;
        GuildId = context.GuildId;
        ChannelId = context.ChannelId;
        ApplicationId = context.ApplicationId;
        Data = context.Data;
        InteractionId = context.InteractionId;
        Token = context.Token;
        Channel = context.Channel;
        Guild = context.Guild;
        Permissions = context.Permissions;
        Entitlements = context.Entitlements;
    }

    /// <inheritdoc />
    public IEnumerable<string> SlashCommandName { get; set; } = null!;
}