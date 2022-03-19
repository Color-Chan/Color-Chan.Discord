namespace Color_Chan.Discord.Core.Common.Models.Application
{
    public interface IDiscordApplicationCommandOptionChoice
    {
        /// <summary>
        ///     1-100 character choice name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Value of the choice, up to 100 characters if string.
        /// </summary>
        public object RawValue { get; set; }
    }
}