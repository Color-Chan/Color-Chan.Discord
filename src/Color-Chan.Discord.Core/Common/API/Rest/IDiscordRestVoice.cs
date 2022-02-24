using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Core.Common.API.Rest;

/// <summary>
///     Contains all the API calls mentioned in the Voice documentation.
///     Docs: https://discord.com/developers/docs/resources/voice
/// </summary>
public interface IDiscordRestVoice
{
    /// <summary>
    ///     Get a list of voice regions that can be used when setting a voice or stage channel's `rtc_region`.
    /// </summary>
    /// <param name="ct">The <see cref="CancellationToken" />.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> of <see cref="IDiscordVoiceRegion" />s with the request results.
    /// </returns>
    Task<Result<IReadOnlyList<IDiscordVoiceRegion>>> GetVoiceRegions(CancellationToken ct = default);
}