namespace UndefinedServer.Events.PlayerEvents
{
    public class PlayerPreConnectingEvent : PlayerEvent
    {
        public override Player Player { get; }
        
        public PlayerPreConnectingEvent(UndefinedServer.Player player)
        {
            Player = player;
        }
    }
}