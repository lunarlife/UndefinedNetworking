using Networking.DataConvert;
using Networking.Packets;

namespace UndefinedNetworking.Packets.World
{
    [DataObject]
    public sealed class WorldPacket : Packet
    {
        [DataProperty]
        public int Seed { get; private set; }
        
        public WorldPacket(int seed)
        {
            Seed = seed;
        }

        private WorldPacket()
        {
            
        }
    }
}