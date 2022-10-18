using Networking;
using Networking.DataConvert;
using Networking.Packets;

namespace UndefinedNetworking.Packets.Player
{
    [DataObject]
    public sealed class PlayerDisconnectPacket : Packet
    {
        [DataProperty]
        public DisconnectCause Cause { get; private set; }
        [DataProperty]
        public Identifier Identifier { get; private set; }
        [DataProperty]
        public string Message { get; private set; }
        public PlayerDisconnectPacket(Identifier identifier, DisconnectCause cause, string message)
        {
            Identifier = identifier;
            Cause = cause;
            Message = message;
        }

        private PlayerDisconnectPacket()
        {
            
        }
    }
}
