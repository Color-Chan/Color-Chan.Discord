using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models
{
    public record DiscordOverwrite : IDiscordOverwrite
    {
        public DiscordOverwrite(DiscordOverwriteData data)
        {
            TargetId = data.TargetId;
            TargetType = data.TargetType;
            Allow = data.Allow;
            Deny = data.Deny;
        }

        /// <inheritdoc />
        public ulong TargetId { get; init; }

        /// <inheritdoc />
        public DiscordPermissionTargetType TargetType { get; init; }

        /// <inheritdoc />
        public string Allow { get; init; }

        /// <inheritdoc />
        public string Deny { get; init; }
    }
}