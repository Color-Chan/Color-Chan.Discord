using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.API.Rest;

/// <inheritdoc cref="IDiscordRestVoice"/>
public class DiscordVoiceRest : DiscordRestBase, IDiscordRestVoice
{
    /// <summary>
    ///     Initializes a new instance of <see cref="DiscordVoiceRest" />.
    /// </summary>
    /// <inheritdoc />
    public DiscordVoiceRest(IDiscordHttpClient httpClient) : base(httpClient)
    {
    }
    
    /// <inheritdoc />
    public virtual async Task<Result<IReadOnlyList<IDiscordVoiceRegion>>> GetVoiceRegions(CancellationToken ct = default)
    {
        const string endpoint = "/voice/regions";
        var result = await HttpClient.GetAsync<IReadOnlyList<DiscordVoiceRegionData>>(endpoint, ct: ct).ConfigureAwait(false);
        return ApiResultConverters.ConvertResult(result);
    }
}