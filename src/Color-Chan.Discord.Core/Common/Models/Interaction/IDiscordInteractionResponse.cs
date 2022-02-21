using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;

namespace Color_Chan.Discord.Core.Common.Models.Interaction
{
    public interface IDiscordInteractionResponse
    {
        /// <summary>
        ///     The type of interaction.
        /// </summary>
        public DiscordInteractionCallbackType Type { get; init; }

        /// <summary>
        ///     An optional response message.
        /// </summary>
        public IDiscordInteractionCallback? Data { get; init; }

        /// <summary>
        ///     Converts the model back to a discord data model so that it can be send to discord.
        /// </summary>
        /// <returns>
        ///     The converted <see cref="DiscordInteractionResponseData" />.
        /// </returns>
        DiscordInteractionResponseData ToDataModel();
    }
}