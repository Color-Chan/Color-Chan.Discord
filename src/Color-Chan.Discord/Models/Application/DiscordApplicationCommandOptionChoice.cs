using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Models.Application
{
    public record DiscordApplicationCommandOptionChoice : IDiscordApplicationCommandOptionChoice
    {
        public DiscordApplicationCommandOptionChoice(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Value { get; set; }
    }
}