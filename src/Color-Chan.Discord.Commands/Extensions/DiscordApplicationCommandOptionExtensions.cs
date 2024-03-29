using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Commands.Extensions;

internal static class DiscordApplicationCommandOptionExtensions
{
    internal static bool HasNewOrUpdatedOptions(this IEnumerable<DiscordApplicationCommandOptionData> newOptions, IReadOnlyCollection<IDiscordApplicationCommandOption> existingCommand)
    {
        return HasNewOrUpdatedOptions(newOptions.ToList(), existingCommand);
    }

    internal static bool HasNewOrUpdatedOptions(this IReadOnlyCollection<DiscordApplicationCommandOptionData> newOptions, IReadOnlyCollection<IDiscordApplicationCommandOption> existingCommand)
    {
        if (newOptions.Count != existingCommand.Count)
            // Amount of choices has been updated.
            return true;

        foreach (var commandOption in newOptions)
        {
            var existingOption = existingCommand.FirstOrDefault(x => x.Name.Equals(commandOption.Name));

            if (existingOption is null)
                // New option found.
                return true;

            // The command option already exists.

            if (!commandOption.Description.Equals(existingOption.Description))
                // Description has been updated.
                return true;

            if ((commandOption.IsRequired ?? false) != (existingOption.IsRequired ?? false))
                // IsRequired has been updated.
                return true;

            if (commandOption.Type != existingOption.Type)
                // Type has been updated.
                return true;

            if ((commandOption.Autocomplete ?? false) != (existingOption.Autocomplete ?? false))
                // Autocomplete has been updated.
                return true;

            if (commandOption.MaxValue != existingOption.MaxValue)
                // MaxValue has been updated.
                return true;

            if (commandOption.MinValue != existingOption.MinValue)
                // Autocomplete has been updated.
                return true;

            if (commandOption.ChanelTypes != null)
            {
                if (existingOption.ChanelTypes is not null && commandOption.ChanelTypes.Count() != existingOption.ChanelTypes.Count())
                    // Amount of ChanelTypes has been updated.
                    return true;

                foreach (var newChannelType in commandOption.ChanelTypes)
                {
                    var existingChannelType = existingOption.ChanelTypes?.FirstOrDefault(x => x == newChannelType);

                    if (existingChannelType is null)
                        // New option found.
                        return true;

                    // The command option channel type already exists.

                    if (newChannelType != existingChannelType)
                        // Value has been updated.
                        return true;
                }
            }

            if (commandOption.Choices != null)
            {
                if (existingOption.Choices is not null && commandOption.Choices.Count() != existingOption.Choices.Count())
                    // Amount of choices has been updated.
                    return true;

                foreach (var newChoice in commandOption.Choices)
                {
                    var existingChoice = existingOption.Choices?.FirstOrDefault(x => x.Name.Equals(newChoice.Name));

                    if (existingChoice is null)
                        // New option found.
                        return true;

                    // The command option choice already exists.

                    if (!newChoice.Value.Equals(existingChoice.RawValue))
                        // Value has been updated.
                        return true;
                }
            }

            if ((commandOption.SubOptions is null || !commandOption.SubOptions.Any()) != existingOption.SubOptions is null)
                // command sub options has been updated.
                return true;

            if ((commandOption.SubOptions is null || !commandOption.SubOptions.Any()) && existingOption.SubOptions is null)
                // command sub options has been updated.
                continue;

            if (HasNewOrUpdatedOptions(commandOption.SubOptions!.ToList(), existingOption.SubOptions!.ToList()))
                // command sub options has been updated.
                return true;
        }

        // No new or updated options found.
        return false;
    }
}