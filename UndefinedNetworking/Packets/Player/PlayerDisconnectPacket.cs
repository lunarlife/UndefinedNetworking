using Networking;
using Networking.Packets;

namespace UndefinedNetworking.Packets.Player
{
    public sealed class PlayerDisconnectPacket : Packet
    {
        public DisconnectCause Cause { get; private set; }
        public string Message { get; private set; }
        public PlayerDisconnectPacket(DisconnectCause cause, string message)
        {
            Cause = cause;
            Message = message;
        }
    }
}
