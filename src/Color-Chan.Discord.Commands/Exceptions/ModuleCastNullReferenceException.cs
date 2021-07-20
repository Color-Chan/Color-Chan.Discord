using System;

namespace Color_Chan.Discord.Commands.Exceptions
{
    public class ModuleCastNullReferenceException : NullReferenceException
    {
        public ModuleCastNullReferenceException(string message) : base(message)
        {
        }
    }
}