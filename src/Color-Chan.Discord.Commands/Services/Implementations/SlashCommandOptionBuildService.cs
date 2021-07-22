using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Info;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations
{
    public class SlashCommandOptionBuildService : ISlashCommandOptionBuildService
    {
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
    }
}