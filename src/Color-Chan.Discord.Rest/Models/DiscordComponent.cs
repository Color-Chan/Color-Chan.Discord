using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models
{
    public record DiscordComponent : IDiscordComponent
    {
        /// <inheritdoc />
        public DiscordComponentType Type { get; init; }

        /// <inheritdoc />
        public DiscordButtonStyle? ButtonStyle { get; init; }

        /// <inheritdoc />
        public string? Label { get; init; }

        /// <inheritdoc />
        public IDiscordEmoji? Emoji { get; init; }

        /// <inheritdoc />
        public string? CustomId { get; init; }

        /// <inheritdoc />
        public string? Url { get; init; }

        /// <inheritdoc />
        public bool? Disabled { get; init; }

        /// <inheritdoc />
        public IEnumerable<IDiscordComponent>? ChildComponents { get; init; }

        /// <inheritdoc />
        public DiscordComponentData ToDataModel()
        {
            return new()
            {
                Disabled = Disabled,
                Emoji = Emoji?.ToDataModel(),
                Label = Label,
                Type = Type,
                Url = Url,
                ButtonStyle = ButtonStyle,
                ChildComponents = ChildComponents?.Select(x => x.ToDataModel()),
                CustomId = CustomId
            };
        }
    }
}