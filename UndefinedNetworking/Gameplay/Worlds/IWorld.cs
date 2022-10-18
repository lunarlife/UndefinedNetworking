namespace UndefinedNetworking.Gameplay.Worlds
{
    public interface IWorld
    {
        public int Seed { get; }
        public string Name { get; }
        public IWater Water { get; }
        
    }
}