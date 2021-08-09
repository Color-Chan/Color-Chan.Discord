using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Contexts;
using Color_Chan.Discord.Commands.Results;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Extensions;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements
{
    /// <summary>
    ///     Requires the bot that requested the slash command to have a certain permissions.
    /// </summary>
    /// <example>
    ///     This example requires the bot to have the <see cref="DiscordPermission.BanMembers" /> and
    ///     <see cref="DiscordPermission.KickMembers" /> permission.
    ///     You can also put the <see cref="SlashCommandRequireBotPermissionAttribute" /> on a method if you only want to have
    ///     it on a specific command.
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
        private readonly DiscordPermission _requiredPermission;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRateLimitAttribute" />.
        /// </summary>
        /// <param name="requiredPermission">
        ///     The <see cref="DiscordPermission" /> the bot is required to have for this
        ///     command/command group.
        /// </param>
        public SlashCommandRequireBotPermissionAttribute(DiscordPermission requiredPermission)
        {
            _requiredPermission = requiredPermission;
        }

        /// <inheritdoc />
        public override async Task<Result> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services)
        {
            if (context.GuildId is null)
            {
                return Result.FromError(new SlashCommandRequireBotPermissionErrorResult("Command can not be executed in DMs", default));
            }

            var restGuild = services.GetRequiredService<IDiscordRestGuild>();

            // Get the guild.
            IDiscordGuild? guild;
            if (context.Guild is not null)
            {
                guild = context.Guild;
            }
            else
            {
                var guildResult = await restGuild.GetGuildAsync(context.GuildId.Value).ConfigureAwait(false);
                guild = guildResult.Entity;
            }

            if (guild is null)
            {
                throw new NullReferenceException("Failed to get guild");
            }

            // Get the bot user.
            var discordTokens = services.GetRequiredService<DiscordTokens>();
            var botMember = await restGuild.GetGuildMemberAsync(context.GuildId.Value, discordTokens.ApplicationId);
            if (!botMember.IsSuccessful)
            {
                return Result.FromError(new SlashCommandRequireBotPermissionErrorResult("Guild member does not exist", default));
            }
            
            // Get the bot role permissions.
            var botRoles = guild.Roles.Where(x => botMember.Entity!.Roles.Contains(x.Id));
            var rolePerms = botRoles.Aggregate(DiscordPermission.None, (current, botRole) => current | botRole.Permissions);

            var restChannel = services.GetRequiredService<IDiscordRestChannel>();
            
            // Get the channel.
            IDiscordChannel? channel;
            if (context.Guild is not null)
            {
                channel = context.Channel;
            }
            else
            {
                var channelResult = await restChannel.GetChannelAsync(context.ChannelId).ConfigureAwait(false);
                channel = channelResult.Entity;
            }

            if (channel is null)
            {
                throw new NullReferenceException("Failed to get channel");
            }

            if (channel.PermissionOverwrites is null)
            {
                throw new NullReferenceException("Missing permission overwrites");
            }

            // Removed the denied permissions from the role perms.
            foreach (var permissionOverwrite in channel.PermissionOverwrites)
            {
                if (permissionOverwrite.TargetId == discordTokens.ApplicationId || botMember.Entity!.Roles.Contains(permissionOverwrite.TargetId))
                {
                    rolePerms |= permissionOverwrite.Allow;
                    rolePerms &= ~permissionOverwrite.Deny;
                }
            }
            
            var missingPerms = _requiredPermission.ToList().Where(requiredPerm => !rolePerms.HasFlag(requiredPerm)).ToList();
            if (missingPerms.Any())
            {
                return Result.FromError(new SlashCommandRequireBotPermissionErrorResult("Bot did not meet permission requirements", missingPerms));
            }
            
            return Result.FromSuccess();
        }
    }
}