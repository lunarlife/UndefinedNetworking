using Networking;
using Networking.DataConvert;
using Networking.Packets;
using Utils;

namespace UndefinedNetworking.Packets.Server
{
    [DataObject]
    public sealed class ClientInfoPacket : Packet
    {
        [DataProperty]
        public Identifier Identitifer { get; private set; }
        [DataProperty]
        public Version Version { get; private set; }
        [DataProperty]
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