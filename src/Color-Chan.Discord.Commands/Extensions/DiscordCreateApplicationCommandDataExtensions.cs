using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.Params;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Commands.Extensions
{
    internal static class DiscordCreateApplicationCommandDataExtensions
    {
        internal static List<DiscordCreateApplicationCommand> GetUpdatedOrNewCommands(this IEnumerable<DiscordCreateApplicationCommand> newCommands,
                                                                                      IReadOnlyCollection<IDiscordApplicationCommand> existingCommands)
        {
            var updatedCommands = new List<DiscordCreateApplicationCommand>();

            foreach (var newCommand in newCommands)
            {
                var existingCommand = existingCommands.FirstOrDefault(x => x.Name.Equals(newCommand.Name));

                if (existingCommand is null)
                {
                    // New command found.
                    updatedCommands.Add(newCommand);
                    continue;
                }

                // Found existing command.

                if (!newCommand.Description.Equals(existingCommand.Description))
                {
                    // Description has been updated.
                    updatedCommands.Add(newCommand);
                    continue;
                }

                if (!newCommand.DefaultPermission == existingCommand.DefaultPermission)
                {
                    // DefaultPermission has been updated.
                    updatedCommands.Add(newCommand);
                    continue;
                }

                if (newCommand.Options!.Any())
                {
                    if (existingCommand.Options is null)
                    {
                        // Options has been updated.
                        updatedCommands.Add(newCommand);
                        continue;
                    }

                    if (newCommand.Options!.HasNewOrUpdatedOptions(existingCommand.Options.ToList()))
                        // Options has been updated.
                        updatedCommands.Add(newCommand);
                }

                if (newCommand.Options!.Any() != existingCommand.Options is not null)
                    // New command doesnt have any options but the existing does.
                    updatedCommands.Add(newCommand);
            }

            return updatedCommands;
        }
    }
}