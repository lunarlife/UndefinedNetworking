using Networking.Packets;

namespace UndefinedNetworking.Packets.World
{
    public sealed class WorldPacket : Packet
    {
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