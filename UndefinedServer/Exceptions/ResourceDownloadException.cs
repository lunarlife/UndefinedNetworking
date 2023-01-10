using System;

namespace UndefinedServer.Exceptions;

public class ResourceDownloadException : Exception
{
    public ResourceDownloadException(string msg) : base(msg)
    {
        
    }
}