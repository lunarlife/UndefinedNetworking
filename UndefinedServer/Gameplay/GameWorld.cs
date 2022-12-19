namespace UndefinedServer.Gameplay
{
    public class GameWorld
    {
        public string Name { get; }
        public int Seed { get; }

        public GameWorld(string name, int seed)
        {
            Name = name;
            Seed = seed;
        }

        public void Unload()
        {
        }
    }
}