using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Select;
using Color_Chan.Discord.Rest.Models.Select;

namespace Color_Chan.Discord.Rest.Models.Interaction
{
    public record DiscordInteractionCommand : IDiscordInteractionCommand
    {
        public DiscordInteractionCommand(DiscordInteractionRequestData data)
        {
            Id = data.Id;
            Name = data.Name;
            Type = data.Type;
            Resolved = data.Resolved is not null ? new DiscordInteractionCommandResolved(data.Resolved) : null;
            Options = data.Options?.Select(optionData => new DiscordInteractionCommandOption(optionData));
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
        public IDiscordInteractionCommandResolved? Resolved { get; init; }
        
        /// <inheritdoc />
        public IEnumerable<IDiscordInteractionCommandOption>? Options { get; set; }
        
        /// <inheritdoc />
        public string? CustomId { get; init; }
        
        /// <inheritdoc />
        public string? ComponentType { get; init; }
        
        /// <inheritdoc />
        public IDiscordSelectOption? Values { get; init; }
        
        /// <inheritdoc />
        public ulong? TargetId { get; init; }
    }
}