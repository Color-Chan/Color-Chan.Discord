using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Rest.Models.Interaction
{
    public record DiscordInteractionCommand : IDiscordInteractionCommand
    {
        public DiscordInteractionCommand(DiscordInteractionCommandData data)
        {
            Id = data.Id;
            Name = data.Name;
            if (data.Resolved is not null) Resolved = new DiscordInteractionCommandResolved(data.Resolved);
            Options = data.Options?.Select(optionData => new DiscordInteractionCommandOption(optionData));
            CustomId = data.CustomId;
            ComponentType = data.ComponentType;
        }

        public ulong Id { get; init; }

        /// <inheritdoc />
        public string Name { get; init; }

        /// <inheritdoc />
        public IDiscordInteractionCommandResolved? Resolved { get; init; }

        /// <inheritdoc />
        public IEnumerable<IDiscordInteractionCommandOption>? Options { get; set; }

        /// <inheritdoc />
        public string? CustomId { get; init; }

        /// <inheritdoc />
        public string? ComponentType { get; init; }
    }
}