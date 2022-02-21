using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Select;
using Color_Chan.Discord.Rest.Models.Select;

namespace Color_Chan.Discord.Rest.Models
{
    /// <inheritdoc cref="IDiscordComponent"/>
    public record DiscordComponent : IDiscordComponent
    {
        /// <summary>
        ///     Initializes a new <see cref="DiscordComponentData"/>
        /// </summary>
        public DiscordComponent()
        {
        }

        /// <summary>
        ///     Initializes a new <see cref="DiscordComponentData"/>
        /// </summary>
        /// <param name="data">The data needed to create the <see cref="DiscordComponentData"/>.</param>
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
            MaxValues = data.MaxValues;
            MinValues = data.MinValues;
            Placeholder = data.Placeholder;
            SelectOptions = data.SelectOptions?.Select(selectData => new DiscordSelectOption(selectData)).Cast<IDiscordSelectOption>().ToList();
        }

        /// <inheritdoc />
        public DiscordComponentType Type { get; init; }

        /// <inheritdoc />
        public string? CustomId { get; init; }

        /// <inheritdoc />
        public bool? Disabled { get; init; }

        /// <inheritdoc />
        public DiscordButtonStyle? ButtonStyle { get; init; }

        /// <inheritdoc />
        public string? Label { get; init; }

        /// <inheritdoc />
        public IDiscordEmoji? Emoji { get; init; }

        /// <inheritdoc />
        public string? Url { get; init; }

        /// <inheritdoc />
        public List<IDiscordSelectOption>? SelectOptions { get; init; }

        /// <inheritdoc />
        public string? Placeholder { get; init; }

        /// <inheritdoc />
        public int? MinValues { get; init; }

        /// <inheritdoc />
        public int? MaxValues { get; init; }

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
                CustomId = CustomId,
                SelectOptions = SelectOptions?.Select(x => x.ToDataModel()),
                Placeholder = Placeholder,
                MaxValues = MaxValues,
                MinValues = MinValues
            };
        }
    }
}