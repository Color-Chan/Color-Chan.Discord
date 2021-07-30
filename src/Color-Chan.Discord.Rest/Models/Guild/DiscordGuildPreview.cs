using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Guild
{
    public class DiscordGuildPreview : IDiscordGuildPreview
    {
        public DiscordGuildPreview(DiscordGuildPreviewData data)
        {
            Id = data.Id;
            Name = data.Name;
            Icon = data.Icon;
            Splash = data.Splash;
            DiscoverySplash = data.DiscoverySplash;
            Roles = data.Roles.Select(roleData=>new DiscordGuildRole(roleData));
            Emojis = data.Emojis.Select(emojiData=>new DiscordEmoji(emojiData));
            ApproximateMemberCount = data.ApproximateMemberCount;
            ApproximatePresenceCount = data.ApproximatePresenceCount;
            Description = data.Description;
        }

        /// <inheritdoc />
        public ulong Id { get; init; }

        /// <inheritdoc />
        public string Name { get; init; } = null!;

        /// <inheritdoc />
        public string? Icon { get; init; }

        /// <inheritdoc />
        public string? Splash { get; init; }

        /// <inheritdoc />
        public string? DiscoverySplash { get; init; }
        
        /// <inheritdoc />
        public IEnumerable<IDiscordGuildRole> Roles { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordEmoji> Emojis { get; set; }
        
        /// <inheritdoc />
        public int? ApproximateMemberCount { get; set; }

        /// <inheritdoc />
        public int? ApproximatePresenceCount { get; set; }
        
        /// <inheritdoc />
        public string? Description { get; init; }
    }
}