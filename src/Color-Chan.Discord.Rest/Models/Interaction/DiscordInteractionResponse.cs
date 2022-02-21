using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Rest.Models.Interaction
{
    /// <inheritdoc cref="IDiscordInteractionResponse"/>
    public record DiscordInteractionResponse : IDiscordInteractionResponse
    {
        /// <summary>
        ///     Initializes a new <see cref="DiscordInteractionResponse"/>
        /// </summary>
        public DiscordInteractionResponse()
        {
        }

        /// <summary>
        ///     Initializes a new <see cref="DiscordInteractionResponse"/>
        /// </summary>
        /// <param name="data">The data needed to create the <see cref="DiscordInteractionResponse"/>.</param>
        public DiscordInteractionResponse(DiscordInteractionResponseData data)
        {
            Type = data.Type;
            Data = data.Data is not null ? new DiscordInteractionCallback(data.Data) : null;
        }

        /// <inheritdoc />
        public DiscordInteractionCallbackType Type { get; init; }

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