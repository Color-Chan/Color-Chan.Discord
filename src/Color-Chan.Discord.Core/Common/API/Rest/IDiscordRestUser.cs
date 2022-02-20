using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.Params.User;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
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

        /// <summary>
        ///     Modify the current user.
        /// </summary>
        /// <param name="modifyCurrentUser">The data used to modify the current user.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordUser" /> with the request results.
        /// </returns>
        Task<Result<IDiscordUser>> ModifyCurrentUser(DiscordModifyCurrentUser modifyCurrentUser, CancellationToken ct = default);

        /// <summary>
        ///     Get a list of <see cref="IDiscordPartialGuild"/>s objects the current suer is a member off.
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordPartialGuild" />s with the request results.
        /// </returns>
        /// <remarks>
        ///     Requires the guilds OAuth2 scope.
        /// </remarks>
        /// <remarks>
        ///     This endpoint returns 200 guilds by default, which is the maximum number of guilds a non-bot user can join.
        ///     Therefore, pagination is not needed for integrations that need to get a list of the users' guilds.
        /// </remarks>
        Task<Result<IReadOnlyList<IDiscordPartialGuild>>> GetCurrentUserGuilds(CancellationToken ct = default);

        /// <summary>
        ///     Get a <see cref="IDiscordGuildMember"/> of the current user for a specific guild.
        /// </summary>
        /// <param name="guildId">The ID of the guild.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordGuildMember" /> with the request results.
        /// </returns>
        /// <remarks>
        ///     Requires the guilds.members.read OAuth2 scope.
        /// </remarks>
        Task<Result<IDiscordGuildMember>> GetCurrentUserGuildMember(ulong guildId, CancellationToken ct = default);
        
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
        /// <remarks>
        ///     You should not use this endpoint to DM everyone in a server about something.
        ///     DMs should generally be initiated by a user action. If you open a significant amount of DMs too quickly,
        ///     your bot may be rate limited or blocked from opening new ones.
        /// </remarks>
        Task<Result<IDiscordChannel>> CreateDm(DiscordCreateDm createDm, CancellationToken ct = default);

        /// <summary>
        ///     Create a new group DM channel with multiple users.
        /// </summary>
        /// <param name="createDm">The <see cref="DiscordCreateDmGroup"/> containing all the members data for the group.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordChannel" /> with the request results.
        /// </returns>
        /// <remarks>
        ///     This endpoint is limited to 10 active group DMs.
        /// </remarks>
        Task<Result<IDiscordChannel>> CreateDmGroup(DiscordCreateDmGroup createDm, CancellationToken ct = default);
    }
}