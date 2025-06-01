using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Commands.InteractionHandlers;

/// <summary>
///     This handler is used to handle <see cref="DiscordInteractionRequestType.ApplicationCommand"/> interactions.
/// </summary>
public class ApplicationCommandRequestHandler(
    ILogger<ApplicationCommandRequestHandler> logger,
    IOptions<SlashCommandConfiguration> slashCommandConfiguration,
    IDiscordRestGuild restGuild,
    IDiscordRestChannel restChannel,
    IDiscordRestApplication restApplication,
    ISlashCommandService slashCommandService,
    IServiceProvider serviceProvider
) : BaseInteractionHandler(slashCommandConfiguration, restGuild, restChannel, restApplication, logger), IInteractionHandler
{
    /// <inheritdoc />
    public bool CanHandle(IDiscordInteraction interaction)
    {
        return interaction.RequestType == DiscordInteractionRequestType.ApplicationCommand;
    }

    /// <inheritdoc />
    public async Task<InternalInteractionResponse> HandleInteractionAsync(IDiscordInteraction interaction)
    {
        var context = new SlashCommandContext(await GetInteractionContextAsync(interaction).ConfigureAwait(false));
        var options = GetInteractionOptions(context);

        var arr = context.SlashCommandName.ToArray();
        var (commandInfo, optionInfo) = arr.Length switch
        {
            1 => (slashCommandService.SearchSlashCommand(arr[0]), (ISlashCommandOptionInfo)null!),
            2 => (null, slashCommandService.SearchSlashCommand(arr[0], arr[1])),
            3 => (null, slashCommandService.SearchSlashCommand(arr[0], arr[1], arr[2])),
            _ => (null, null)
        };

        if (commandInfo is null && optionInfo is null) throw new NullReferenceException($"Failed to find the requested interaction command {interaction.Data!.Name}");

        var acknowledged = false;
        if ((commandInfo is not null && commandInfo.Acknowledge) || (optionInfo is not null && optionInfo.Acknowledge))
        {
            acknowledged = await AcknowledgedIfRequiredAsync(interaction, DiscordInteractionCallbackType.DeferredChannelMessageWithSource).ConfigureAwait(false);
        }

        async Task<Result<IDiscordInteractionResponse>> Handler()
        {
            return commandInfo is not null
                ? await slashCommandService.ExecuteSlashCommandAsync(commandInfo, context, options, serviceProvider).ConfigureAwait(false)
                : await slashCommandService.ExecuteSlashCommandAsync(optionInfo!, context, options, serviceProvider).ConfigureAwait(false);
        }

        // Execute the pipelines and the command.
        var result = await serviceProvider
            .GetServices<IInteractionPipeline>()
            .Aggregate((InteractionHandlerDelegate)Handler, (next, pipeline) => () => pipeline.HandleAsync(context, next))()
            .ConfigureAwait(false);

        return GetInternalInteractionResponse(result, acknowledged, interaction.Id);
    }

    private static List<IDiscordInteractionOption>? GetInteractionOptions(SlashCommandContext context)
    {
        IEnumerable<IDiscordInteractionOption>? options = null;

        // Get the command name and the options.
        if (context.Data.Options is null)
        {
            options = context.Data.Options;
            context.SlashCommandName = [context.Data.Name];
        }
        else
        {
            // Check if any of the used options is a sub command (group).
            foreach (var option in context.Data.Options)
            {
                switch (option.Type)
                {
                    case DiscordApplicationCommandOptionType.SubCommand:
                    {
                        context.SlashCommandName = [context.Data.Name, option.Name];
                        options = option.SubOptions;
                        break;
                    }
                    case DiscordApplicationCommandOptionType.SubCommandGroup when option.SubOptions is null:
                        throw new NullReferenceException("A sub command group needs to have options");
                    case DiscordApplicationCommandOptionType.SubCommandGroup:
                    {
                        context.SlashCommandName = [context.Data.Name, option.Name];
                        foreach (var subOption in option.SubOptions.Where(x => x.Type == DiscordApplicationCommandOptionType.SubCommand))
                        {
                            context.SlashCommandName = context.SlashCommandName.Append(subOption.Name);
                            options = subOption.SubOptions;
                        }

                        break;
                    }
                }
            }
        }

        return options?.ToList();
    }
}