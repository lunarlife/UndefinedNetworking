namespace UndefinedServer.Events.PlayerEvents.PingEvents;

public sealed class TotalPingUpdateEventData : PingUpdateEventData
{
    public TotalPingUpdateEventData(Player player) : base(player)
    {
    }
}