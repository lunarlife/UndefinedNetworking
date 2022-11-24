using Networking;
using Networking.Packets;

namespace UndefinedNetworking.Packets.Player
{
    public sealed class PlayerConnectPacket : Packet
    {
        public Identifier Identitifer { get; private set; }
        public string Nickname { get; private set; }
        
        public PlayerConnectPacket(Identifier identitifer, string nickname)
        {
            Identitifer = identitifer;
            Nickname = nickname;
        }

        private PlayerConnectPacket()
        {
            
        }
    }    
}

