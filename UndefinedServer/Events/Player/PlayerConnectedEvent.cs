namespace UndefinedServer.Events.Player
{
    public class PlayerConnectedEvent : PlayerEvent
    {
        public override UndefinedServer.Player Player { get; }
        
        public PlayerConnectedEvent(UndefinedServer.Player player)
        {
            Player = player;
        }
    }
}