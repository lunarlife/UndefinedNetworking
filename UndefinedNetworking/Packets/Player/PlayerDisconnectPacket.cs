using Networking;
using Networking.Packets;

namespace UndefinedNetworking.Packets.Player
{
    public sealed class PlayerDisconnectPacket : Packet
    {
        public DisconnectCause Cause { get; private set; }
        public Identifier Identifier { get; private set; }
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
