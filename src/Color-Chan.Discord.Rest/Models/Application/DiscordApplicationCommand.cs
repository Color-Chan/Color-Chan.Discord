using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Rest.Models.Application;

/// <inheritdoc />
public class DiscordApplicationCommand : IDiscordApplicationCommand
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordApplicationCommand" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordApplicationCommand" />.</param>
    public DiscordApplicationCommand(DiscordApplicationCommandData data)
    {
        Id = data.Id;
        ApplicationId = data.ApplicationId;
        GuildId = data.GuildId;
        Name = data.Name;
        Description = data.Description;
        Options = data.Options?.Select(option => new DiscordApplicationCommandOption(option));
        DefaultPermission = data.DefaultPermission;
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