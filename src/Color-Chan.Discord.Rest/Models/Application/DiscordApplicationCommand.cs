using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Rest.Models.Application;

public class DiscordApplicationCommand : IDiscordApplicationCommand
{
    public DiscordApplicationCommand(DiscordApplicationCommandData resultEntity)
    {
        Id = resultEntity.Id;
        ApplicationId = resultEntity.ApplicationId;
        GuildId = resultEntity.GuildId;
        Name = resultEntity.Name;
        Description = resultEntity.Description;
        Options = resultEntity.Options?.Select(option => new DiscordApplicationCommandOption(option));
        DefaultPermission = resultEntity.DefaultPermission;
    }

    /// <inheritdoc />
    public ulong Id { get; init; }

    /// <inheritdoc />
    public ulong ApplicationId { get; init; }

    /// <inheritdoc />
    public ulong? GuildId { get; init; }

    /// <inheritdoc />
    public string Name { get; init; }

    /// <inheritdoc />
    public string Description { get; init; }

    /// <inheritdoc />
    public IEnumerable<IDiscordApplicationCommandOption>? Options { get; init; }

    /// <inheritdoc />
    public bool? DefaultPermission { get; init; }
}