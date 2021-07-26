using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Commands.Extensions
{
    public static class DiscordApplicationCommandOptionDataExtensions
    {
        public static bool HasNewOrUpdatedOptions(this IEnumerable<DiscordApplicationCommandOptionData> newOptions, IReadOnlyCollection<DiscordApplicationCommandOptionData> existingCommand)
        {
            return HasNewOrUpdatedOptions(newOptions.ToList(), existingCommand);
        }
        
        public static bool HasNewOrUpdatedOptions(this IReadOnlyCollection<DiscordApplicationCommandOptionData> newOptions, IReadOnlyCollection<DiscordApplicationCommandOptionData> existingCommand)
        {
            if (newOptions!.Count != existingCommand!.Count)
            {
                // Amount of choices has been updated.
                return true;
            }
            
            foreach (var commandOption in newOptions!)
            {
                var existingOption = existingCommand!.FirstOrDefault(x => x.Name.Equals(commandOption.Name));

                if (existingOption is null)
                {
                    // New option found.
                    return true;
                }
                
                // The command option already exists.
                
                if (!commandOption.Description.Equals(existingOption.Description))
                {
                    // Description has been updated.
                    return true;
                }

                if (!commandOption.IsRequired == existingOption.IsRequired)
                {
                    // IsRequired has been updated.
                    return true;
                }

                if (!commandOption.Type.Equals(existingOption.Type))
                {
                    // Type has been updated.
                    return true;
                }

                if (commandOption.Choices != null)
                {
                    foreach (var newChoice in commandOption.Choices)
                    {
                        var existingChoice = existingOption.Choices?.FirstOrDefault(x => x.Name.Equals(newChoice.Name));

                        if (existingChoice is null)
                        {
                            // New option found.
                            return true;
                        }
                        
                        // The command option choice already exists.

                        if (newChoice.Value.Equals(existingChoice.Value))
                        {
                            // Value has been updated.
                            return true;
                        }
                    }
                }
            }
            
            // No new or updated options found.
            return false;
        }
    }
}