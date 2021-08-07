using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Commands.Results
{
    /// <summary>
    ///     An error result describing an error for <see cref="SlashRequireUserPermissionAttribute"/> when the user did not meed the permission requirements.
    /// </summary>
    public record SlashRequireUserPermissionResult : SlashCommandRequirementErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashRequireUserPermissionResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        /// <param name="requiredPermissions">The <see cref="DiscordGuildPermission"/> the user was required to have for this command/command group.</param>
        public SlashRequireUserPermissionResult(string errorMessage, DiscordGuildPermission requiredPermissions) : base(errorMessage)
        {
            RequiredPermissions = requiredPermissions;
        }
        
        /// <summary>
        ///     The <see cref="DiscordGuildPermission"/> the user was required to have for this command/command group.
        /// </summary>
        public DiscordGuildPermission RequiredPermissions { get; }
    }
}