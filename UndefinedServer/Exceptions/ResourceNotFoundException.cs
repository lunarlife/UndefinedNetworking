using System;

namespace UndefinedServer.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string? msg = null) : base(msg)
    {
        
    }
}