using System.IO;
using System.Threading.Tasks;

namespace Color_Chan.Discord.Services
{
    /// <summary>
    ///     The service that verifies all interaction request.
    /// </summary>
    public interface IDiscordInteractionAuthService
    {
        /// <summary>
        ///     Verifies an interaction request.
        /// </summary>
        /// <param name="signature">The signature send with the request.</param>
        /// <param name="timestamp">The signature time stamp send with the request.</param>
        /// <param name="rawBody">The body of the request.</param>
        /// <returns>
        ///     Whether or not the request has been successfully verified.
        /// </returns>
        bool VerifySignature(string signature, string timestamp, string rawBody);

        /// <summary>
        ///     Verifies an interaction request asynchronously.
        /// </summary>
        /// <param name="signature">The signature send with the request.</param>
        /// <param name="timestamp">The signature time stamp send with the request.</param>
        /// <param name="rawBody">The body <see cref="Stream" /> of the request.</param>
        /// <returns>
        ///     Whether or not the request has been successfully verified.
        /// </returns>
        Task<bool> VerifySignatureAsync(string signature, string timestamp, Stream rawBody);
    }
}