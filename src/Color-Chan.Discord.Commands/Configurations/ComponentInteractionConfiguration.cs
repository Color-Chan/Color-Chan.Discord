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
        public bool SendDefaultErrorMessage { get; set; } = false;
    }
}