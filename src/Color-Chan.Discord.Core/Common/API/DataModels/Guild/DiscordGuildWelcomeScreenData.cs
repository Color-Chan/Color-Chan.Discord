﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    public record DiscordGuildWelcomeScreenData
    {
        /// <summary>
        ///     The server description shown in the welcome screen.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     The server description shown in the welcome screen.
        /// </summary>
        [JsonPropertyName("welcome_channels")]
        public IEnumerable<DiscordGuildWelcomeChannelData> WelcomeChannels { get; set; } = new List<DiscordGuildWelcomeChannelData>();
    }
}