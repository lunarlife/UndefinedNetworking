using System.Collections.Generic;
using UndefinedNetworking.Gameplay.Colonies;
using UndefinedNetworking.Gameplay.Entities.LivingEntities.Humans;
using UndefinedNetworking.Types;
using Utils.Dots;

namespace UndefinedServer.Gameplay
{
    public class ServerColony : IColony
    {
        public Player Owner { get; }

        private List<IHuman> _humans = new();

        private Dot2Int _position;

        public Dot2Int Position => _position;
        public IEnumerable<IHuman> Humans => _humans;


        public ServerColony(Dot2Int position, Player owner)
        {
            _position = position;
            Owner = owner;
        }
        
        public IHuman AddHuman(HumanType type)
        {
            return null;
        }
    }
}