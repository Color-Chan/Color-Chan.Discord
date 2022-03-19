using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Params.User
{
    /// <summary>
    ///     Represents a discord Create DM parameter model.
    ///     Docs: https://discord.com/developers/docs/resources/user#create-dm-json-params
    /// </summary>
    public class DiscordCreateDm
    {
        /// <summary>
        ///     The recipient to open a DM channel with
        /// </summary>
        [JsonPropertyName("recipient_id")]
        public ulong RecipientId { get; set; }
    }
}