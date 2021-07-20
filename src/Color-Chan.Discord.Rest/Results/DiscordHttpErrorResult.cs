using System.Net;

namespace Color_Chan.Discord.Rest.Results
{
    public record DiscordHttpErrorResult : HttpErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordHttpErrorResult" />.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode" /> of the response.</param>
        /// <param name="errorMessage">The message of the error.</param>
        public DiscordHttpErrorResult(HttpStatusCode statusCode, string errorMessage) : base(statusCode, errorMessage)
        {
        }
    }
}