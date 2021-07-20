using System;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    [Flags]
    public enum DiscordInteractionCommandCallbackFlags
    {
        Ephemeral = 1 << 6
    }
}