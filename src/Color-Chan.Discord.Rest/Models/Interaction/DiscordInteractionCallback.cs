using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Rest.Models.Embed;

namespace Color_Chan.Discord.Rest.Models.Interaction
{
    /// <inheritdoc cref="IDiscordInteractionCallback"/>
    public record DiscordInteractionCallback : IDiscordInteractionCallback
    {
        /// <summary>
        ///     Initializes a new <see cref="DiscordInteractionCallback"/>
        /// </summary>
        public DiscordInteractionCallback()
        {
        }

        /// <summary>
        ///     Initializes a new <see cref="DiscordInteractionCallback"/>
        /// </summary>
        /// <param name="data">The data needed to create the <see cref="DiscordInteractionCallback"/>.</param>
        public DiscordInteractionCallback(DiscordInteractionCallbackData data)
        {
            IsTts = data.IsTts;
            Content = data.Content;
            Embeds = data.Embeds?.Select(x => new DiscordEmbed(x));
            AllowedMentions = data.AllowedMentions is not null ? new DiscordAllowedMentions(data.AllowedMentions) : null;
            Components = data.Components?.Select(x => new DiscordComponent(x));
        }

        /// <inheritdoc />
        public bool? IsTts { get; init; }

        /// <inheritdoc />
        public string? Content { get; init; }

        /// <inheritdoc />
        public IEnumerable<IDiscordEmbed>? Embeds { get; init; }

        /// <inheritdoc />
        public IDiscordAllowedMentions? AllowedMentions { get; init; }

        /// <inheritdoc />
        public DiscordMessageFlags? Flags { get; init; }

        /// <inheritdoc />
        public IEnumerable<IDiscordComponent>? Components { get; init; }

        /// <inheritdoc />
        public DiscordInteractionCallbackData ToDataModel()
        {
            return new DiscordInteractionCallbackData
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