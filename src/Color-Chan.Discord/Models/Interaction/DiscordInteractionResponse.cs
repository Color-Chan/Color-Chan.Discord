using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Models.Interaction
{
    public record DiscordInteractionResponse : IDiscordInteractionResponse
    {
        /// <inheritdoc />
        public DiscordInteractionResponseType Type { get; init; }

        /// <inheritdoc />
        public IDiscordInteractionCommandCallback? Data { get; init; }

        /// <inheritdoc />
        public DiscordInteractionResponseData ToDataModel()
        {
            return new()
            {
                Type = Type,
                Data = Data?.ToDataModel()
            };
        }

        public static IDiscordInteractionResponse PingResponse()
        {
            return new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.Pong,
                Data = null
            };
        }
    }
}