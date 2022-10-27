using System;

namespace UndefinedServer.Exeptions
{
    public class ObjectException : Exception
    {
        public ObjectException(string msg) : base(msg)
        {
            
        }
    }
}