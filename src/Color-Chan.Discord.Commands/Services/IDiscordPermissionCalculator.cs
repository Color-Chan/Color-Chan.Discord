using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Services;

/// <summary>
///     The permission calculator service that will calculate the correct <see cref="DiscordPermission"/> that the bot has in a <see cref="IDiscordChannel"/>. 
/// </summary>
public interface IDiscordPermissionCalculator
{
    /// <summary>
    ///     Calculates the <see cref="DiscordPermission"/> that the bot has in a <see cref="IDiscordChannel"/>.
    /// </summary>
    /// <param name="guildId">The ID of the <see cref="IDiscordGuild"/> containing the base role <see cref="DiscordPermission"/>.</param>
    /// <param name="channelId">The ID of the <see cref="IDiscordChannel"/> that will be used to get all the <see cref="DiscordPermission"/> overwrites.</param>
    /// <returns>
    ///     The <see cref="Result"/> containing the <see cref="DiscordPermission"/> that the bot has in a given <see cref="IDiscordChannel"/>.
    /// </returns>
    Task<Result<DiscordPermission?>> CalculatePermissionAsync(ulong guildId, ulong channelId);
}