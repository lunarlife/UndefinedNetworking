namespace UndefinedServer.Events.PlayerEvents.PingEvents;

public class NetworkPingUpdateEvent : PingUpdateEvent
{
    public NetworkPingUpdateEvent(Player player) : base(player)
    {
    }
}