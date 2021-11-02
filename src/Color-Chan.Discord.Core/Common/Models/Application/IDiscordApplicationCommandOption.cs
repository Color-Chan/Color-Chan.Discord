using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Core.Common.Models.Application
{
    public interface IDiscordApplicationCommandOption
    {
        /// <summary>
        ///     value of application command option type.
        /// </summary>
        public DiscordApplicationCommandOptionType Type { get; set; }

        /// <summary>
        ///     1-32 lowercase character name matching ^[\w-]{1,32}$.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     1-100 character description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     If the parameter is required or optional--default false.
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        ///     Choices for string and int types for the user to pick from.
        /// </summary>
        public IEnumerable<IDiscordApplicationCommandOptionChoice>? Choices { get; set; }

        /// <summary>
        ///     If the option is a subcommand or subcommand group type, this nested options will be the parameters.
        /// </summary>
        public IEnumerable<IDiscordApplicationCommandOption>? SubOptions { get; set; }

        /// <summary>
        ///     If the option is a channel type, the channels shown will be restricted to these types.
        /// </summary>
        public IEnumerable<DiscordChannelType>? ChanelTypes { get; init; }

        /// <summary>
        ///     If the option is an INTEGER or NUMBER type, the minimum value permitted.
        /// </summary>
        public int? MinValue { get; init; }

        /// <summary>
        ///     If the option is an INTEGER or NUMBER type, the maximum value permitted.
        /// </summary>
        public int? MaxValue { get; init; }

        /// <summary>
        ///     Enable autocomplete interactions for this option.
        /// </summary>
        public bool? Autocomplete { get; init; }
    }
}