using System;

namespace UndefinedNetworking.Exceptions;

public sealed class RequireException : Exception
{
    public RequireException(string msg) : base(msg)
    {
        
    }
}