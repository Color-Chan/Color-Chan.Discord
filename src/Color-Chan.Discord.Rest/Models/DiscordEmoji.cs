using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models
{
    /// <inheritdoc cref="IDiscordEmoji"/>
    public record DiscordEmoji : IDiscordEmoji
    {
        /// <summary>
        ///     Initializes a new <see cref="DiscordEmoji"/>
        /// </summary>
        /// <param name="data">The data needed to create the <see cref="DiscordEmoji"/>.</param>
        public DiscordEmoji(DiscordEmojiData data)
        {
            Id = data.Id;
            Name = data.Name;
            RoleIds = data.RoleIds;
            User = data.User is not null ? new DiscordUser(data.User) : null;
            IsAnimated = data.IsAnimated;
            IsAvailable = data.IsAvailable;
            IsManaged = data.IsManaged;
            RequireColons = data.RequireColons;
        }

        /// <inheritdoc />
        public ulong Id { get; init; }

        /// <inheritdoc />
        public string Name { get; init; } = null!;

        /// <inheritdoc />
        public IEnumerable<ulong>? RoleIds { get; init; }

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

        /// <inheritdoc />
        public DiscordEmojiData ToDataModel()
        {
            return new DiscordEmojiData
            {
                Id = Id,
                Name = Name,
                RoleIds = RoleIds,
                User = User?.ToDataModel(),
                IsAnimated = IsAnimated,
                IsAvailable = IsAvailable,
                IsManaged = IsManaged,
                RequireColons = RequireColons
            };
        }
    }
}