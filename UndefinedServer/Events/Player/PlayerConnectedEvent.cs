namespace UndefinedServer.Events.Player
{
    public class PlayerConnectedEvent : PlayerEvent
    {
        public override ServerPlayer Player { get; }
        
        public PlayerConnectedEvent(ServerPlayer player)
        {
            Player = player;
        }
    }
}