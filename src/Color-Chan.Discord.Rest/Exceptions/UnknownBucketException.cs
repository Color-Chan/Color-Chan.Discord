using System;

namespace Color_Chan.Discord.Rest.Exceptions;

internal class UnknownBucketException : Exception
{
    internal UnknownBucketException(string message) : base(message)
    {
    }
}