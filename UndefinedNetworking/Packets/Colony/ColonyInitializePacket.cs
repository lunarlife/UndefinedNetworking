using Networking;
using Networking.DataConvert;
using Networking.Packets;
using UndefinedNetworking.Gameplay.Colonies;
using Utils.Dots;

namespace UndefinedNetworking.Packets.Colony
{
    [DataObject]
    public sealed class ColonyInitializePacket : Packet
    {
        [DataProperty]
        public Dot2Int Position { get; private set; }
        [DataProperty]
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