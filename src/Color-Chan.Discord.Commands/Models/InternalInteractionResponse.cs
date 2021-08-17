using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Rest.Models.Interaction;

namespace Color_Chan.Discord.Commands.Models
{
    /// <summary>
    ///     An internal interaction response.
    /// </summary>
    public class InternalInteractionResponse
    {
        internal InternalInteractionResponse(bool acknowledged, IDiscordInteractionResponse response)
        {
            Acknowledged = acknowledged;
            Response = response;
        }

        /// <summary>
        ///     Whether or not the the interaction has been acknowledged.
        /// </summary>
        public bool Acknowledged { get; set; }

        /// <summary>
        ///     The response that will be returned to discord.
        /// </summary>
        public IDiscordInteractionResponse Response { get; set; }

        /// <summary>
        ///     Get a ping response.
        /// </summary>
        /// <returns>
        ///     A <see cref="InternalInteractionResponse" /> containing a ping response.
        /// </returns>
        public static InternalInteractionResponse PingResponse()
        {
            return new InternalInteractionResponse(false, new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.Pong,
                Data = null
            });
        }
    }
}