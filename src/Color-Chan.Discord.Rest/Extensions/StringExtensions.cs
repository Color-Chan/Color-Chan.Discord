using System.Linq;

namespace Color_Chan.Discord.Rest.Extensions
{
    internal static class StringExtensions
    {
        internal static string? GetMayorParameter(this string endpoint)
        {
            var splitEndpoint = endpoint.ToLower().Split('/');
            if (splitEndpoint.FirstOrDefault() is not ("channels" or "guilds" or "webhooks"))
            {
                return null;
            }

            var mayorParam = splitEndpoint.GetValue(1) as string;

            // todo: Check if the the webhook_token  mayor param exist.

            // We can assume that the mayor param at pos 1 is always a ulong, since the webhook_token mayor param never occurs there. 
            return ulong.TryParse(mayorParam, out _) ? mayorParam : null;
        }
    }
}