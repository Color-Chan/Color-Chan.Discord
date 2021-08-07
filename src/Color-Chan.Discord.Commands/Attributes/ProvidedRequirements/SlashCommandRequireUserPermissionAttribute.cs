using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Results;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements
{
    /// <summary>
    ///     Requires the user that requested the slash command to have a certain permissions.
    /// </summary>
    /// <example>
    ///     This example limits all the slash commands in the PongCommands slash command module to users that have the
    ///     <see cref="DiscordGuildPermission.BanMembers" /> and <see cref="DiscordGuildPermission.KickMembers" /> permission.
    ///     You can also put the <see cref="SlashCommandRequireUserPermissionAttribute" /> on a method if you only want to have it ona
    ///     specific command.
    ///     <code language="cs">
    ///     [SlashCommandRequireUserPermission(DiscordGuildPermission.BanMembers | DiscordGuildPermission.KickMembers)]
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
    public class SlashCommandRequireUserPermissionAttribute : SlashCommandRequirementAttribute
    {
        private readonly DiscordGuildPermission _requiredGuildPermission;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRateLimitAttribute" />.
        /// </summary>
        /// <param name="requiredGuildPermission">
        ///     The <see cref="DiscordGuildPermission" /> the user is required to have for this
        ///     command/command group.
        /// </param>
        public SlashCommandRequireUserPermissionAttribute(DiscordGuildPermission requiredGuildPermission)
        {
            _requiredGuildPermission = requiredGuildPermission;
        }

        /// <inheritdoc />
        public override Task<Result> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services)
        {
            if (context.Member is null)
            {
                return Task.FromResult(Result.FromError(new SlashCommandRequireUserPermissionErrorResult("Command can not be executed in DMs", _requiredGuildPermission)));
            }

            if (context.Member.Permissions is null)
            {
                throw new ArgumentNullException(nameof(context.Member.Permissions), "No permission found");
            }

            if (_requiredGuildPermission.HasFlag(context.Member.Permissions))
            {
                return Task.FromResult(Result.FromSuccess());
            }

            return Task.FromResult(Result.FromError(new SlashCommandRequireUserPermissionErrorResult("User did not need permission requirements", _requiredGuildPermission)));
        }
    }
}