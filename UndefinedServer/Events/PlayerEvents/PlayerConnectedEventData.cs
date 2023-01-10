namespace UndefinedServer.Events.PlayerEvents
{
    public class PlayerConnectedEventData : PlayerEventData
    {
        public override Player Player { get; }
        
        public PlayerConnectedEventData(Player player)
        {
            Player = player;
        }
    }
}