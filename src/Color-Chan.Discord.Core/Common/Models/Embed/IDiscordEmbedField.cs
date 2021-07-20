using Color_Chan.Discord.Core.Common.API.DataModels.Embed;

namespace Color_Chan.Discord.Core.Common.Models.Embed
{
    public interface IDiscordEmbedField
    {
        /// <summary>
        ///     Name of the field.
        /// </summary>
        string Name { get; init; }

        /// <summary>
        ///     Value of the field.
        /// </summary>
        string Value { get; init; }

        /// <summary>
        ///     Whether or not this field should display inline.
        /// </summary>
        bool? Inline { get; init; }

        /// <summary>
        ///     Converts the model back to a discord data model so that it can be send to discord.
        /// </summary>
        /// <returns>
        ///     The converted <see cref="DiscordEmbedFieldData" />.
        /// </returns>
        DiscordEmbedFieldData ToDataModel();
    }
}