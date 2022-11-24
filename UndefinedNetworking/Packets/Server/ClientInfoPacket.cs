using Networking;
using Networking.Packets;
using Utils;

namespace UndefinedNetworking.Packets.Server
{
    public sealed class ClientInfoPacket : Packet
    {
        public Identifier Identitifer { get; private set; }
        public Version Version { get; private set; }
        public string Name { get; private set; }
        
        public ClientInfoPacket(Identifier identitifer, Version version, string name)
        {
            Identitifer = identitifer;
            Version = version;
            Name = name;
        }
        private ClientInfoPacket()
        {
            
        }
    }
}