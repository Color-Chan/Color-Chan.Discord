using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.API.Params.Channel;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Message;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models;
using Color_Chan.Discord.Rest.Models.Message;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Rest.API.Rest
{
    /// <inheritdoc cref="IDiscordRestChannel" />
    public class DiscordRestChannel : DiscordRestBase, IDiscordRestChannel
    {
        private readonly IOptions<JsonSerializerOptions> _serializerOptions;

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRestGuild" />.
        /// </summary>
        /// <inheritdoc />
        public DiscordRestChannel(IDiscordHttpClient httpClient,  IOptions<JsonSerializerOptions> serializerOptions) : base(httpClient)
        {
            _serializerOptions = serializerOptions;
        }

        // All api calls for channels.
        #region Channels

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordChannel>> GetChannelAsync(ulong channelId, CancellationToken ct = default)
        {
            var endpoint = $"channels/{channelId.ToString()}";
            var result = await HttpClient.GetAsync<DiscordChannelData>(endpoint, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        // Todo: Implement Modify Channel: https://discord.com/developers/docs/resources/channel#modify-channel

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordChannel>> DeleteOrCloseAsync(ulong channelId, CancellationToken ct = default)
        {
            var endpoint = $"channels/{channelId.ToString()}";
            var result = await HttpClient.DeleteAsync<DiscordChannelData>(endpoint, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        private Result<IDiscordChannel> ConvertResult(Result<DiscordChannelData> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordChannel>.FromError(null, result.ErrorResult);

            return Result<IDiscordChannel>.FromSuccess(new DiscordChannel(result.Entity));
        }

        #endregion

        // All api calls for channel messages.
        #region Channel messages

        /// <inheritdoc />
        public virtual async Task<Result<IReadOnlyList<IDiscordMessage>>> GetChannelMessagesAsync(ulong channelId, ulong? around = null, ulong? before = null, ulong? after = null, int limit = 50,
                                                                                                  CancellationToken ct = default)
        {
            if (limit is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(limit), "Limit has to be between 1-100");

            var queries = new List<KeyValuePair<string, string>>
            {
                new(Constants.Headers.LimitQueryName, limit.ToString())
            };

            if (around is not null) queries.Add(new KeyValuePair<string, string>(Constants.Headers.AroundQueryName, around.ToString()!));
            if (before is not null) queries.Add(new KeyValuePair<string, string>(Constants.Headers.BeforeQueryName, before.ToString()!));
            if (after is not null) queries.Add(new KeyValuePair<string, string>(Constants.Headers.AfterQueryName, after.ToString()!));

            var endpoint = $"channels/{channelId.ToString()}/messages";
            var result = await HttpClient.GetAsync<IReadOnlyList<DiscordMessageData>>(endpoint, queries, ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordMessage>> GetChannelMessageAsync(ulong channelId, ulong messageId, CancellationToken ct = default)
        {
            var endpoint = $"channels/{channelId.ToString()}/messages/{messageId.ToString()}";
            var result = await HttpClient.GetAsync<DiscordMessageData>(endpoint, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordMessage>> CreateMessageAsync(ulong channelId, DiscordCreateChannelMessage message, CancellationToken ct = default)
        {
            var endpoint = $"channels/{channelId.ToString()}/messages";
            var result = await HttpClient.PostAsync<DiscordMessageData, DiscordCreateChannelMessage>(endpoint, message, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }

        /// <inheritdoc />
        public virtual async Task<Result<IDiscordMessage>> CrosspostMessageAsync(ulong channelId, ulong messageId, CancellationToken ct = default)
        {
            var endpoint = $"channels/{channelId.ToString()}/messages/{messageId.ToString()}/crosspost";
            var result = await HttpClient.PostAsync<DiscordMessageData>(endpoint, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }
        
        /// <inheritdoc />
        public virtual async Task<Result<IDiscordMessage>> EditMessageAsync(ulong channelId, ulong messageId, DiscordEditChannelMessage editChannelMessage, CancellationToken ct = default)
        {
            var endpoint = $"channels/{channelId.ToString()}/messages/{messageId.ToString()}";
            var result = await HttpClient.PatchAsync<DiscordMessageData, DiscordEditChannelMessage>(endpoint, editChannelMessage, ct: ct).ConfigureAwait(false);
            return ConvertResult(result);
        }
        
        /// <inheritdoc />
        public virtual async Task<Result> DeleteMessageAsync(ulong channelId, ulong messageId, string auditLogReason, CancellationToken ct = default)
        {
            var endpoint = $"channels/{channelId.ToString()}/messages/{messageId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, null, auditLogReason, ct).ConfigureAwait(false);
        }
        
        /// <inheritdoc />
        public virtual async Task<Result> BulkDeleteMessageAsync(ulong channelId, IReadOnlyList<ulong> messageIds, string auditLogReason, CancellationToken ct = default)
        {
            if (messageIds.Count is < 2 or > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(messageIds), "The amount of message IDs has to be between 2 and 100.");
            }
            
            var queries = new List<KeyValuePair<string, string>>
            {
                new(Constants.Headers.MessageQueryName, JsonSerializer.Serialize(messageIds, _serializerOptions.Value))
            };

            var endpoint = $"channels/{channelId.ToString()}/messages/bulk-delete";
            return await HttpClient.DeleteAsync(endpoint, queries, auditLogReason, ct).ConfigureAwait(false);
        }

        private Result<IDiscordMessage> ConvertResult(Result<DiscordMessageData> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordMessage>.FromError(null, result.ErrorResult);

            return Result<IDiscordMessage>.FromSuccess(new DiscordMessage(result.Entity));
        }

        private Result<IReadOnlyList<IDiscordMessage>> ConvertResult(Result<IReadOnlyList<DiscordMessageData>> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordMessage>>.FromError(null, result.ErrorResult);

            var list = new List<IDiscordMessage>();
            foreach (var data in result.Entity) list.Add(new DiscordMessage(data));

            return Result<IReadOnlyList<IDiscordMessage>>.FromSuccess(list);
        }

        #endregion
        
        // All api calls for channel messages.
        #region Channel mesessages

        /// <inheritdoc />
        public virtual async Task<Result> CreateReactionAsync(ulong channelId, ulong messageId, string emoji, CancellationToken ct = default)
        {
            var endpoint = $"channels/{channelId.ToString()}/messages/{messageId.ToString()}/reactions/{emoji}/@me";
            return await HttpClient.PutAsync(endpoint, ct: ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<Result> DeleteOwnReactionAsync(ulong channelId, ulong messageId, string emoji, CancellationToken ct = default)
        {
            var endpoint = $"channels/{channelId.ToString()}/messages/{messageId.ToString()}/reactions/{emoji}/@me";
            return await HttpClient.DeleteAsync(endpoint, ct: ct).ConfigureAwait(false);
        }
        
        /// <inheritdoc />
        public virtual async Task<Result> DeleteUserReactionAsync(ulong channelId, ulong messageId, string emoji, ulong userId, CancellationToken ct = default)
        {
            var endpoint = $"channels/{channelId.ToString()}/messages/{messageId.ToString()}/reactions/{emoji}/{userId.ToString()}";
            return await HttpClient.DeleteAsync(endpoint, ct: ct).ConfigureAwait(false);
        }

        // Todo: Implement Get Reactions: https://discord.com/developers/docs/resources/channel#get-reactions
        
        /// <inheritdoc />
        public virtual async Task<Result> DeleteAllReactionAsync(ulong channelId, ulong messageId, string emoji, CancellationToken ct = default)
        {
            var endpoint = $"channels/{channelId.ToString()}/messages/{messageId.ToString()}/reactions/{emoji}";
            return await HttpClient.DeleteAsync(endpoint, ct: ct).ConfigureAwait(false);
        }
        
        #endregion
    }
}