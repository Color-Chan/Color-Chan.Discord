using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;

namespace Color_Chan.Discord.Rest.Extensions
{
    public static class HttpResponseHeadersExtensions
    {
        /// <summary>
        ///     Tries to parse the bucket id from the <see cref="HttpResponseHeaders" />.
        /// </summary>
        /// <param name="headers">The <see cref="HttpResponseHeaders" /> that should contain the bucket id.</param>
        /// <param name="id">The bucket id.</param>
        /// <returns>
        ///     Whether or not the bucket id has been successfully parsed.
        /// </returns>
        public static bool TryParseBucketId(this HttpResponseHeaders headers, [NotNullWhen(true)] out string? id)
        {
            id = default;
            if (!headers.TryGetValues("X-RateLimit-Bucket", out var ids)) 
                return false;

            id = ids.SingleOrDefault();
            return id is not null;
        }
    }
}