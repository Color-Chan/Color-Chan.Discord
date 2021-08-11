using Color_Chan.Discord.Core.Common.API.DataModels.Select;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Select;

namespace Color_Chan.Discord.Rest.Models.Select
{
    public record DiscordSelectOption : IDiscordSelectOption
    {
        public DiscordSelectOption(DiscordSelectOptionData data)
        {
            Label = data.Label;
            Value = data.Value;
            Description = data.Description;
            Emoji = data.Emoji is not null ? new DiscordEmoji(data.Emoji) : null;
            Default = data.Default;
        }

        /// <inheritdoc />
        public string Label { get; init; }
        /// <inheritdoc />
        public string Value { get; init; }
        /// <inheritdoc />
        public string? Description { get; init; }
        /// <inheritdoc />
        public IDiscordEmoji? Emoji { get; init; }
        /// <inheritdoc />
        public bool? Default { get; init; }
    }
}