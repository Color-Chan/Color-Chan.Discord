﻿using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Embed
{
    public record DiscordEmbedField : IDiscordEmbedField
    {
        /// <inheritdoc />
        public string Name { get; init; } = null!;

        /// <inheritdoc />
        public string Value { get; init; } = null!;

        /// <inheritdoc />
        public bool? Inline { get; init; }

        /// <inheritdoc />
        public DiscordEmbedFieldData ToDataModel()
        {
            return new()
            {
                Inline = Inline,
                Name = Name,
                Value = Value
            };
        }
    }
}