using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Rest.Models.Interaction
{
    public record DiscordInteractionResponse : IDiscordInteractionResponse
    {
        public DiscordInteractionResponse()
        {
        }

        public DiscordInteractionResponse(DiscordInteractionResponseData data)
        {
            Type = data.Type;
            Data = data.Data is not null ? new DiscordInteractionCallback(data.Data) : null;
        }

        /// <inheritdoc />
        public DiscordInteractionResponseType Type { get; init; }

        /// <inheritdoc />
        public IDiscordInteractionCallback? Data { get; init; }

        /// <inheritdoc />
        public DiscordInteractionResponseData ToDataModel()
        {
            return new DiscordInteractionResponseData
            {
                Type = Type,
                Data = Data?.ToDataModel()
            };
        }
    }
}