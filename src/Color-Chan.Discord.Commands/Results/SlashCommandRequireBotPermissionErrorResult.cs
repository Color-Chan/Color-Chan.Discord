using System.Collections.Generic;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Commands.Results
{
    /// <summary>
    ///     An error result describing an error for <see cref="SlashCommandRequireBotPermissionAttribute" /> when the bot did
    ///     not meet the permission requirements.
    /// </summary>
    public record SlashCommandRequireBotPermissionErrorResult : SlashCommandRequirementErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRequireBotPermissionErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        /// <param name="missingPermissions">
        ///     The missing <see cref="DiscordGuildPermission" />s the bot was required to have for
        ///     this command/command group.
        /// </param>
        public SlashCommandRequireBotPermissionErrorResult(string errorMessage, List<DiscordGuildPermission>? missingPermissions) : base(errorMessage)
        {
            MissingPermissions = missingPermissions;
        }

        /// <summary>
        ///     The missing <see cref="DiscordGuildPermission" />s the bot was required to have for this command/command group.
        /// </summary>
        /// <remarks>
        ///     Null when the command was used in DMs.
        /// </remarks>
        public List<DiscordGuildPermission>? MissingPermissions { get; }
    }
}