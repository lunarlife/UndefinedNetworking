using System;

namespace UndefinedServer.Exceptions
{
    public class ChatException : Exception
    {
        public ChatException(string msg) : base(msg)
        {
            
        }
    }
}