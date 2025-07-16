using System.Collections.Generic;

namespace Color_Chan.Discord.Commands.Models.Contexts;

/// <inheritdoc cref="IComponentContext" />
public class ComponentContext : InteractionContext, IComponentContext
{
    /// <summary>
    ///     The default constructor for the <see cref="ComponentContext"/> class.
    /// </summary>
    public ComponentContext()
    {
        
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ComponentContext"/> class with the specified interaction context and slash command name.
    /// </summary>
    public ComponentContext(IInteractionContext context)
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
    public List<string> Args { get; set; } = new();
}