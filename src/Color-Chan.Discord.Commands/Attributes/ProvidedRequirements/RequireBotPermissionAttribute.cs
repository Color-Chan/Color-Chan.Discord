using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Results;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Extensions;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements
{
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
        private DiscordPermission _requiredPermission;

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
        public override async Task<Result> CheckRequirementAsync(IInteractionContext context, IServiceProvider services)
        {
            if (context.GuildId is null)
            {
                return Result.FromError(new RequireBotPermissionErrorResult("Interaction can not be executed in DMs", default));
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

                if (!guildResult.IsSuccessful)
                {
                    return Result.FromError(guildResult.ErrorResult ?? new ErrorResult("Failed to get the guild"));
                }

                guild = guildResult.Entity;
            }

            if (guild is null)
            {
                throw new NullReferenceException("Failed to get the guild");
            }

            // Get the bot user.
            var discordTokens = services.GetRequiredService<DiscordTokens>();
            var botMemberResult = await restGuild.GetGuildMemberAsync(context.GuildId.Value, discordTokens.ApplicationId);
            if (!botMemberResult.IsSuccessful)
            {
                return Result.FromError(botMemberResult.ErrorResult ?? new RequireBotPermissionErrorResult("Guild member does not exist", default));
            }

            if (botMemberResult.Entity is null)
            {
                throw new NullReferenceException("Failed to get the bot member");
            }

            // Get the bot role permissions.
            var botRoles = guild.Roles.Where(x => botMemberResult.Entity.Roles.Contains(x.Id));
            var rolePerms = botRoles.Aggregate(DiscordPermission.None, (current, botRole) => current | botRole.Permissions);

            // Admin overrides any potential permission overwrites.
            if ((rolePerms & DiscordPermission.Administrator) == DiscordPermission.Administrator)
            {
                return Result.FromSuccess();
            }

            // Only try to deny any channel perms if there are any required.
            if (_requiredPermission.HasChannelPermissions())
            {
                // Get the channel.
                IDiscordChannel? channel;
                if (context.Channel is not null)
                {
                    channel = context.Channel;
                }
                else
                {
                    var restChannel = services.GetRequiredService<IDiscordRestChannel>();
                    var channelResult = await restChannel.GetChannelAsync(context.ChannelId).ConfigureAwait(false);

                    if (!channelResult.IsSuccessful)
                    {
                        if (channelResult.ErrorResult?.ErrorMessage != "Missing Access" && channelResult.ErrorResult?.ErrorMessage != "Unknown Channel")
                        {
                            return Result.FromError(channelResult.ErrorResult ?? new ErrorResult("Failed to get the channel"));
                        }

                        // Assume it doesn't have access to see the channel.
                        _requiredPermission |= DiscordPermission.ViewChannel;
                        channel = new DiscordChannel(new DiscordChannelData
                        {
                            PermissionOverwrites = new List<DiscordOverwriteData>
                            {
                                new()
                                {
                                    TargetId = discordTokens.ApplicationId,
                                    TargetType = DiscordPermissionTargetType.User,
                                    Deny = DiscordPermission.ViewChannel | DiscordPermission.SendMessages,
                                    Allow = default
                                }
                            }
                        });
                    }
                    else
                    {
                        channel = channelResult.Entity;
                    }
                }

                if (channel is null)
                {
                    throw new NullReferenceException("Failed to get the channel");
                }

                if (channel.PermissionOverwrites is null)
                {
                    throw new NullReferenceException("Missing permission overwrites");
                }

                // Apply the `everyone` role permission overwrites for this channel. This needs to be done first to avoid issues with the other overwrites.
                var everyoneOverwrite = channel.PermissionOverwrites.FirstOrDefault(x => x.TargetId == guild.Roles.FirstOrDefault(z => z.Name == "@everyone")?.Id);
                if (everyoneOverwrite is not null)
                {
                    rolePerms |= everyoneOverwrite.Allow;
                    rolePerms &= ~everyoneOverwrite.Deny;
                }

                // Apply permission overwrites for the bot for this channel.
                foreach (var permissionOverwrite in channel.PermissionOverwrites)
                {
                    if (permissionOverwrite.TargetId == discordTokens.ApplicationId || botMemberResult.Entity.Roles.Contains(permissionOverwrite.TargetId))
                    {
                        rolePerms |= permissionOverwrite.Allow;
                        rolePerms &= ~permissionOverwrite.Deny;
                    }
                }
            }

            if ((rolePerms & _requiredPermission) == _requiredPermission)
            {
                return Result.FromSuccess();
            }

            var missingPerms = _requiredPermission.ToList().Where(requiredPerm => (rolePerms & requiredPerm) != requiredPerm).ToList();
            if (missingPerms.Any())
            {
                return Result.FromError(new RequireBotPermissionErrorResult("Bot did not meet permission requirements", missingPerms));
            }

            return Result.FromSuccess();
        }
    }
}