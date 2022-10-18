using Utils.Dots;

namespace UndefinedNetworking.Gameplay.Worlds
{
    public interface IWater
    {
        public Dot2Int Direction { get; }
        public int Speed { get; }
    }
}