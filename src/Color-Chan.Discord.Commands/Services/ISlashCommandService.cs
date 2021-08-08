using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Contexts;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Services
{
    /// <summary>
    ///     Holds all methods to setup, build and execute slash commands.
    /// </summary>
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
        /// <param name="commandMethod">The command method that will be executed.</param>
        /// <param name="options">The command options of the command.</param>
        /// <param name="requirements">The requirements before the command can be executed.</param>
        /// <param name="context">The current <see cref="ISlashCommandContext" /> that will be passed to the command module.</param>
        /// <param name="suppliedOptions">The options used by the user.</param>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider" /> containing the necessary dependencies for the the
        ///     module of the command.
        /// </param>
        /// <returns>
        ///     The <see cref="Result" /> containing the result of the command execution.
        /// </returns>
        /// <seealso cref="Result" />
        /// <seealso cref="SlashCommandAttribute" />
        Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(MethodInfo commandMethod, IEnumerable<ISlashCommandOptionInfo>? options,
                                                                           IEnumerable<SlashCommandRequirementAttribute>? requirements, ISlashCommandContext context,
                                                                           List<IDiscordInteractionCommandOption>? suppliedOptions = null, IServiceProvider? serviceProvider = null);

        /// <summary>
        ///     Execute a specific command with their dependencies.
        ///     The command will be searched for with its <see cref="SlashCommandAttribute.Name" />.
        /// </summary>
        /// <param name="commandInfo">The command that will be executed.</param>
        /// <param name="context">The current <see cref="ISlashCommandContext" /> that will be passed to the command module.</param>
        /// <param name="suppliedOptions">The options used by the user.</param>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider" /> containing the necessary dependencies for the the
        ///     module of the command.
        /// </param>
        /// <returns>
        ///     The <see cref="Result" /> containing the result of the command execution.
        /// </returns>
        /// <seealso cref="Result" />
        /// <seealso cref="SlashCommandAttribute" />
        Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(ISlashCommandInfo commandInfo, ISlashCommandContext context, List<IDiscordInteractionCommandOption>? suppliedOptions = null,
                                                                           IServiceProvider? serviceProvider = null);

        /// <summary>
        ///     Execute a specific command with their dependencies.
        ///     The command will be searched for with its <see cref="SlashCommandAttribute.Name" />.
        /// </summary>
        /// <param name="commandOptionInfo">The sub command that will be executed.</param>
        /// <param name="context">The current <see cref="ISlashCommandContext" /> that will be passed to the command module.</param>
        /// <param name="suppliedOptions">The options used by the user.</param>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider" /> containing the necessary dependencies for the the
        ///     module of the command.
        /// </param>
        /// <returns>
        ///     The <see cref="Result" /> containing the result of the command execution.
        /// </returns>
        /// <seealso cref="Result" />
        /// <seealso cref="SlashCommandAttribute" />
        Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(ISlashCommandOptionInfo commandOptionInfo, ISlashCommandContext context,
                                                                           List<IDiscordInteractionCommandOption>? suppliedOptions = null, IServiceProvider? serviceProvider = null);

        /// <summary>
        ///     Execute a specific command with their dependencies.
        ///     The command will be searched for with its <see cref="SlashCommandAttribute.Name" />.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="context">The current <see cref="ISlashCommandContext" /> that will be passed to the command module.</param>
        /// <param name="options">The options used with the command.</param>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider" /> containing the necessary dependencies for the the
        ///     module of the command.
        /// </param>
        /// <returns>
        ///     The <see cref="Result" /> containing the result of the command execution.
        /// </returns>
        /// <seealso cref="Result" />
        /// <seealso cref="SlashCommandAttribute" />
        Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(string name, ISlashCommandContext context, IEnumerable<IDiscordInteractionCommandOption>? options = null,
                                                                           IServiceProvider? serviceProvider = null);

        /// <summary>
        ///     Execute a specific command with their dependencies.
        ///     The command will be searched for with its <see cref="SlashCommandAttribute.Name" />.
        /// </summary>
        /// <param name="commandGroupName">The name of the command group.</param>
        /// <param name="commandName">The name of the command.</param>
        /// <param name="context">The current <see cref="ISlashCommandContext" /> that will be passed to the command module.</param>
        /// <param name="options">The options used with the command.</param>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider" /> containing the necessary dependencies for the the
        ///     module of the command.
        /// </param>
        /// <returns>
        ///     The <see cref="Result" /> containing the result of the command execution.
        /// </returns>
        /// <seealso cref="Result" />
        /// <seealso cref="SlashCommandAttribute" />
        Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(string commandGroupName, string commandName, ISlashCommandContext context,
                                                                           IEnumerable<IDiscordInteractionCommandOption>? options = null, IServiceProvider? serviceProvider = null);

        /// <summary>
        ///     Execute a specific command with their dependencies.
        ///     The command will be searched for with its <see cref="SlashCommandAttribute.Name" />.
        /// </summary>
        /// <param name="commandGroupName">The name of the command group.</param>
        /// <param name="subCommandGroupName">The name of the sub command group.</param>
        /// <param name="commandName">The name of the command.</param>
        /// <param name="context">The current <see cref="ISlashCommandContext" /> that will be passed to the command module.</param>
        /// <param name="options">The options used with the command.</param>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider" /> containing the necessary dependencies for the the
        ///     module of the command.
        /// </param>
        /// <returns>
        ///     The <see cref="Result" /> containing the result of the command execution.
        /// </returns>
        /// <seealso cref="Result" />
        /// <seealso cref="SlashCommandAttribute" />
        Task<Result<IDiscordInteractionResponse>> ExecuteSlashCommandAsync(string commandGroupName, string subCommandGroupName, string commandName, ISlashCommandContext context,
                                                                           IEnumerable<IDiscordInteractionCommandOption>? options = null, IServiceProvider? serviceProvider = null);

        /// <summary>
        ///     Search for a command by its <see cref="SlashCommandAttribute.Name" />.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <returns>
        ///     The <see cref="SlashCommandInfo" /> containing the command.
        /// </returns>
        ISlashCommandInfo? SearchSlashCommand(string name);

        /// <summary>
        ///     Search for a sub command by its <see cref="SlashCommandGroupAttribute.Name" /> of the command module.
        ///     And the <see cref="SlashCommandAttribute.Name" /> of the sub slash command.
        /// </summary>
        /// <param name="groupName">The command group name.</param>
        /// <param name="subCommandName">The name of the command.</param>
        /// <returns>
        ///     The <see cref="ISlashCommandOptionInfo" /> containing the sub command.
        /// </returns>
        ISlashCommandOptionInfo? SearchSlashCommand(string groupName, string subCommandName);

        /// <summary>
        ///     Search for a sub command by its <see cref="SlashCommandGroupAttribute.Name" /> of the command module.
        ///     And the <see cref="SlashCommandGroupAttribute.Name" /> of the sub command group.
        ///     And the <see cref="SlashCommandAttribute.Name" /> of the sub slash command.
        /// </summary>
        /// <param name="groupName">The command group name.</param>
        /// <param name="subCommandGroupName">The name of the sub command group.</param>
        /// <param name="subCommandName">The name of the command.</param>
        /// <returns>
        ///     The <see cref="ISlashCommandOptionInfo" /> containing the sub command.
        /// </returns>
        ISlashCommandOptionInfo? SearchSlashCommand(string groupName, string subCommandGroupName, string subCommandName);
    }
}