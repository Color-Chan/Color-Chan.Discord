using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.Models.Invites;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Core.Common.API.Rest;

/// <summary>
///     Contains all the API calls mentioned in the Invite documentation.
///     Docs: https://discord.com/developers/docs/resources/invite
/// </summary>
public interface IDiscordRestInvite
{
    /// <summary>
    ///     Get an <see cref="IDiscordInvite"/> for a given invite code.
    /// </summary>
    /// <param name="inviteCode">The invite code.</param>
    /// <param name="withCounts">Whether the invite should contain approximate member counts.</param>
    /// <param name="withExpiration">Whether the invite should contain the expiration date.</param>
    /// <param name="evenId">The guild scheduled event to include with the invite.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IDiscordInvite" /> with the request results.
    /// </returns>
    Task<Result<IDiscordInvite>> GetInvite(string inviteCode, bool? withCounts = null, bool? withExpiration = null, ulong? evenId = null, CancellationToken ct = default);

    /// <summary>
    ///     Deletes an invite.
    /// </summary>
    /// <param name="inviteCode">The code of the invite that will be deleted.</param>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IDiscordInvite" /> with the request results.
    /// </returns>
    /// <remarks>
    ///     <para>
    ///         Requires the `MANAGE_CHANNELS` permission on the channel this invite belongs to.
    ///     </para>
    ///     <para>
    ///         Requires the `MANAGE_GUILD` permission to remove any invite across the guild.
    ///     </para>
    ///     <para>
    ///         Fires a Invite Delete Gateway event.
    ///         More info: https://discord.com/developers/docs/topics/gateway#invite-delete
    ///     </para>
    /// </remarks>
    Task<Result<IDiscordInvite>> DeleteInvite(string inviteCode, CancellationToken ct = default);
}