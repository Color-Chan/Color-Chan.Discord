﻿using System;
using System.Collections.Generic;
using System.Drawing;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;

namespace Color_Chan.Discord.Core.Common.Models.Embed;

/// <summary>
///     Represents a discord Embed Object API model.
///     Docs: https://discord.com/developers/docs/resources/channel#embed-object
/// </summary>
public interface IDiscordEmbed
{
    /// <summary>
    ///     Title of embed.
    /// </summary>
    string? Title { get; init; }

    /// <summary>
    ///     Type of embed (always "rich" for webhook embeds).
    /// </summary>
    DiscordEmbedType? Type { get; init; }

    /// <summary>
    ///     Description of embed.
    /// </summary>
    string? Description { get; init; }

    /// <summary>
    ///     Url of embed.
    /// </summary>
    string? Url { get; init; }

    /// <summary>
    ///     Timestamp of embed content.
    /// </summary>
    DateTimeOffset? Timestamp { get; init; }

    /// <summary>
    ///     Color of the embed.
    /// </summary>
    Color? Color { get; init; }

    /// <summary>
    ///     Footer information.
    /// </summary>
    IDiscordEmbedFooter? Footer { get; init; }

    /// <summary>
    ///     Image information.
    /// </summary>
    IDiscordEmbedImage? Image { get; init; }

    /// <summary>
    ///     Thumbnail information.
    /// </summary>
    IDiscordEmbedThumbnail? Thumbnail { get; init; }

    /// <summary>
    ///     Video information.
    /// </summary>
    IDiscordEmbedVideo? Video { get; init; }

    /// <summary>
    ///     Provider information.
    /// </summary>
    IDiscordEmbedProvider? Provider { get; init; }

    /// <summary>
    ///     Author information.
    /// </summary>
    IDiscordEmbedAuthor? Author { get; init; }

    /// <summary>
    ///     Fields information.
    /// </summary>
    IEnumerable<IDiscordEmbedField>? Fields { get; init; }

    /// <summary>
    ///     Converts the model back to a discord data model so that it can be send to discord.
    /// </summary>
    /// <returns>
    ///     The converted <see cref="DiscordEmbedData" />.
    /// </returns>
    DiscordEmbedData ToDataModel();
}