using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Builders.Implementations;

/// <inheritdoc />
public class SlashCommandOptionBuildService : ISlashCommandOptionBuildService
{
    private const int MaxCommandOptions = 25;
    private const int MaxCommandOptionChoices = 25;
    private readonly ILogger<SlashCommandOptionBuildService> _logger;

    /// <summary>
    ///     Initializes a new instance of <see cref="SlashCommandOptionBuildService" />.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger" /> for <see cref="SlashCommandOptionBuildService" />.</param>
    public SlashCommandOptionBuildService(ILogger<SlashCommandOptionBuildService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public IEnumerable<ISlashCommandOptionInfo> GetCommandOptions(MethodInfo command)
    {
        var options = new List<SlashCommandOptionInfo>();

        foreach (var parameter in command.GetParameters())
        {
            var optionAttribute = parameter.GetCustomAttribute<SlashCommandOptionAttribute>();
            var choiceAttributes = parameter.GetCustomAttributes<SlashCommandChoiceAttribute>().ToList();

            if (optionAttribute is null)
            {
                throw new NoOptionAttributeArgumentException(
                    $"Parameter {parameter.Name} for command method {command.Name} is missing SlashCommandOptionAttribute"
                );
            }

            if (choiceAttributes.Any())
            {
                var choices = choiceAttributes.Select(choiceAttribute => new KeyValuePair<string, object>(choiceAttribute.Name, choiceAttribute.ObjectValue()))
                    .ToList();

                options.Add(new SlashCommandOptionInfo(optionAttribute, parameter.ParameterType, choices));
            }
            else
            {
                options.Add(new SlashCommandOptionInfo(optionAttribute, parameter.ParameterType));
            }
        }

        _logger.LogDebug("Found {Count} options for command {MethodName}", options.Count.ToString(), command.Name);
        return options;
    }

    /// <inheritdoc />
    public IEnumerable<DiscordApplicationCommandOptionData>? BuildSlashCommandsOptions(IEnumerable<ISlashCommandOptionInfo>? commandOptionInfos)
    {
        if (commandOptionInfos is null)
        {
            return null;
        }

        var options = new List<DiscordApplicationCommandOptionData>();

        foreach (var optionInfo in commandOptionInfos)
        {
            var subOptions = BuildSlashCommandsOptions(optionInfo.CommandOptions);

            options.Add(
                new DiscordApplicationCommandOptionData
                {
                    Name = optionInfo.Name,
                    Description = optionInfo.Description,
                    Type = optionInfo.Type,
                    IsRequired = optionInfo.IsRequired is true ? true : null,
                    Choices = BuildChoiceData(optionInfo.Choices),
                    SubOptions = subOptions,
                    Autocomplete = optionInfo.Autocomplete,
                    ChanelTypes = optionInfo.ChanelTypes,
                    MaxValue = optionInfo.MaxValue,
                    MinValue = optionInfo.MinValue
                }
            );
        }

        if (options.Count > MaxCommandOptions)
        {
            throw new UpdateSlashCommandException($"A slash command can not have more then {MaxCommandOptions} options.");
        }

        return options;
    }

    private IEnumerable<DiscordApplicationCommandOptionChoiceData>? BuildChoiceData(IEnumerable<KeyValuePair<string, object>>? choicePairs)
    {
        if (choicePairs is null)
        {
            return null;
        }

        var choices = choicePairs.Select(choicePair => new DiscordApplicationCommandOptionChoiceData { Name = choicePair.Key, Value = choicePair.Value })
            .ToList();

        if (choices.Count > MaxCommandOptionChoices)
        {
            throw new UpdateSlashCommandException($"A slash command option can not have more then {MaxCommandOptionChoices} choices.");
        }

        return choices;
    }
}