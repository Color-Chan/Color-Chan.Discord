using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Select;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    /// <inheritdoc cref="IDiscordInteractionRequest"/>
    public record DiscordInteractionRequestData
    {
        /// <inheritdoc cref="IDiscordInteractionRequest.Id"/>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <inheritdoc cref="IDiscordInteractionRequest.Name"/>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <inheritdoc cref="IDiscordInteractionRequest.Type"/>
        [JsonPropertyName("type")]
        public DiscordApplicationCommandTypes Type { get; init; }

        /// <inheritdoc cref="IDiscordInteractionRequest.Resolved"/>
        [JsonPropertyName("resolved")]
        public DiscordInteractionResolvedData? Resolved { get; init; }

        /// <inheritdoc cref="IDiscordInteractionRequest.Options"/>
        [JsonPropertyName("options")]
        public IEnumerable<DiscordInteractionOptionData>? Options { get; init; }

        /// <inheritdoc cref="IDiscordInteractionRequest.CustomId"/>
        [JsonPropertyName("custom_id")]
        public string? CustomId { get; init; }

        /// <inheritdoc cref="IDiscordInteractionRequest.ComponentType"/>
        [JsonPropertyName("component_type")]
        public DiscordComponentType? ComponentType { get; init; }

        /// <inheritdoc cref="IDiscordInteractionRequest.Values"/>
        [JsonPropertyName("values")]
        public DiscordSelectOptionData? Values { get; init; }

        /// <inheritdoc cref="IDiscordInteractionRequest.TargetId"/>
        [JsonPropertyName("target_id")]
        public ulong? TargetId { get; init; }
    }
}