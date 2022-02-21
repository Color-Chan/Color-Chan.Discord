using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Invite;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Invites;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.API.Rest;

/// <inheritdoc cref="IDiscordRestInvite"/>
public class DiscordRestInvite : DiscordRestBase, IDiscordRestInvite
{
    /// <summary>
    ///     Initializes a new instance of <see cref="DiscordRestInvite" />.
    /// </summary>
    /// <inheritdoc />
    public DiscordRestInvite(IDiscordHttpClient httpClient) : base(httpClient)
    {
    }
    
    /// <inheritdoc />
    public virtual async Task<Result<IDiscordInvite>> GetInvite(string inviteCode, bool? withCounts = null, bool? withExpiration = null, ulong? evenId = null, CancellationToken ct = default)
    {
        var endpoint = $"invites/{inviteCode}";

        var queries = new List<KeyValuePair<string, string>>();
        if(withCounts is not null) 
            queries.Add(new KeyValuePair<string, string>(Constants.Headers.WithCountsQueryName, withCounts.ToString()!));
        if(withExpiration is not null) 
            queries.Add(new KeyValuePair<string, string>(Constants.Headers.WithExpirationQueryName, withExpiration.ToString()!));
        if(evenId is not null) 
            queries.Add(new KeyValuePair<string, string>(Constants.Headers.WithGuildScheduledEventIdQueryName, evenId.ToString()!));
        
        var result = await HttpClient.GetAsync<DiscordInviteData>(endpoint, queries, ct).ConfigureAwait(false);
        return ApiResultConverters.ConvertResult(result);
    }
}