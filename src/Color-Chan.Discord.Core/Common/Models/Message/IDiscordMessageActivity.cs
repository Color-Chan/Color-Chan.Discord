using Color_Chan.Discord.Core.Common.API.DataModels.Message;

namespace Color_Chan.Discord.Core.Common.Models.Message
{
    public interface IDiscordMessageActivity
    {
        /// <summary>
        ///     Type of message activity.
        /// </summary>
        DiscordMessageActivityType Type { get; set; }

        /// <summary>
        ///     Party_id from a Rich Presence event.
        /// </summary>
        string? PartyId { get; set; }
    }
}