﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Commands.Extensions;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Commands.Services.Builders;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Commands.Services.Implementations;

/// <inheritdoc />
public class SlashCommandAutoSyncService : ISlashCommandAutoSyncService
{
    private const int MaxGlobalCommands = 100;
    private const int MaxGuildCommands = 100;
    private readonly ISlashCommandBuildService _commandBuildService;
    private readonly DiscordTokens _discordTokens;
    private readonly ILogger<SlashCommandAutoSyncService> _logger;
    private readonly IDiscordRestApplication _restApplication;
    private readonly SlashCommandConfiguration _slashConfigs;

    /// <summary>
    ///     Initializes a new instance of <see cref="IDiscordRestApplication" />.
    /// </summary>
    /// <param name="restApplication">
    ///     The <see cref="IDiscordRestApplication" /> that will be used to update and delete slash commands.
    /// </param>
    /// <param name="discordTokens">The tokens for the application.</param>
    /// <param name="logger">The <see cref="ILogger" /> for <see cref="SlashCommandAutoSyncService" />.</param>
    /// <param name="commandBuildService">
    ///     The <see cref="ISlashCommandBuildService" /> that will be used to build the slash
    ///     commands parameters.
    /// </param>
    /// <param name="slashConfigs">The configurations for the slash commands.</param>
    public SlashCommandAutoSyncService(IDiscordRestApplication restApplication, DiscordTokens discordTokens, ILogger<SlashCommandAutoSyncService> logger,
                                       ISlashCommandBuildService commandBuildService, IOptions<SlashCommandConfiguration> slashConfigs)
    {
        _commandBuildService = commandBuildService;
        _slashConfigs = slashConfigs.Value;
        _restApplication = restApplication;
        _discordTokens = discordTokens;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<Result> UpdateApplicationCommandsAsync(IEnumerable<ISlashCommandInfo> commandInfos)
    {
        if (!_slashConfigs.EnableAutoSync) return Result.FromSuccess();

        _logger.LogInformation("Checking for new or outdated slash commands");
        var slashCommandInfos = commandInfos.ToList();

        // Update all guild commands.
        var guildResult = await UpdateGuildCommandsAsync(slashCommandInfos).ConfigureAwait(false);
        if (!guildResult.IsSuccessful) return Result.FromError(guildResult.ErrorResult ?? new ErrorResult("Failed to sync guild slash commands"));

        // Update all global commands.
        var globalResult = await UpdateGlobalCommandsAsync(slashCommandInfos).ConfigureAwait(false);

        return !globalResult.IsSuccessful
            ? Result.FromError(globalResult.ErrorResult ?? new ErrorResult("Failed to sync global slash commands"))
            : Result.FromSuccess();
    }

    private async Task<Result> UpdateGlobalCommandsAsync(IEnumerable<ISlashCommandInfo> slashCommandInfos)
    {
        // Build the slash commands.
        var globalSlashCommands = _commandBuildService.BuildSlashCommandsParams(slashCommandInfos.Where(x => x.Guilds is null || !x.Guilds.Any())).ToList();
        if (globalSlashCommands.Count > MaxGlobalCommands) throw new UpdateSlashCommandException($"An application can not have more then {MaxGlobalCommands} global commands.");

        // Get the current global slash commands.
        var existingCommands = await _restApplication.GetGlobalApplicationCommandsAsync(_discordTokens.ApplicationId).ConfigureAwait(false);
        if (!existingCommands.IsSuccessful) return Result.FromError(existingCommands.ErrorResult ?? new ErrorResult("Failed to get existing global slash commands."));

        // Create or update the slash commands.
        var newGlobalSlashCommands = globalSlashCommands.GetUpdatedOrNewCommands(existingCommands.Entity!);
        _logger.LogInformation("Found {Count} new/updated global slash commands", newGlobalSlashCommands.Count.ToString());

        // Sync all the global commands with discord.
        foreach (var (command, isNew, commandId) in newGlobalSlashCommands)
        {
            if (isNew)
            {
                // Creating new slash command.
                var result = await _restApplication.CreateGlobalApplicationCommandAsync(_discordTokens.ApplicationId, command).ConfigureAwait(false);
                if (!result.IsSuccessful) return Result.FromError(existingCommands.ErrorResult ?? new ErrorResult("Failed to create global slash commands."));
                _logger.LogInformation("Created new global slash command {CommandName} {Id}", result.Entity!.Name, result.Entity!.Id.ToString());
            }
            else
            {
                if (!commandId.HasValue)
                {
                    _logger.LogWarning("Skipping updating global command {CommandName}, missing command ID", command.Name);
                    continue;
                }

                // Updating slash command.
                var result = await _restApplication.EditGlobalApplicationCommandAsync(_discordTokens.ApplicationId, commandId.Value, command).ConfigureAwait(false);
                if (!result.IsSuccessful) return Result.FromError(existingCommands.ErrorResult ?? new ErrorResult("Failed to create global slash commands."));
                _logger.LogInformation("Updated existing global slash command {CommandName} {Id}", result.Entity!.Name, result.Entity!.Id.ToString());
            }
        }

        // Skip deleting old slash commands if none exist.
        if (!existingCommands.Entity!.Any()) return Result.FromSuccess();

        // Delete old global commands.
        foreach (var existingCommand in existingCommands.Entity!)
        {
            if (!globalSlashCommands.Select(x => x.Name).Contains(existingCommand.Name))
            {
                // Delete old global slash command.
                var result = await _restApplication.DeleteGlobalApplicationCommandAsync(_discordTokens.ApplicationId, existingCommand.Id).ConfigureAwait(false);
                if (!result.IsSuccessful) return Result.FromError(existingCommands.ErrorResult ?? new ErrorResult("Failed to delete existing global slash commands."));
                _logger.LogInformation("Deleted old global slash command {CommandName} {Id}", existingCommand.Name, existingCommand.Id.ToString());
            }
        }

        _logger.LogInformation("Finished syncing global slash commands");
        return Result.FromSuccess();
    }

    private async Task<Result> UpdateGuildCommandsAsync(IReadOnlyCollection<ISlashCommandInfo> slashCommandInfos)
    {
        var guildIds = slashCommandInfos
                       .Where(x => x.Guilds is not null)
                       .SelectMany(x => x.Guilds!)
                       .Select(x => x.GuildId)
                       .Distinct();

        foreach (var guildId in guildIds)
        {
            // Build the slash commands.
            var guildCommandInfos = GetGuildCommandInfos(slashCommandInfos, guildId).ToList();
            var guildCommands = _commandBuildService.BuildSlashCommandsParams(guildCommandInfos).ToList();
            if (guildCommands.Count > MaxGuildCommands) throw new UpdateSlashCommandException($"A guild can not have more then {MaxGuildCommands} slash commands.");

            // Get the current guild slash commands.
            var existingCommands = await _restApplication.GetGuildApplicationCommandsAsync(_discordTokens.ApplicationId, guildId).ConfigureAwait(false);
            if (!existingCommands.IsSuccessful) return Result.FromError(existingCommands.ErrorResult ?? new ErrorResult("Failed to get existing guild slash commands."));

            // Create or update the slash commands.
            var newGuildSlashCommandInfos = guildCommands.GetUpdatedOrNewCommands(existingCommands.Entity!);
            _logger.LogInformation("Found {Count} new/updated guild slash commands for guildId {GuildId}", newGuildSlashCommandInfos.Count.ToString(), guildId.ToString());

            // Sync all the guild commands with discord.
            foreach (var (newCommand, isNew, commandId) in newGuildSlashCommandInfos)
            {
                if (isNew)
                {
                    // Creating a new slash command.
                    var result = await _restApplication.CreateGuildApplicationCommandAsync(_discordTokens.ApplicationId, guildId, newCommand).ConfigureAwait(false);
                    if (!result.IsSuccessful) return Result.FromError(existingCommands.ErrorResult ?? new ErrorResult("Failed to create guild slash commands."));

                    _logger.LogInformation("Created new guild slash command {CommandName} {Id}", result.Entity!.Name, result.Entity!.Id.ToString());
                }
                else
                {
                    if (!commandId.HasValue)
                    {
                        _logger.LogWarning("Skipping updating guild command {CommandName}, missing command ID", newCommand.Name);
                        continue;
                    }

                    // Updating a slash command.
                    var result = await _restApplication.EditGuildApplicationCommandAsync(_discordTokens.ApplicationId, guildId, commandId.Value, newCommand).ConfigureAwait(false);
                    if (!result.IsSuccessful) return Result.FromError(existingCommands.ErrorResult ?? new ErrorResult("Failed to create guild slash commands."));


                    _logger.LogInformation("Updated existing guild slash command {CommandName} {Id}", result.Entity!.Name, result.Entity!.Id.ToString());
                }
            }

            // Delete old guild guild commands.
            foreach (var existingCommand in existingCommands.Entity!)
            {
                if (guildCommands.Select(x => x.Name).Contains(existingCommand.Name))
                {
                    continue;
                }

                // Delete old guild slash command.
                var result = await _restApplication.DeleteGuildApplicationCommandAsync(_discordTokens.ApplicationId, guildId, existingCommand.Id).ConfigureAwait(false);
                if (!result.IsSuccessful) return Result.FromError(existingCommands.ErrorResult ?? new ErrorResult("Failed to delete existing guild slash commands."));

                _logger.LogInformation("Deleted old guild slash command {CommandName} {Id}", existingCommand.Name, existingCommand.Id.ToString());
            }

            _logger.LogInformation("Finished syncing guild slash commands for guild {Id}", guildId.ToString());
        }

        return Result.FromSuccess();
    }

    private static IEnumerable<ISlashCommandInfo> GetGuildCommandInfos(IEnumerable<ISlashCommandInfo> commandInfos, ulong guildId)
    {
        return commandInfos.Where(x => x.Guilds is not null && x.Guilds.Select(z => z.GuildId).Contains(guildId)).ToList();
    }
}