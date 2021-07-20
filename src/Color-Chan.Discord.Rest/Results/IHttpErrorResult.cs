using System.Net;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.Results
{
    public interface IHttpErrorResult : IErrorResult
    {
        /// <summary>
        ///     The <see cref="HttpStatusCode" /> of the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; }
    }
}