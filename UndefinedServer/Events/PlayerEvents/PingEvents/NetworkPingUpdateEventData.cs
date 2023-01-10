namespace UndefinedServer.Events.PlayerEvents.PingEvents;

public class NetworkPingUpdateEventData : PingUpdateEventData
{
    public NetworkPingUpdateEventData(Player player) : base(player)
    {
    }
}