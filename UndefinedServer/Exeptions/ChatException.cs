using System;

namespace UndefinedServer.Exeptions
{
    public class ChatException : Exception
    {
        public ChatException(string msg) : base(msg)
        {
            
        }
    }
}