﻿using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Embed;

namespace Color_Chan.Discord.Core.Common.Models.Interaction
{
    public interface IDiscordInteractionCommandCallback
    {
        /// <summary>
        ///     Whether or not the response is TTS.
        /// </summary>
        public bool? IsTts { get; init; }

        /// <summary>
        ///     The message content.
        /// </summary>
        public string? Content { get; init; }

        /// <summary>
        ///     A list of embed that will be added tot he response.
        /// </summary>
        /// <remarks>
        ///     Supports up to 10 embeds.
        /// </remarks>
        public IEnumerable<IDiscordEmbed>? Embeds { get; init; }

        /// <summary>
        ///     Allowed mentions object.
        /// </summary>
        public IDiscordAllowedMentions? AllowedMentions { get; init; }

        /// <summary>
        ///     Interaction application command callback data flags
        /// </summary>
        public DiscordInteractionCommandCallbackFlags? Flags { get; init; }

        /// <summary>
        ///     Message components.
        /// </summary>
        public IEnumerable<IDiscordComponent>? Components { get; init; }

        /// <summary>
        ///     Converts the model back to a discord data model so that it can be send to discord.
        /// </summary>
        /// <returns>
        ///     The converted <see cref="DiscordInteractionCommandCallbackData" />.
        /// </returns>
        DiscordInteractionCommandCallbackData ToDataModel();
    }
}