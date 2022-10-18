using System;

namespace UndefinedServer.Exeptions
{
    public class ServerConfigurationException : Exception
    {
        public ServerConfigurationException(string msg) : base(msg)
        {
            
        }
    }
}