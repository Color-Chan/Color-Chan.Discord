using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.Params;

namespace Color_Chan.Discord.Commands.Services
{
    public interface ISlashCommandBuildService
    {
        /// <summary>
        ///     Builds all commands in a specific <paramref name="assembly" /> and stores them in a
        ///     <see cref="IReadOnlyList{T}" /> of <see cref="KeyValuePair{T,U}" /> of <see cref="string" />,
        ///     <see cref="ISlashCommandInfo" />.
        /// </summary>
        /// <param name="assembly">
        ///     The <see cref="Assembly" /> where the <see cref="ISlashCommandBuildService" /> will search for commands.
        /// </param>
        /// <returns>
        ///     A <see cref="IReadOnlyList{T}" /> of <see cref="KeyValuePair{T,U}" /> of <see cref="string" />,
        ///     <see cref="ISlashCommandInfo" />.
        ///     The key <see cref="string" /> contains the command name.
        ///     And the value <see cref="ISlashCommandInfo" /> contains the commands information to execute it.
        /// </returns>
        IReadOnlyList<KeyValuePair<string, ISlashCommandInfo>> BuildSlashCommandInfos(Assembly assembly);

        /// <summary>
        ///     Get all the interaction command modules.
        ///     These modules need to inherit <see cref="ISlashCommandModuleBase" /> so they can be found by the
        ///     <see cref="ISlashCommandBuildService" />.
        /// </summary>
        /// <param name="assembly">
        ///     The <see cref="Assembly" /> where the <see cref="ISlashCommandBuildService" /> will search
        ///     for the modules.
        /// </param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> of <see cref="TypeInfo" />s that inherits
        ///     <see cref="ISlashCommandModuleBase" />.
        /// </returns>
        IEnumerable<TypeInfo> GetSlashCommandModules(Assembly assembly);

        /// <summary>
        ///     Builds the slash command parameters so it can be send to Discord's API.
        /// </summary>
        /// <param name="commandInfos">The <see cref="ISlashCommandInfo" />s.</param>
        /// <returns>
        ///     The build slash command parameters.
        /// </returns>
        IEnumerable<DiscordCreateApplicationCommand> BuildSlashCommandsParams(IEnumerable<ISlashCommandInfo> commandInfos);
    }
}