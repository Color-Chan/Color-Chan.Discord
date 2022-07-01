using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.Params.Application;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Commands.Extensions;

internal static class DiscordCreateApplicationCommandDataExtensions
{
    /// <summary>
    ///     Filters out any commands that haven't been updated or are not new.
    /// </summary>
    /// <param name="newCommands">The local commands.</param>
    /// <param name="existingDiscordCommands">The commands pulled from discords api.</param>
    /// <returns>
    ///     A <see cref="Tuple" /> of <see cref="DiscordCreateApplicationCommand" /> and <see cref="bool" /> and
    ///     <see cref="ulong" />
    ///     False means that the command is not new but is updated, and True means that the command is new.
    ///     The <see cref="ulong" /> contains the command ID if the command was not new.
    /// </returns>
    internal static List<Tuple<DiscordCreateApplicationCommand, bool, ulong?>> GetUpdatedOrNewCommands(this IEnumerable<DiscordCreateApplicationCommand> newCommands,
                                                                                                       IReadOnlyList<IDiscordApplicationCommand> existingDiscordCommands)
    {
        var updatedCommands = new List<Tuple<DiscordCreateApplicationCommand, bool, ulong?>>();
        var existingCommands = existingDiscordCommands.Select(existingCommand => new DiscordCreateApplicationCommand
        {
            Name = existingCommand.Name, 
            Description = existingCommand.Description, 
            DefaultPermission = existingCommand.DefaultPermission, 
            Options = GetOptions(existingCommand.Options)
        }).ToList();

        foreach (var newCommand in newCommands)
        {
            var existingCommand = existingCommands.FirstOrDefault(command => command.Name == newCommand.Name);
            if (existingCommand == null)
            {
                updatedCommands.Add(CreateTuple(newCommand, true));
            }
            else if(existingCommand != newCommand)
            {
                Console.WriteLine(JsonSerializer.Serialize(existingCommand, new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
                Console.WriteLine();
                Console.WriteLine(JsonSerializer.Serialize(newCommand, new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
                var id = existingDiscordCommands.FirstOrDefault(x => x.Name.Equals(newCommand.Name))!.Id;
                updatedCommands.Add(CreateTuple(newCommand, false, id));
            }
        }
        
        return updatedCommands;
    }

    private static IEnumerable<DiscordApplicationCommandOptionData>? GetOptions(IEnumerable<IDiscordApplicationCommandOption>? discordOptions)
    {
        var options = new List<DiscordApplicationCommandOptionData>();

        if (discordOptions is not null)
        {
            options.AddRange(discordOptions.Select(option => new DiscordApplicationCommandOptionData
            {
                Description = option.Description,
                Name = option.Name,
                Autocomplete = option.Autocomplete,
                Type = option.Type,
                ChanelTypes = option.ChanelTypes,
                IsRequired = option.IsRequired is true ? true : null,
                MaxValue = option.MaxValue,
                MinValue = option.MinValue,
                Choices = option.Choices?.Select(choice => new DiscordApplicationCommandOptionChoiceData
                {
                    Name = choice.Name,
                    Value = choice.RawValue
                }),
                SubOptions = GetOptions(option.SubOptions)
            }));
        }

        return !options.Any() ? null : options;
    }

    private static Tuple<DiscordCreateApplicationCommand, bool, ulong?> CreateTuple(DiscordCreateApplicationCommand command, bool isNew, ulong? commandId = null)
    {
        return new Tuple<DiscordCreateApplicationCommand, bool, ulong?>(command, isNew, commandId);
    }
}