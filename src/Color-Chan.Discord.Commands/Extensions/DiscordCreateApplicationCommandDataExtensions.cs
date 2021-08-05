using System;
using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.Params.Application;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Commands.Extensions
{
    internal static class DiscordCreateApplicationCommandDataExtensions
    {
        /// <summary>
        ///     Filters out any commands that haven't been updated or are not new.
        /// </summary>
        /// <param name="newCommands">The local commands.</param>
        /// <param name="existingCommands">The commands pulled from discords api.</param>
        /// <returns>
        ///     A <see cref="Tuple"/> of <see cref="DiscordCreateApplicationCommand"/> and <see cref="bool"/> and <see cref="ulong"/>
        ///     False means that the command is not new but is updated, and True means that the command is new.
        ///     The <see cref="ulong"/> contains the command ID if the command was not new.
        /// </returns>
        internal static List<Tuple<DiscordCreateApplicationCommand, bool, ulong?>> GetUpdatedOrNewCommands(this IEnumerable<DiscordCreateApplicationCommand> newCommands,
                                                                                             IReadOnlyCollection<IDiscordApplicationCommand> existingCommands)
        {
            var updatedCommands = new List<Tuple<DiscordCreateApplicationCommand, bool, ulong?>>();

            foreach (var newCommand in newCommands)
            {
                var existingCommand = existingCommands.FirstOrDefault(x => x.Name.Equals(newCommand.Name));

                if (existingCommand is null)
                {
                    // New command found.
                    updatedCommands.Add(CreateTuple(newCommand, true));
                    continue;
                }

                // Found existing command.

                if (!newCommand.Description.Equals(existingCommand.Description))
                {
                    // Description has been updated.
                    updatedCommands.Add(CreateTuple(newCommand, false, existingCommand.Id));
                    continue;
                }

                if (!newCommand.DefaultPermission == existingCommand.DefaultPermission)
                {
                    // DefaultPermission has been updated.
                    updatedCommands.Add(CreateTuple(newCommand, false, existingCommand.Id));
                    continue;
                }

                if (newCommand.Options!.Any())
                {
                    if (existingCommand.Options is null)
                    {
                        // Options has been updated.
                        updatedCommands.Add(CreateTuple(newCommand, false, existingCommand.Id));
                        continue;
                    }

                    if (newCommand.Options!.HasNewOrUpdatedOptions(existingCommand.Options.ToList()))
                    {
                        // Options has been updated.
                        updatedCommands.Add(CreateTuple(newCommand, false, existingCommand.Id));
                    }
                }

                if (newCommand.Options!.Any() != existingCommand.Options is not null)
                {
                    // New command doesnt have any options but the existing does.
                    updatedCommands.Add(CreateTuple(newCommand, false, existingCommand.Id));
                }
            }

            return updatedCommands;
        }

        private static Tuple<DiscordCreateApplicationCommand, bool, ulong?> CreateTuple(DiscordCreateApplicationCommand command, bool isNew, ulong? commandId = null)
        {
            return new Tuple<DiscordCreateApplicationCommand, bool, ulong?>(command, isNew, commandId);
        }
    }
}