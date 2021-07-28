using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.Models.Interaction
{
    public interface IDiscordInteractionCommandOption
    {
        /// <summary>
        ///     The name of the parameter.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Value of application command option type.
        /// </summary>
        DiscordApplicationCommandOptionType Type { get; set; }

        /// <summary>
        ///     The value of the option.
        /// </summary>
        object? Value { get; set; }

        /// <summary>
        ///     Present if this option is a group or subcommand.
        /// </summary>
        IEnumerable<IDiscordInteraction>? SubOptions { get; set; }

        /// <summary>
        ///     The <see cref="string" /> value of <see cref="Value" />.
        /// </summary>
        string? GetStringValue();

        /// <summary>
        ///     The <see cref="int" /> value of <see cref="Value" />.
        /// </summary>
        int GetIntValue();

        /// <summary>
        ///     The <see cref="double" /> value of <see cref="Value" />.
        /// </summary>
        double GetNumberValue();

        /// <summary>
        ///     The <see cref="bool" /> value of <see cref="Value" />.
        /// </summary>
        bool GetBoolValue();

        /// <summary>
        ///     The <see cref="IDiscordUser.Id" /> value of <see cref="Value" />.
        /// </summary>
        ulong GetUserValue();

        /// <summary>
        ///     The <see cref="IDiscordChannel.Id" /> value of <see cref="Value" />.
        /// </summary>
        ulong GetChannelValue();

        /// <summary>
        ///     The <see cref="IDiscordGuildRole.Id" /> value of <see cref="Value" />.
        /// </summary>
        ulong GetRoleValue();
    }
}