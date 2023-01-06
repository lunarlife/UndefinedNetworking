using System;

namespace UndefinedNetworking.GameEngine.Jobs;

public sealed class JobException : Exception
{
    public JobException(string msg) : base(msg) 
    {
        
    }
}