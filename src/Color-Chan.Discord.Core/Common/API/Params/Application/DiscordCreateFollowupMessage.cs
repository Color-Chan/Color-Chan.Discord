using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.API.Params.Webhook;

namespace Color_Chan.Discord.Core.Common.API.Params.Application
{
    public record DiscordCreateFollowupMessage : DiscordExecuteWebhook
    {
        [JsonIgnore] private DiscordInteractionCallbackFlags? _flags;

        /// <summary>
        ///     The flags of the message response.
        /// </summary>
        [JsonPropertyName("flags")]
        public DiscordMessageFlags? MessageFlags { get; private set; }

        /// <summary>
        ///     The interaction response flags.
        /// </summary>
        [JsonIgnore]
        public DiscordInteractionCallbackFlags? Flags
        {
            get => _flags;
            set
            {
                if ((value & DiscordInteractionCallbackFlags.Ephemeral) == DiscordInteractionCallbackFlags.Ephemeral)
                {
                    MessageFlags = DiscordMessageFlags.Ephemeral;
                }

                _flags = value;
            }
        }
    }
}