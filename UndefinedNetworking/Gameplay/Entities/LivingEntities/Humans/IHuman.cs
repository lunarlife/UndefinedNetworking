using System;
using UndefinedNetworking.Types;
using Utils.Dots;

namespace UndefinedNetworking.Gameplay.Entities.LivingEntities.Humans
{
    public interface IHuman : ILivingEntity
    {

        public IDamager LastDamager { get; }
        public HumanType Type { get; set; }
        public float Damage { get; }
        public Dot2 Position { get; set; }
        public float Speed { get; set; }
        public float Health { get; set; }
        public float MaxHealth { get; set; }
    }

    public class HumanException : Exception
    {
        public HumanException(string msg) : base(msg)
        {

        }
    }
}
