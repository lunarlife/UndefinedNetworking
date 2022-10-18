using System.Collections.Generic;
using UndefinedNetworking.Gameplay.Entities.LivingEntities.Humans;
using Utils.Dots;

namespace UndefinedNetworking.Gameplay.Colonies
{
    public interface IColony
    {
        public Dot2Int Position { get; }
        public IEnumerable<IHuman> Humans { get; }
    }
}