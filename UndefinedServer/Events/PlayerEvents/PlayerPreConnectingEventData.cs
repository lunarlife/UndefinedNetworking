namespace UndefinedServer.Events.PlayerEvents
{
    public class PlayerPreConnectingEventDataData : PlayerEventData
    {
        public override Player Player { get; }
        
        public PlayerPreConnectingEventDataData(UndefinedServer.Player player)
        {
            Player = player;
        }
    }
}