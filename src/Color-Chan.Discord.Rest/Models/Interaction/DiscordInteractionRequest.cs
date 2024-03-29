﻿using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Select;
using Color_Chan.Discord.Rest.Models.Select;

namespace Color_Chan.Discord.Rest.Models.Interaction;

/// <inheritdoc cref="IDiscordInteractionRequest" />
public record DiscordInteractionRequest : IDiscordInteractionRequest
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordInteractionRequest" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordInteractionRequest" />.</param>
    public DiscordInteractionRequest(DiscordInteractionRequestData data)
    {
        Id = data.Id;
        Name = data.Name;
        Type = data.Type;
        Resolved = data.Resolved is not null ? new DiscordInteractionResolved(data.Resolved) : null;
        Options = data.Options?.Select(optionData => new DiscordInteractionOption(optionData));
        CustomId = data.CustomId;
        ComponentType = data.ComponentType;
        Values = data.Values is not null ? new DiscordSelectOption(data.Values) : null;
        TargetId = data.TargetId;
    }

    /// <inheritdoc />
    public ulong Id { get; init; }

    /// <inheritdoc />
    public string Name { get; init; } = null!;

    /// <inheritdoc />
    public DiscordApplicationCommandTypes Type { get; set; }

    /// <inheritdoc />
    public IDiscordInteractionResolved? Resolved { get; init; }

    /// <inheritdoc />
    public IEnumerable<IDiscordInteractionOption>? Options { get; set; }

    /// <inheritdoc />
    public string? CustomId { get; init; }

    /// <inheritdoc />
    public DiscordComponentType? ComponentType { get; init; }

    /// <inheritdoc />
    public IDiscordSelectOption? Values { get; init; }

    /// <inheritdoc />
    public ulong? TargetId { get; init; }
}