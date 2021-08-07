using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Results;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Extensions;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements
{
    /// <summary>
    ///     Requires the bot that requested the slash command to have a certain permissions.
    /// </summary>
    /// <example>
    ///     This example requires the bot to have the <see cref="DiscordGuildPermission.BanMembers" /> and <see cref="DiscordGuildPermission.KickMembers" /> permission.
    ///     You can also put the <see cref="SlashCommandRequireBotPermissionAttribute" /> on a method if you only want to have it on a specific command.
    ///     <code language="cs">
    ///     [SlashCommandRequireBotPermission(DiscordGuildPermission.BanMembers | DiscordGuildPermission.KickMembers)]
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
    public class SlashCommandRequireBotPermissionAttribute : SlashCommandRequirementAttribute
    {
        private readonly DiscordGuildPermission _requiredGuildPermission;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRateLimitAttribute" />.
        /// </summary>
        /// <param name="requiredGuildPermission">
        ///     The <see cref="DiscordGuildPermission" /> the bot is required to have for this
        ///     command/command group.
        /// </param>
        public SlashCommandRequireBotPermissionAttribute(DiscordGuildPermission requiredGuildPermission)
        {
            _requiredGuildPermission = requiredGuildPermission;
        }

        /// <inheritdoc />
        public override async Task<Result> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services)
        {
            if (context.GuildId is null)
            {
                return Result.FromError(new SlashCommandRequireBotPermissionErrorResult("Command can not be executed in DMs", default));
            }

            var restGuild = services.GetRequiredService<IDiscordRestGuild>();
            var discordTokens = services.GetRequiredService<DiscordTokens>();
            
            var botMember = await restGuild.GetGuildMemberAsync(context.GuildId.Value, discordTokens.ApplicationId);
            if (!botMember.IsSuccessful)
            {
                return Result.FromError(new SlashCommandRequireBotPermissionErrorResult("Guild member does not exist", default));
            }
            
            if (botMember.Entity!.Permissions is null)
            {
                throw new ArgumentNullException(nameof(botMember.Entity.Permissions), "No permission found");
            }

            if (botMember.Entity.Permissions.Value.HasFlag(_requiredGuildPermission))
            {
                return Result.FromSuccess();
            }

            var botPerms = botMember.Entity!.Permissions.ToList();
            var requiredPerms = _requiredGuildPermission.ToList();
            var missingPerms = new List<DiscordGuildPermission>();

            foreach (var requiredPerm in requiredPerms)
            {
                if (!botPerms.Contains(requiredPerm))
                {
                    missingPerms.Add(requiredPerm);
                }
            }

            return Result.FromError(new SlashCommandRequireBotPermissionErrorResult("Bot did not meet permission requirements", missingPerms));
        }
    }
}