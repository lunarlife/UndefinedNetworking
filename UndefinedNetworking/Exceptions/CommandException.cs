using System;

namespace UndefinedNetworking.Exceptions
{
    public class CommandException : Exception
    {
        public CommandException(string msg) : base(msg)
        {
            
        }
    }
}