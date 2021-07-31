using Color_Chan.Discord.Core;

namespace Color_Chan.Discord.Commands.Modules
{
    /// <summary>
    ///     The base that should be used for all slash command modules.
    /// </summary>
    public interface ISlashCommandModule
    {
        /// <summary>
        ///     Set the current <see cref="ISlashCommandContext" /> for a command.
        /// </summary>
        /// <param name="context">The new <see cref="ISlashCommandContext" />.</param>
        void SetContext(ISlashCommandContext context);
    }
}