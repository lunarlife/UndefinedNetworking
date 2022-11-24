using System;

namespace UndefinedServer.Exeptions;

public class ViewException : Exception
{
    public ViewException(string msg) : base(msg)
    {
        
    }
}