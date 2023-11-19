using System;
using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Builders.Implementations;

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

        if (includeParentAttributes && command.DeclaringType is not null)
        {
            attributes.AddRange(GetCommandGuilds(command.DeclaringType));
        }

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
}