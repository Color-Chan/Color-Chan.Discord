using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Models.Application
{
    public record DiscordApplicationCommandOption : IDiscordApplicationCommandOption
    {
        public DiscordApplicationCommandOption(DiscordApplicationCommandOptionType type, string name, string description, bool? isRequired = default,
            IEnumerable<IDiscordApplicationCommandOptionChoice>? choice = null, IEnumerable<IDiscordApplicationCommandOption>? subOptions = null)
        {
            Type = type;
            Name = name;
            Description = description;
            IsRequired = isRequired;
            Choice = choice;
            SubOptions = subOptions;
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
        public IEnumerable<IDiscordApplicationCommandOptionChoice>? Choice { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordApplicationCommandOption>? SubOptions { get; set; }
    }
}