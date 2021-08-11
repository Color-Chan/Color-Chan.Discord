using System;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    [Flags]
    public enum DiscordInteractionCallbackFlags
    {
        None = 0,
        Ephemeral = 1 << 6
    }
}