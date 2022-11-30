namespace UndefinedServer.Events.PlayerEvents
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