using Color_Chan.Discord.Commands.Models.Contexts;

namespace Color_Chan.Discord.Commands.Configurations
{
    /// <summary>
    ///     This holds all the configurations for the component interactions.
    /// </summary>
    public class ComponentInteractionConfiguration
    {
        /// <summary>
        ///     Whether or not the command handler should send a default error message when a component interaction returned
        ///     unsuccessfully. Default: false.
        /// </summary>
        public bool SendDefaultErrorMessage { get; set; }
        
        /// <summary>
        ///     Whether or not <see cref="IInteractionContext.Guild" /> should be auto loaded on command requests. Default: false.
        /// </summary>
        public bool EnableAutoGetGuild { get; set; }

        /// <summary>
        ///     Whether or not <see cref="IInteractionContext.Channel" /> should be auto loaded on command requests. Default:
        ///     false.
        /// </summary>
        public bool EnableAutoGetChannel { get; set; }
    }
}