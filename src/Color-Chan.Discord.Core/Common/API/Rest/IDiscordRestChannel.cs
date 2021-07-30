using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.Params.Channel;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Message;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Core.Common.API.Rest
{
    // Todo: Implement missing API methods
    /// <summary>
    ///     Contains all the API calls mentioned in the channel object documentation.
    ///     https://discord.com/developers/docs/resources/channel
    /// </summary>
    public interface IDiscordRestChannel
    {
        /// <summary>
        ///     Gets a channel.
        /// </summary>
        /// <param name="channelId">The channel id.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordChannel" /> with the request results.
        /// </returns>
        Task<Result<IDiscordChannel>> GetChannelAsync(ulong channelId, CancellationToken ct = default);

        /// <summary>
        ///     Deletes or closes a channel.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Requires the MANAGE_CHANNELS permission for the guild, or MANAGE_THREADS if the channel is a thread.
        ///     </para>
        ///     <para>
        ///         Deleting a category does not delete its child channels; they will have their parent_id removed and a Channel
        ///         Update Gateway event will fire for each of them.
        ///     </para>
        /// </remarks>
        /// <param name="channelId">The channel id.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordChannel" /> with the request results.
        /// </returns>
        Task<Result<IDiscordChannel>> DeleteOrCloseAsync(ulong channelId, CancellationToken ct = default);

        /// <summary>
        ///     Get messages from a channel.
        /// </summary>
        /// <param name="channelId">The channel id.</param>
        /// <param name="around">Get messages around this message ID.</param>
        /// <param name="before">Get messages before this message ID.</param>
        /// <param name="after">Get messages after this message ID.</param>
        /// <param name="limit">Max number of messages to return (1-100)</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IReadOnlyList{T}" /> of <see cref="IDiscordMessage" /> with the request
        ///     results.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="limit" /> is out of range.</exception>
        Task<Result<IReadOnlyList<IDiscordMessage>>> GetChannelMessagesAsync(ulong channelId, ulong? around = null, ulong? before = null, ulong? after = null, int limit = 50,
                                                                             CancellationToken ct = default);

        /// <summary>
        ///     Get a specific message from a channel.
        /// </summary>
        /// <remarks>
        ///     If operating on a guild channel, this endpoint requires the 'READ_MESSAGE_HISTORY' permission to be present on the
        ///     current user.
        /// </remarks>
        /// <param name="channelId">The channel id.</param>
        /// <param name="messageId">The message id.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordMessage" /> with the request results.
        /// </returns>
        Task<Result<IDiscordMessage>> GetChannelMessageAsync(ulong channelId, ulong messageId, CancellationToken ct = default);

        /// <summary>
        ///     Posts a message to a text channel.
        /// </summary>
        /// <remarks>
        ///     <para>you must provide a value for at least one of content, embeds, or file.</para>
        ///     <para>
        ///         <list type="bullet">
        ///             <listheader>
        ///                 <term>Limitations</term>
        ///             </listheader>
        ///             <item>
        ///                 <description>
        ///                     When operating on a guild channel, the current user must have the SEND_MESSAGES
        ///                     permission.
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     When sending a message with tts (text-to-speech) set to true, the current user must have
        ///                     the SEND_TTS_MESSAGES permission.
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     When creating a message as a reply to another message, the current user must have the
        ///                     READ_MESSAGE_HISTORY permission.
        ///                     And the message can not be a system message.
        ///                 </description>
        ///             </item>
        ///             <item>
        ///                 <description>The maximum request size when sending a message is 8MB</description>
        ///             </item>
        ///             <item>
        ///                 <description>
        ///                     For the embed object, you can set every field except type (it will be rich regardless of if you try
        ///                     to set it),
        ///                     provider, video, and any height, width, or proxy_url values for images.
        ///                 </description>
        ///             </item>
        ///         </list>
        ///     </para>
        /// </remarks>
        /// <param name="channelId">The ID of the text channel.</param>
        /// <param name="message">
        ///     The <see cref="DiscordCreateChannelMessage" /> containing the data that will be used to create a
        ///     message.
        /// </param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordMessage" /> with the request results.
        /// </returns>
        Task<Result<IDiscordMessage>> CreateMessageAsync(ulong channelId, DiscordCreateChannelMessage message, CancellationToken ct = default);

        /// <summary>
        ///     Crosspost a message in a News Channel to following channels.
        /// </summary>
        /// <remarks>
        ///     This endpoint requires the 'SEND_MESSAGES' permission, if the current user sent the message,
        ///     or additionally the 'MANAGE_MESSAGES' permission, for all other messages,
        ///     to be present for the current user.
        /// </remarks>
        /// <param name="channelId">The channel id.</param>
        /// <param name="messageId">The message id.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     A <see cref="Result{T}" /> of <see cref="IDiscordMessage" /> with the request results.
        /// </returns>
        Task<Result<IDiscordMessage>> CrosspostMessageAsync(ulong channelId, ulong messageId, CancellationToken ct = default);
    }
}