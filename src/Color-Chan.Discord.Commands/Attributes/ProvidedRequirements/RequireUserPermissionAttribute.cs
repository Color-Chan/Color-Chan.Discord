using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Results;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Extensions;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements
{
    /// <summary>
    ///     Requires the user that requested the interaction to have a certain permissions.
    /// </summary>
    /// <example>
    ///     This example limits all the slash commands in the PongCommands slash command module to users that have the
    ///     <see cref="DiscordPermission.BanMembers" /> and <see cref="DiscordPermission.KickMembers" /> permission.
    ///     You can also put the <see cref="RequireUserPermissionAttribute" /> on a method if you only want to have
    ///     it on a specific command.
    ///     <code language="cs">
    ///     [RequireUserPermission(DiscordGuildPermission.BanMembers | DiscordGuildPermission.KickMembers)]
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
    public class RequireUserPermissionAttribute : InteractionRequirementAttribute
    {
        private readonly DiscordPermission _requiredPermission;

        /// <summary>
        ///     Initializes a new instance of <see cref="UserRateLimitAttribute" />.
        /// </summary>
        /// <param name="requiredPermission">
        ///     The <see cref="DiscordPermission" /> the user is required to have for this
        ///     command/command group.
        /// </param>
        public RequireUserPermissionAttribute(DiscordPermission requiredPermission)
        {
            _requiredPermission = requiredPermission;
        }

        /// <inheritdoc />
        public override Task<Result> CheckRequirementAsync(IInteractionContext context, IServiceProvider services)
        {
            if (context.Member is null)
            {
                return Task.FromResult(Result.FromError(new RequireUserPermissionErrorResult("Command can not be executed in DMs", default)));
            }

            if (context.Member.Permissions is null)
            {
                throw new ArgumentNullException(nameof(context.Member.Permissions), "No permission found");
            }

            if (context.Member.Permissions.Value.HasFlag(_requiredPermission))
            {
                return Task.FromResult(Result.FromSuccess());
            }

            var userPerms = context.Member.Permissions.ToList();
            var requiredPerms = _requiredPermission.ToList();
            var missingPerms = new List<DiscordPermission>();

            foreach (var requiredPerm in requiredPerms)
            {
                if (!userPerms.Contains(requiredPerm))
                {
                    missingPerms.Add(requiredPerm);
                }
            }

            return Task.FromResult(Result.FromError(new RequireUserPermissionErrorResult("User did not meet permission requirements", missingPerms)));
        }
    }
}