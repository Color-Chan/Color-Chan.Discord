using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Color_Chan.Discord.Core;
using Sodium;

namespace Color_Chan.Discord.Commands.Services.Implementations
{
    /// <inheritdoc />
    public class DiscordInteractionAuthService : IDiscordInteractionAuthService
    {
        private readonly byte[] _publicKeyBytes;

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordInteractionAuthService" />.
        /// </summary>
        /// <param name="discordTokens">The <see cref="DiscordTokens" /> containing all the necessary tokens.</param>
        public DiscordInteractionAuthService(DiscordTokens discordTokens)
        {
            _publicKeyBytes = Convert.FromHexString(discordTokens.PublicToken.AsSpan());
        }

        /// <inheritdoc />
        public bool VerifySignature(string signature, string timestamp, string rawBody)
        {
            var byteSig = Convert.FromHexString(signature.AsSpan());
            var byteBody = Encoding.Default.GetBytes($"{timestamp}{rawBody}");

            return PublicKeyAuth.VerifyDetached(byteSig, byteBody, _publicKeyBytes);
        }

        /// <inheritdoc />
        public async Task<bool> VerifySignatureAsync(string signature, string timestamp, Stream bodyStream)
        {
            // Read the bytes from the body stream.
            var memStream = new MemoryStream();
            await bodyStream.CopyToAsync(memStream).ConfigureAwait(false);
            var bodyBytes = memStream.ToArray();

            // Get convert the headers into byte arrays.
            var timeStampBytes = Encoding.Default.GetBytes(timestamp);
            var byteSig = Convert.FromHexString(signature.AsSpan());

            // Add the rawBodyBytes behind the timeStampBytes.
            var timeStampLength = timeStampBytes.Length;
            Array.Resize(ref timeStampBytes, timeStampLength + bodyBytes.Length);
            Array.Copy(bodyBytes, 0, timeStampBytes, timeStampLength, bodyBytes.Length);

            return PublicKeyAuth.VerifyDetached(byteSig, timeStampBytes, _publicKeyBytes);
        }
    }
}