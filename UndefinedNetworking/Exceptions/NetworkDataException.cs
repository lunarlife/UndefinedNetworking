using System;

namespace UndefinedNetworking.Exceptions;

public class NetworkDataException : Exception
{
    public NetworkDataException(string msg) : base(msg)
    {
        
    }
}