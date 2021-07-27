using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models
{
    public record DiscordEmoji : IDiscordEmoji
    {
        /// <inheritdoc />
        public ulong Id { get; init; }

        /// <inheritdoc />
        public string Name { get; init; } = null!;

        /// <inheritdoc />
        public IEnumerable<IDiscordGuildRole>? Roles { get; init; }

        /// <inheritdoc />
        public IDiscordUser? User { get; init; }

        /// <inheritdoc />
        public bool? RequireColons { get; init; }

        /// <inheritdoc />
        public bool? IsManaged { get; init; }

        /// <inheritdoc />
        public bool? IsAnimated { get; init; }

        /// <inheritdoc />
        public bool? IsAvailable { get; init; }

        public DiscordEmojiData ToDataModel()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Roles = Roles?.Select(x => x.ToDataModel()),
                User = User?.ToDataModel(),
                IsAnimated = IsAnimated,
                IsAvailable = IsAvailable,
                IsManaged = IsManaged,
                RequireColons = RequireColons
            };
        }
    }
}