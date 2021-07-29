using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations
{
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
                    throw new NoOptionAttributeArgumentException($"Parameter {parameter.Name} for command method {command.Name} is missing SlashCommandOptionAttribute");

                if (choiceAttributes.Any())
                {
                    var choices = choiceAttributes
                                  .Select(choiceAttribute => new KeyValuePair<string, string>(choiceAttribute.Name, choiceAttribute.Value))
                                  .ToList();

                    options.Add(new SlashCommandOptionInfo(optionAttribute.Name, optionAttribute.Description, parameter.ParameterType, optionAttribute.IsRequired, choices));
                }
                else
                {
                    options.Add(new SlashCommandOptionInfo(optionAttribute.Name, optionAttribute.Description, parameter.ParameterType, optionAttribute.IsRequired));
                }
            }

            _logger.LogDebug("Found {Count} options for command {MethodName}", options.Count, command.Name);
            return options;
        }

        /// <inheritdoc />
        public IEnumerable<DiscordApplicationCommandOptionData>? BuildSlashCommandsOptions(IEnumerable<ISlashCommandOptionInfo>? commandOptionInfos)
        {
            if (commandOptionInfos is null) return null;

            var options = new List<DiscordApplicationCommandOptionData>();

            foreach (var optionInfo in commandOptionInfos)
                options.Add(new DiscordApplicationCommandOptionData
                {
                    Name = optionInfo.Name,
                    Description = optionInfo.Description,
                    Type = optionInfo.Type,
                    IsRequired = optionInfo.IsRequired,
                    Choices = BuildChoiceData(optionInfo.Choices)
                });

            if (options.Count > MaxCommandOptions) throw new UpdateSlashCommandException($"A slash command can not have more then {MaxCommandOptions} options.");

            return options;
        }

        /// <inheritdoc />
        public IEnumerable<DiscordApplicationCommandOptionChoiceData>? BuildChoiceData(IEnumerable<KeyValuePair<string, string>>? choicePairs)
        {
            if (choicePairs is null) return null;

            var choices = new List<DiscordApplicationCommandOptionChoiceData>();

            foreach (var choicePair in choicePairs)
                choices.Add(new DiscordApplicationCommandOptionChoiceData
                {
                    Name = choicePair.Key,
                    Value = choicePair.Value
                });

            if (choices.Count > MaxCommandOptionChoices) throw new UpdateSlashCommandException($"A slash command option can not have more then {MaxCommandOptionChoices} choices.");

            return choices;
        }
    }
}