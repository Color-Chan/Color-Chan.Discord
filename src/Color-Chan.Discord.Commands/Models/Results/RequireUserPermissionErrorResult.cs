using System.Collections.Generic;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Commands.Models.Results
{
    /// <summary>
    ///     An error result describing an error for <see cref="RequireUserPermissionAttribute" /> when the user did
    ///     not meet the permission requirements.
    /// </summary>
    public record RequireUserPermissionErrorResult : SlashCommandRequirementErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="RequireUserPermissionErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        /// <param name="missingPermissions">
        ///     The missing <see cref="DiscordPermission" />s the user was required to have for
        ///     this command/command group.
        /// </param>
        public RequireUserPermissionErrorResult(string errorMessage, List<DiscordPermission>? missingPermissions) : base(errorMessage)
        {
            MissingPermissions = missingPermissions;
        }

        /// <summary>
        ///     The missing <see cref="DiscordPermission" />s the user was required to have for this command/command group.
        /// </summary>
        /// <remarks>
        ///     Null when the command was used in DMs.
        /// </remarks>
        public List<DiscordPermission>? MissingPermissions { get; }
    }
}