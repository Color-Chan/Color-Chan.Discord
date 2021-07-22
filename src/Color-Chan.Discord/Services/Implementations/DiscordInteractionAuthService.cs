using System;
using System.Text;
using Color_Chan.Discord.Core;
using Sodium;

namespace Color_Chan.Discord.Services.Implementations
{
    public class DiscordInteractionAuthService : IDiscordInteractionAuthService
    {
        private readonly byte[] _publicKeyBytes;

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
    }
}