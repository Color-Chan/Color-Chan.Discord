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
            Data = new DiscordInteractionCommandCallback(data.Data);
        }
        
        /// <inheritdoc />
        public DiscordInteractionResponseType Type { get; init; }

        /// <inheritdoc />
        public IDiscordInteractionCommandCallback? Data { get; init; }

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