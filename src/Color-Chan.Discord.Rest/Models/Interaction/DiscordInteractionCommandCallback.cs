using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Rest.Models.Interaction
{
    public record DiscordInteractionCommandCallback : IDiscordInteractionCommandCallback
    {
        /// <inheritdoc />
        public bool? IsTts { get; init; }

        /// <inheritdoc />
        public string? Content { get; init; }

        /// <inheritdoc />
        public IEnumerable<IDiscordEmbed>? Embeds { get; init; }

        /// <inheritdoc />
        public IDiscordAllowedMentions? AllowedMentions { get; init; }

        /// <inheritdoc />
        public DiscordInteractionCommandCallbackFlags? Flags { get; init; }

        /// <inheritdoc />
        public IEnumerable<IDiscordComponent>? Components { get; init; }

        /// <inheritdoc />
        public DiscordInteractionCommandCallbackData ToDataModel()
        {
            return new DiscordInteractionCommandCallbackData
            {
                Content = Content,
                Embeds = Embeds?.Select(x => x.ToDataModel()),
                Components = Components?.Select(x => x.ToDataModel()),
                Flags = Flags,
                AllowedMentions = AllowedMentions?.ToDataModel(),
                IsTts = IsTts
            };
        }
    }
}