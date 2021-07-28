﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Embed
{
    public record DiscordEmbed : IDiscordEmbed
    {
        /// <inheritdoc />
        public string? Title { get; init; }

        /// <inheritdoc />
        public string? Type { get; init; }

        /// <inheritdoc />
        public string? Description { get; init; }

        /// <inheritdoc />
        public string? Url { get; init; }

        /// <inheritdoc />
        public DateTimeOffset? Timestamp { get; init; }

        /// <inheritdoc />
        public Color? Color { get; init; }

        /// <inheritdoc />
        public IDiscordEmbedFooter? Footer { get; init; }

        /// <inheritdoc />
        public IDiscordEmbedImage? Image { get; init; }

        /// <inheritdoc />
        public IDiscordEmbedThumbnail? Thumbnail { get; init; }

        /// <inheritdoc />
        public IDiscordEmbedVideo? Video { get; init; }

        /// <inheritdoc />
        public IDiscordEmbedProvider? Provider { get; init; }

        /// <inheritdoc />
        public IDiscordEmbedAuthor? Author { get; init; }

        /// <inheritdoc />
        public IEnumerable<IDiscordEmbedField>? Fields { get; init; }

        public DiscordEmbedData ToDataModel()
        {
            return new()
            {
                Footer = Footer?.ToDataModel(),
                Image = Image?.ToDataModel(),
                Video = Video?.ToDataModel(),
                Provider = Provider?.ToDataModel(),
                Author = Author?.ToDataModel(),
                Fields = Fields?.Select(field => field.ToDataModel()),
                Color = Color,
                Description = Description,
                Thumbnail = Thumbnail?.ToDataModel(),
                Timestamp = Timestamp,
                Title = Title,
                Type = Type,
                Url = Url
            };
        }
    }
}