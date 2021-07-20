using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.Params;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Logging;

namespace Color_Chan.Discord.Commands.Services.Implementations
{
    public class SlashCommandAutoSyncService : ISlashCommandAutoSyncService
    {
        private readonly ILogger<SlashCommandAutoSyncService> _logger;
        private readonly IDiscordRestApplication _restApplication;

        /// <summary>
        ///     Initializes a new instance of <see cref="IDiscordRestApplication" />.
        /// </summary>
        /// <param name="restApplication"></param>
        /// <param name="logger">The <see cref="ILogger" /> for <see cref="SlashCommandAutoSyncService" />.</param>
        public SlashCommandAutoSyncService(IDiscordRestApplication restApplication, ILogger<SlashCommandAutoSyncService> logger)
        {
            _restApplication = restApplication;
            _logger = logger;
        }

        public async Task<Result> AddUpdateApplicationCommandsAsync(IEnumerable<ISlashCommandInfo> commandInfos, SlashCommandConfiguration configurations)
        {
            _logger.LogInformation("Syncing slash commands");
            var slashCommandInfos = commandInfos.ToList();

            var guildIds = slashCommandInfos
                .Where(x => x.Guilds is not null)
                .SelectMany(x => x.Guilds!)
                .Select(x => x.GuildId)
                .Distinct();

            foreach (var guildId in guildIds)
            {
                var guildCommands = BuildSlashCommandsParams(slashCommandInfos
                    .Where(x => x.Guilds is not null &&
                                x.Guilds
                                    .Select(z => z.GuildId)
                                    .Contains(guildId))
                ).ToList();

                var guildResult = await _restApplication.BulkOverwriteGuildApplicationCommandsAsync(669311564272369682, guildId, guildCommands).ConfigureAwait(false);

                if (!guildResult.IsSuccessful)
                {
                    _logger.LogWarning("Failed to update slash commands for guild {GuildId}", guildId.ToString());
                    return Result.FromError(guildResult.ErrorResult ?? new ErrorResult($"Failed to sync slash commands for guild {guildId.ToString()}"));
                }

                _logger.LogDebug("Updated {Count} slash commands for guild {GuildId}", guildCommands.Count.ToString(), guildId.ToString());
            }

            var globalSlashCommands = BuildSlashCommandsParams(slashCommandInfos.Where(x => x.Guilds is null || !x.Guilds.Any())).ToList();

            var globalResult = await _restApplication.BulkOverwriteGlobalApplicationCommandsAsync(669311564272369682, globalSlashCommands).ConfigureAwait(false);

            if (globalResult.IsSuccessful)
            {
                _logger.LogDebug("Updated {Count} global slash commands", globalSlashCommands.Count.ToString());
                _logger.LogInformation("Finished syncing slash commands");
                return Result.FromSuccess();
            }

            _logger.LogWarning("Failed to update global slash commands");
            return Result.FromError(globalResult.ErrorResult ?? new ErrorResult("Failed to sync global slash commands"));
        }

        private IEnumerable<DiscordCreateGlobalApplicationCommandParams> BuildSlashCommandsParams(IEnumerable<ISlashCommandInfo> commandInfos)
        {
            var applicationCommandParams = new List<DiscordCreateGlobalApplicationCommandParams>();

            foreach (var commandInfo in commandInfos)
                applicationCommandParams.Add(new DiscordCreateGlobalApplicationCommandParams
                {
                    Name = commandInfo.CommandName,
                    Description = commandInfo.Description,
                    Options = BuildSlashCommandsOptions(commandInfo.CommandOptions)
                });

            return applicationCommandParams;
        }

        private IEnumerable<DiscordApplicationCommandOptionData>? BuildSlashCommandsOptions(IEnumerable<ISlashCommandOptionInfo>? commandOptionInfos)
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
                    Choice = BuildChoiceData(optionInfo.Choices)
                });

            return options;
        }

        private IEnumerable<DiscordApplicationCommandOptionChoiceData>? BuildChoiceData(IEnumerable<KeyValuePair<string, string>>? choicePairs)
        {
            if (choicePairs is null) return null;

            var choices = new List<DiscordApplicationCommandOptionChoiceData>();

            foreach (var choicePair in choicePairs)
                choices.Add(new DiscordApplicationCommandOptionChoiceData
                {
                    Name = choicePair.Key,
                    Value = choicePair.Key
                });

            return choices;
        }
    }
}