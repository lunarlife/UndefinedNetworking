using System;

namespace UndefinedServer.Exceptions
{
    public class ServerConfigurationException : Exception
    {
        public ServerConfigurationException(string msg) : base(msg)
        {
            
        }
    }
}