using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.Params.User;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Core.Common.API.Rest
{
    // Todo: Implement missing API methods
    /// <summary>
    ///     Contains all the API calls mentioned in the user documentation.
    ///     Docs: https://discord.com/developers/docs/resources/user
    /// </summary>
    public interface IDiscordRestUser
    {
        /// <summary>
        ///     Leave a guild.
        /// </summary>
        /// <param name="guildId">The ID of the guild that the current user will leave.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result" /> containing the request results.
        /// </returns>
        Task<Result> LeaveGuild(ulong guildId, CancellationToken ct = default);

        /// <summary>
        ///     Creates a new DM channel with a user.
        /// </summary>
        /// <param name="createDm">The <see cref="DiscordCreateDm" /> containing the recipient ID.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordChannel" /> with the request results.
        /// </returns>
        Task<Result<IDiscordChannel>> CreateDm(DiscordCreateDm createDm, CancellationToken ct = default);

        /// <summary>
        ///     Get the user object of the requesters account. For OAuth2, this requires the identify scope, which will return the
        ///     object without an email, and optionally the email scope, which returns the object with an email.
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordUser" /> with the request results.
        /// </returns>
        Task<Result<IDiscordUser>> GetCurrentUser(CancellationToken ct = default);

        /// <summary>
        ///     Get a user with their <paramref name="userId" />.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordUser" /> with the request results.
        /// </returns>
        Task<Result<IDiscordUser>> GetUser(ulong userId, CancellationToken ct = default);
    }
}