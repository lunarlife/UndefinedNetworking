using System;

namespace UndefinedServer.Exeptions;

public class WindowException : Exception
{
    public WindowException(string msg) : base(msg)
    {
        
    }
}