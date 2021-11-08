using System;

namespace Color_Chan.Discord.Rest.Exceptions
{
    public class UnknownBucketException : Exception
    {
        public UnknownBucketException(string message) : base(message)
        {
        }
    }
}