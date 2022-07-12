using System;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Results;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Extensions;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;

/// <summary>
///     Requires the bot that requested the interaction to have a certain permissions.
/// </summary>
/// <example>
///     This example requires the bot to have the <see cref="DiscordPermission.BanMembers" /> and
///     <see cref="DiscordPermission.KickMembers" /> permission.
///     You can also put the <see cref="RequireBotPermissionAttribute" /> on a method if you only want to have
///     it on a specific command.
///     <code language="cs">
///     [RequireBotPermission(DiscordGuildPermission.BanMembers | DiscordGuildPermission.KickMembers)]
///     public class PongCommands : SlashCommandModule
///     {
///         [SlashCommand("ping", "Ping Pong!")]
///         public Task&lt;IDiscordInteractionResponse&gt; PongAsync()
///         {
///             // Command code...
///         }
///     }
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequireBotPermissionAttribute : InteractionRequirementAttribute
{
    private readonly DiscordPermission _requiredPermission;

    /// <summary>
    ///     Initializes a new instance of <see cref="UserRateLimitAttribute" />.
    /// </summary>
    /// <param name="requiredPermission">
    ///     The <see cref="DiscordPermission" /> the bot is required to have for this
    ///     command/command group.
    /// </param>
    public RequireBotPermissionAttribute(DiscordPermission requiredPermission)
    {
        _requiredPermission = requiredPermission;
    }

    /// <inheritdoc />
    public override Task<Result> CheckRequirementAsync(IInteractionContext context, IServiceProvider services)
    {
        if (context.Permissions is null)
        {
            return Task.FromResult(Result.FromError(new RequireBotPermissionErrorResult("Interaction can not be executed in DMs", default)));
        }

        // Admin overrides any potential permission overwrites.
        if ((context.Permissions & DiscordPermission.Administrator) == DiscordPermission.Administrator)
        {
            return Task.FromResult(Result.FromSuccess());
        }

        if ((context.Permissions & _requiredPermission) == _requiredPermission)
        {
            return Task.FromResult(Result.FromSuccess());
        }

        var missingPerms = _requiredPermission.ToList().Where(requiredPerm => (context.Permissions & requiredPerm) != requiredPerm).ToList();
        return Task.FromResult(missingPerms.Any() 
                                   ? Result.FromError(new RequireBotPermissionErrorResult("Bot did not meet permission requirements", missingPerms)) 
                                   : Result.FromSuccess());
    }
}