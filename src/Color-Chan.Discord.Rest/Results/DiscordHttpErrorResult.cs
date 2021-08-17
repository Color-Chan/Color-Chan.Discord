using System.Net;
using Color_Chan.Discord.Core.Common.API.DataModels.Errors;

namespace Color_Chan.Discord.Rest.Results
{
    /// <summary>
    ///     A Discord HTTP error result.
    /// </summary>
    public record DiscordHttpErrorResult : HttpErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordHttpErrorResult" />.
        /// </summary>
        /// <param name="errorData">The error data.</param>
        /// <param name="statusCode">The <see cref="HttpStatusCode" /> of the response.</param>
        public DiscordHttpErrorResult(DiscordJsonErrorData errorData, HttpStatusCode statusCode) : base(statusCode, errorData.Message)
        {
            ErrorData = errorData;
        }

        /// <summary>
        ///     The error data of the HTTP request.
        /// </summary>
        public DiscordJsonErrorData ErrorData { get; }
    }
}