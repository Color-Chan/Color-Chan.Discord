using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models
{
    public interface IDiscordOverwrite
    {
        /// <summary>
        ///     Role or user id.
        /// </summary>
        ulong TargetId { get; init; }

        /// <summary>
        ///     Either 0 (role) or 1 (member).
        /// </summary>
        DiscordPermissionTargetType TargetType { get; init; }

        /// <summary>
        ///     Permission bit set.
        /// </summary>
        string Allow { get; init; }

        /// <summary>
        ///     Permission bit set.
        /// </summary>
        string Deny { get; init; }
    }
}