﻿using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Embed;

/// <inheritdoc cref="IDiscordEmbedField" />
public record DiscordEmbedField : IDiscordEmbedField
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordEmbedField" />
    /// </summary>
    public DiscordEmbedField()
    {
    }

    /// <summary>
    ///     Initializes a new <see cref="DiscordEmbedField" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordEmbedField" />.</param>
    public DiscordEmbedField(DiscordEmbedFieldData data)
    {
        Name = data.Name;
        Value = data.Value;
        Inline = data.Inline;
    }

    /// <inheritdoc />
    public string Name { get; init; } = null!;

    /// <inheritdoc />
    public string Value { get; init; } = null!;

    /// <inheritdoc />
    public bool? Inline { get; init; }

    /// <inheritdoc />
    public DiscordEmbedFieldData ToDataModel()
    {
        return new DiscordEmbedFieldData
        {
            Inline = Inline,
            Name = Name,
            Value = Value
        };
    }
}