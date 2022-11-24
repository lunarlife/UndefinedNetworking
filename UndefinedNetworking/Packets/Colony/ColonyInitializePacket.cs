using Networking;
using Networking.Packets;
using UndefinedNetworking.Gameplay.Colonies;
using Utils.Dots;

namespace UndefinedNetworking.Packets.Colony
{
    public sealed class ColonyInitializePacket : Packet
    {
        public Dot2Int Position { get; private set; }
        public Identifier Owner { get; private set; }
        
        public ColonyInitializePacket(IColony colony, Identifier owner)
        {
            Position = colony.Position;
            Owner = owner;
        }

        private ColonyInitializePacket()
        {
            
        }
    }
}