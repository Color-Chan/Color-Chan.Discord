using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Rest.Models.Application
{
    public record DiscordApplicationCommandOption : IDiscordApplicationCommandOption
    {
        public DiscordApplicationCommandOption(DiscordApplicationCommandOptionData data)
        {
            Type = data.Type;
            Name = data.Name;
            Description = data.Description;
            IsRequired = data.IsRequired;
            Choices = data.Choices?.Select(choice => new DiscordApplicationCommandOptionChoice(choice.Name, choice.Value));
            SubOptions = data.SubOptions?.Select(subOption => new DiscordApplicationCommandOption(subOption));
        }

        /// <inheritdoc />
        public DiscordApplicationCommandOptionType Type { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public bool? IsRequired { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordApplicationCommandOptionChoice>? Choices { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordApplicationCommandOption>? SubOptions { get; set; }
    }
}