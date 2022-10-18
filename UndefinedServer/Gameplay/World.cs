namespace UndefinedServer.Gameplay
{
    public class World
    {
        public string Name { get; }
        public int Seed { get; }

        public World(string name, int seed)
        {
            Name = name;
            Seed = seed;
        }

        public void Unload()
        {
        }
    }
}