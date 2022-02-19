using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.API.Params.Webhook;

namespace Color_Chan.Discord.Core.Common.API.Params.Application
{
    public record DiscordCreateFollowupMessage : DiscordExecuteWebhook
    {
        /// <summary>
        ///     The flags of the message response.
        /// </summary>
        [JsonPropertyName("flags")]
        public DiscordMessageFlags? MessageFlags { get; init; }
    }
}