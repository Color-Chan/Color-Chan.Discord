using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models
{
    public record DiscordComponent : IDiscordComponent
    {
        public DiscordComponent(DiscordComponentData data)
        {
            Type = data.Type;
            ButtonStyle = data.ButtonStyle;
            Label = data.Label;
            Emoji = data.Emoji is not null ? new DiscordEmoji(data.Emoji) : null;
            CustomId = data.CustomId;
            Url = data.Url;
            Disabled = data.Disabled;
            ChildComponents = data.ChildComponents?.Select(componentData => new DiscordComponent(componentData));
        }

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
            return new DiscordComponentData
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