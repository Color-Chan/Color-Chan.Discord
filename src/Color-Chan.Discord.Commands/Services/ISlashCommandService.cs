using System;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Services
{
    public interface ISlashCommandService
    {
        /// <summary>
        ///     Add all interaction commands in an <see cref="Assembly" /> to the <see cref="ISlashCommandService" />.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly" /> where the commands are located.</param>
        /// <seealso cref="Result" />
        /// <seealso cref="SlashCommandAttribute" />
        Task AddInteractionCommandsAsync(Assembly assembly);

        /// <summary>
        ///     Execute a specific command with their dependencies.
        ///     The command will be searched for with its <see cref="SlashCommandAttribute.Name" />.
        /// </summary>
        /// <param name="commandInfo">The command info.</param>
        /// <param name="context">The current <see cref="ISlashCommandContext" /> that will be passed to the command module.</param>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider" /> containing the necessary dependencies for the the
        ///     module of the command.
        /// </param>
        /// <returns>
        ///     The <see cref="Result" /> containing the result of the command execution.
        /// </returns>
        /// <seealso cref="Result" />
        /// <seealso cref="SlashCommandAttribute" />
        Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(ISlashCommandInfo commandInfo, ISlashCommandContext context, IServiceProvider? serviceProvider = null);

        /// <summary>
        ///     Execute a specific command with their dependencies.
        ///     The command will be searched for with its <see cref="SlashCommandAttribute.Name" />.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="context">The current <see cref="ISlashCommandContext" /> that will be passed to the command module.</param>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider" /> containing the necessary dependencies for the the
        ///     module of the command.
        /// </param>
        /// <returns>
        ///     The <see cref="Result" /> containing the result of the command execution.
        /// </returns>
        /// <seealso cref="Result" />
        /// <seealso cref="SlashCommandAttribute" />
        Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(string name, ISlashCommandContext context, IServiceProvider? serviceProvider = null);

        /// <summary>
        ///     Search for a command by its <see cref="SlashCommandAttribute.Name" />.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <returns>
        ///     The <see cref="SlashCommandInfo" /> of the command.
        ///     This method should be executed to run the command.
        /// </returns>
        ISlashCommandInfo? SearchSlashCommand(string name);

        /// <summary>
        ///     Configure the <see cref="ISlashCommandService" />.
        /// </summary>
        /// <param name="options">The configurations.</param>
        /// <returns>
        ///     The <see cref="ISlashCommandService" /> containing the config settings.
        /// </returns>
        SlashCommandConfiguration Configure(SlashCommandConfiguration options);
    }
}