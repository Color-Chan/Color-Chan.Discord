using System;
using System.Collections.Generic;
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

namespace Color_Chan.Discord.Rest.API.Rest
{
    /// <inheritdoc cref="IDiscordRestChannel" />
    public class DiscordRestChannel : DiscordRestBase, IDiscordRestChannel
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRestGuild" />.
        /// </summary>
        /// <inheritdoc />
        public DiscordRestChannel(IDiscordHttpClient httpClient) : base(httpClient)
        {
        }

        // All api calls for guilds.

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
    }
}