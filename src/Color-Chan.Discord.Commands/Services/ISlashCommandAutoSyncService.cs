using System.Collections.Generic;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Services;

/// <summary>
///     Hold all the methods to sync the local slash command to Discords api.
/// </summary>
public interface ISlashCommandAutoSyncService
{
    /// <summary>
    ///     Updates the slash commands for the currently signed in application.
    /// </summary>
    /// <param name="commandInfos">The commandInfos that will be used to update slash commands.</param>
    /// <returns>
    ///     The <see cref="Result" /> with the results of the update process.
    /// </returns>
    Task<Result> UpdateApplicationCommandsAsync(IEnumerable<ISlashCommandInfo> commandInfos);
}