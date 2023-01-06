using System;

namespace UndefinedServer.Exceptions;

public class ResourcesLoadException : Exception
{
    public ResourcesLoadException(string msg) : base(msg)
    {
        
    }
}