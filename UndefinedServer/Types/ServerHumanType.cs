using UndefinedNetworking.Types;

namespace UndefinedServer.Types
{
    public class ServerHumanType : HumanType
    {
        public override string HumanName { get; }
        /// <summary>
        /// path should be starts without Resources folder 
        /// </summary>
        public override string MainSpritePath { get; }
    }
}