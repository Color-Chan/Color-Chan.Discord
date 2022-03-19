using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Rest.Models.Message
{
    public class DiscordMessageActivity : IDiscordMessageActivity
    {
        public DiscordMessageActivity(DiscordMessageActivityData data)
        {
            Type = data.Type;
            PartyId = data.PartyId;
        }

        /// <inheritdoc />
        public DiscordMessageActivityType Type { get; set; }

        /// <inheritdoc />
        public string? PartyId { get; set; }
    }
}