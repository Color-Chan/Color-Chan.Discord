using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models
{
    public interface IDiscordUser
    {
        /// <summary>
        ///     The user's id.
        /// </summary>
        ulong Id { get; init; }

        /// <summary>
        ///     The user's username, not unique across the platform.
        /// </summary>
        string Username { get; init; }

        /// <summary>
        ///     The user's 4-digit discord-tag
        /// </summary>
        string Discriminator { get; init; }

        /// <summary>
        ///     The user's avatar hash.
        /// </summary>
        string? Avatar { get; init; }

        /// <summary>
        ///     Whether the user belongs to an OAuth2 application.
        /// </summary>
        bool? IsBot { get; init; }

        /// <summary>
        ///     Whether the user is an Official Discord System user (part of the urgent message system).
        /// </summary>
        bool? IsSystemUser { get; init; }

        /// <summary>
        ///     Whether the user has two factor enabled on their account.
        /// </summary>
        bool? HasMfaEnabled { get; init; }

        /// <summary>
        ///     The user's chosen language option.
        /// </summary>
        string? Locale { get; init; }

        /// <summary>
        ///     Whether the email on this account has been verified.
        /// </summary>
        bool? Verified { get; init; }

        /// <summary>
        ///     The user's email.
        /// </summary>
        string? Email { get; init; }

        /// <summary>
        ///     The private flags on a user's account.
        /// </summary>
        DiscordUserFlags? PrivateFlags { get; init; }

        /// <summary>
        ///     The type of Nitro subscription on a user's account.
        /// </summary>
        DiscordPremiumType? PremiumType { get; init; }

        /// <summary>
        ///     The public flags on a user's account.
        /// </summary>
        DiscordUserFlags? PublicFlags { get; init; }

        /// <summary>
        ///     Converts the model back to a discord data model so that it can be send to discord.
        /// </summary>
        /// <returns>
        ///     The converted <see cref="DiscordUserData" />.
        /// </returns>
        DiscordUserData ToDataModel();
    }
}