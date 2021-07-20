namespace Color_Chan.Discord.Host.Services
{
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
    }
}