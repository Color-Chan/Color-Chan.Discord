using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models;

namespace Color_Chan.Discord.Rest.Services.Implementations;

/// <inheritdoc />
public class DiscordPermissionCalculator : IDiscordPermissionCalculator
{
    private readonly DiscordTokens _discordTokens;
    private readonly IDiscordRestChannel _restChannel;
    private readonly IDiscordRestGuild _restGuild;

    /// <summary>
    ///     Initializes a new <see cref="DiscordPermissionCalculator" />.
    /// </summary>
    /// <param name="restGuild">The <see cref="IDiscordRestGuild" /> REST client.</param>
    /// <param name="restChannel">The <see cref="IDiscordRestChannel" /> REST client.</param>
    /// <param name="discordTokens">The <see cref="DiscordTokens" /> needed for the application ID.</param>
    public DiscordPermissionCalculator(IDiscordRestGuild restGuild, IDiscordRestChannel restChannel, DiscordTokens discordTokens)
    {
        _restGuild = restGuild;
        _restChannel = restChannel;
        _discordTokens = discordTokens;
    }

    /// <inheritdoc />
    public async Task<Result<DiscordPermission?>> CalculatePermissionAsync(ulong guildId, ulong channelId)
    {
        // Get the guild.
        var guildResult = await _restGuild.GetGuildAsync(guildId).ConfigureAwait(false);

        if (!guildResult.IsSuccessful)
        {
            return Result<DiscordPermission?>.FromError(null, guildResult.ErrorResult ?? new ErrorResult("Failed to get the guild"));
        }

        var guild = guildResult.Entity;

        if (guild is null)
        {
            throw new NullReferenceException("Failed to get the guild");
        }

        // Get the bot user.
        var botMemberResult = await _restGuild.GetGuildMemberAsync(guildId, _discordTokens.ApplicationId);
        if (!botMemberResult.IsSuccessful)
        {
            return Result<DiscordPermission?>.FromError(null, botMemberResult.ErrorResult ?? new ErrorResult("Guild member does not exist"));
        }

        if (botMemberResult.Entity is null)
        {
            throw new NullReferenceException("Failed to get the bot member");
        }

        // Get the bot role permissions.
        var botRoles = guild.Roles.Where(x => botMemberResult.Entity.Roles.Contains(x.Id));
        var rolePerms = botRoles.Aggregate(DiscordPermission.None, (current, botRole) => current | botRole.Permissions);

        // Get the channel.
        IDiscordChannel? channel;
        var channelResult = await _restChannel.GetChannelAsync(channelId).ConfigureAwait(false);

        if (!channelResult.IsSuccessful)
        {
            if (channelResult.ErrorResult?.ErrorMessage != "Missing Access" && channelResult.ErrorResult?.ErrorMessage != "Unknown Channel")
            {
                return Result<DiscordPermission?>.FromError(null, channelResult.ErrorResult ?? new ErrorResult("Failed to get the channel"));
            }

            // Assume it doesn't have access to see the channel.
            rolePerms |= DiscordPermission.ViewChannel;
            channel = new DiscordChannel(new DiscordChannelData
            {
                PermissionOverwrites = new List<DiscordOverwriteData>
                {
                    new()
                    {
                        TargetId = _discordTokens.ApplicationId,
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
            if (permissionOverwrite.TargetId == _discordTokens.ApplicationId || botMemberResult.Entity.Roles.Contains(permissionOverwrite.TargetId))
            {
                rolePerms |= permissionOverwrite.Allow;
                rolePerms &= ~permissionOverwrite.Deny;
            }
        }

        return Result<DiscordPermission?>.FromSuccess(rolePerms);
    }
}