using System.Net;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.Results;

/// <summary>
///     A HTTP error result.
/// </summary>
public interface IHttpErrorResult : IErrorResult
{
    /// <summary>
    ///     The <see cref="HttpStatusCode" /> of the response.
    /// </summary>
    public HttpStatusCode StatusCode { get; }
}