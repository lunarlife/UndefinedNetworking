using Networking;
using Networking.DataConvert;
using Networking.Packets;

namespace UndefinedNetworking.Packets.Player
{
    [DataObject]
    public sealed class PlayerConnectPacket : Packet
    {
        [DataProperty]
        public Identifier Identitifer { get; private set; }
        [DataProperty]
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

