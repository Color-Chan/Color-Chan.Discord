using Color_Chan.Discord.Core;

namespace Color_Chan.Discord.Commands.Modules
{
    public interface ISlashCommandModuleBase
    {
        /// <summary>
        ///     Set the current <see cref="ISlashCommandContext" /> for a command.
        /// </summary>
        /// <param name="context">The new <see cref="ISlashCommandContext" />.</param>
        void SetContext(ISlashCommandContext context);
    }
}