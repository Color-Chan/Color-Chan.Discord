using System.Net;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Rest.Results
{
    public record HttpErrorResult : ErrorResult, IHttpErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ErrorResult" />.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode" /> of the response.</param>
        /// <param name="errorMessage">The message of the error.</param>
        public HttpErrorResult(HttpStatusCode statusCode, string errorMessage) : base(errorMessage)
        {
            StatusCode = statusCode;
        }

        /// <inheritdoc />
        public HttpStatusCode StatusCode { get; }
    }
}