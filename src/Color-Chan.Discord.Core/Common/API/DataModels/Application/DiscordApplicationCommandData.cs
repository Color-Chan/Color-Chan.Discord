using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Application
{
    /// <inheritdoc cref="IDiscordApplicationCommand"/>
    public record DiscordApplicationCommandData
    {
        /// <inheritdoc cref="IDiscordApplicationCommand.Id"/>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <inheritdoc cref="IDiscordApplicationCommand.ApplicationId"/>
        [JsonPropertyName("application_id")]
        public ulong ApplicationId { get; init; }

        /// <inheritdoc cref="IDiscordApplicationCommand.GuildId"/>
        [JsonPropertyName("guild_id")]
        public ulong? GuildId { get; init; }

        /// <inheritdoc cref="IDiscordApplicationCommand.Name"/>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <inheritdoc cref="IDiscordApplicationCommand.Description"/>
        [JsonPropertyName("description")]
        public string Description { get; init; } = null!;

        /// <inheritdoc cref="IDiscordApplicationCommand.Options"/>
        [JsonPropertyName("options")]
        public IEnumerable<DiscordApplicationCommandOptionData>? Options { get; init; }

        /// <inheritdoc cref="IDiscordApplicationCommand.DefaultPermission"/>
        [JsonPropertyName("default_permission")]
        public bool? DefaultPermission { get; init; }
    }
}