namespace UndefinedServer.Events.PlayerEvents
{
    public class PlayerConnectedEvent : PlayerEvent
    {
        public override Player Player { get; }
        
        public PlayerConnectedEvent(Player player)
        {
            Player = player;
        }
    }
}