using System;

namespace UndefinedNetworking.Exceptions;

public class ComponentException : Exception
{
    public ComponentException(string msg) : base(msg)
    {
        
    }
}