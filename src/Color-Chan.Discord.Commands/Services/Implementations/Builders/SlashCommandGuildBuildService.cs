using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Commands.Services.Builders;
using Color_Chan.Discord.Core.Common.API.Params.Application;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations.Builders
{
    /// <inheritdoc />
    public class SlashCommandGuildBuildService : ISlashCommandGuildBuildService
    {
        private readonly ILogger<SlashCommandGuildBuildService> _logger;

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandGuildBuildService" />.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger" /> for <see cref="SlashCommandGuildBuildService" />.</param>
        public SlashCommandGuildBuildService(ILogger<SlashCommandGuildBuildService> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public IEnumerable<SlashCommandGuildAttribute> GetCommandGuilds(MethodInfo command, bool includeParentAttributes = true)
        {
            var attributes = new List<SlashCommandGuildAttribute>();

            if (includeParentAttributes && command.DeclaringType is not null) attributes.AddRange(GetCommandGuilds(command.DeclaringType));

            var methodAttributes = command.GetCustomAttributes<SlashCommandGuildAttribute>();
            attributes.AddRange(methodAttributes);

            _logger.LogDebug("Found {Count} guild attributes for command {MethodName}", attributes.Count.ToString(), command.Name);
            return attributes;
        }

        /// <inheritdoc />
        public IEnumerable<SlashCommandGuildAttribute> GetCommandGuilds(Type commandModule)
        {
            var attributes = new List<SlashCommandGuildAttribute>();

            var parentAttributes = commandModule.GetCustomAttributes<SlashCommandGuildAttribute>();
            attributes.AddRange(parentAttributes);
            return attributes;
        }
        
        /// <inheritdoc />
        public IEnumerable<SlashCommandPermissionAttribute> GetCommandGuildPermissions(MethodInfo command, bool includeParentAttributes = true)
        {
            var attributes = new List<SlashCommandPermissionAttribute>();

            if (includeParentAttributes && command.DeclaringType is not null) attributes.AddRange(GetCommandGuildPermissions(command.DeclaringType));

            var methodAttributes = command.GetCustomAttributes<SlashCommandPermissionAttribute>();
            attributes.AddRange(methodAttributes);

            if (!GetCommandGuilds(command).Any() && attributes.Any())
            {
                _logger.LogWarning("Skipping slash permission for {CommandName}, they can not be used with global commands", command.Name);
                return new List<SlashCommandPermissionAttribute>();
            }
            
            _logger.LogDebug("Found {Count} guild permissions attributes for command {MethodName}", attributes.Count.ToString(), command.Name);
            return attributes;
        }

        /// <inheritdoc />
        public IEnumerable<SlashCommandPermissionAttribute> GetCommandGuildPermissions(Type commandModule)
        {
            var attributes = new List<SlashCommandPermissionAttribute>();
            
            var parentAttributes = commandModule.GetCustomAttributes<SlashCommandPermissionAttribute>();
            attributes.AddRange(parentAttributes);

            if (!GetCommandGuilds(commandModule).Any() && attributes.Any())
            {
                _logger.LogWarning("Skipping slash permission for {ModuleName}, they can not be used with global commands", commandModule.Name);
                return new List<SlashCommandPermissionAttribute>();
            }
            
            return attributes;
        }

        /// <inheritdoc />
        public IEnumerable<DiscordBatchEditApplicationCommandPermissions> BuildGuildPermissions(IEnumerable<ISlashCommandInfo> commandInfos)
        {
            throw new NotImplementedException();
        }
    }
}